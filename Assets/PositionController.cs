﻿using System.Collections;
using System.Collections.Generic;
using System.IO;       
using UnityEngine;

public class PositionController : MonoBehaviour {
    public string sceneDataFileName;
    public Transform table;

    public ExperimentInfo[] experiment_info;

    public ErrorModel error_model;
    public float error_model_conversion_factor;

    private List<GameObject> tracked_game_objects;
    public MovingBall[] object_samples;
    public ProjectionError error_sample;
    public ProjectionError heatmap_error_sample;

    public GameObject hand_model_l;
    public GameObject hand_model_r;

    // List<List<Vector3>> positions;
    public AudioSource save_sound;
    public AudioSource toggle_sound;
    

    public Material wireframe_material;

    public int current_index = 0;

	// Use this for initialization
	void Start () {
        StartCoroutine(LateStart(1));
	}

    IEnumerator LateStart(float waitTime) {
        yield return new WaitForSeconds(waitTime);

        // positions = new List<List<Vector3>>();
        tracked_game_objects = new List<GameObject>();
        error_model = new ErrorModel(error_model_conversion_factor);

        // Load the data and objects into the scene.
        loadData();
        updateObjects();
    }

    void updateObjects() {

        // Clean up the scene objects
        foreach (GameObject ext_object in tracked_game_objects) {
            Destroy(ext_object);
        }
        tracked_game_objects.Clear();

        // Set the hand information for the current index
        if (experiment_info[current_index].show_hands) {
            hand_model_l.SetActive(true);
            hand_model_r.SetActive(true);
        } else {
            hand_model_r.SetActive(false);
            hand_model_l.SetActive(false);
        }

        // Get the information of the moving balls
        MovingBallInfo[] object_infos = experiment_info[current_index].data;
        error_model.UpdateKinectPosition(experiment_info[current_index].kinect_x_offset, experiment_info[current_index].kinect_y_offset, experiment_info[current_index].kinect_z_offset);

        // Set the hand tracking
        foreach (HandPositionManager hp in GameObject.FindObjectsOfType<HandPositionManager>()) {
            hp.from_leap = !experiment_info[current_index].use_vive_tracker;
        }

        // Construct the object
        foreach (MovingBallInfo info in object_infos) {
            // Instantiate the new object based on the reference object
            MovingBall new_object = null;
            if (info.type == "sphere")
            {
                new_object = Instantiate(object_samples[0]);
            }
            else if (info.type == "cylinder")
            {
                new_object = Instantiate(object_samples[1]);
            }
            else if (info.type == "cube")
            {
                new_object = Instantiate(object_samples[2]);
            }
            else if (info.type == "flat-cylinder")
            {
                new_object = Instantiate(object_samples[3]);
            }
            else if (info.type == "hand")
            {
                HandTracker.Hand h = HandTracker.LoadHand(info.handPath);
                h._sphere_material = GameObject.FindGameObjectWithTag("hand_tracker").GetComponent<HandTracker>()._sphere_material;
                h._cyl_material = GameObject.FindGameObjectWithTag("hand_tracker").GetComponent<HandTracker>()._cyl_material;
                new_object = Instantiate(object_samples[4]);
                new_object.GetComponent<HandMovingBall>().h = h;

                // ref_object = object_samples[4];
            } 
            else if (info.type == "controller")
            {
                new_object = Instantiate(object_samples[5]);
            }
            else if (info.type == "neutral")
            {
                new_object = Instantiate(object_samples[6]);
            }
            else
            {
                Debug.Log("Error. Invalid object type.");
                continue;
            }
            new_object.use_vive = info.enable_tracking;


            // Set the x, y and z offset from the info
            new_object.x_offset = info.x_offset;
            new_object.z_offset = info.z_offset;
            new_object.y_offset = info.y_offset;

            new_object.xrot_offset = info.xrot_offset;// + t.start_rot_x;
            new_object.zrot_offset = info.zrot_offset;// + t.start_rot_z;
            new_object.yrot_offset = info.yrot_offset;// + t.start_rot_y;

           
            if (info.wireframe) {
                new_object.SetWireframe();
            }
            
            // Set the rotation and scale from the info
            new_object.transform.eulerAngles = new Vector3(new_object.transform.eulerAngles.x, new_object.transform.eulerAngles.y , new_object.transform.eulerAngles.z);
            new_object.transform.localScale = new Vector3(info.scale_x, info.scale_y, info.scale_z);
            
            // Add the object to the tracked game objects class
            tracked_game_objects.Add(new_object.gameObject);
            if (info.show_error) {
                tracked_game_objects.Add(addErrors(new_object, info.error_type));
            }

        }

    }
    GameObject addErrors(MovingBall m, string error_type) {
        float error_std_x = error_model.GetErrorStdX(m);
        float error_std_y = error_model.GetErrorStdY(m);
        float error_std_z = error_model.GetErrorStdZ(m);
        Debug.LogFormat("Errors: X: {0}, Y: {1}, Z: {2}", error_std_x, error_std_y, error_std_z);
        ProjectionError error_object = null;
        if (error_type == "shell") {
            // Sample error object
            error_object = Instantiate<ProjectionError>(error_sample);
           
        } else if (error_type == "heatmap") {
            error_object = Instantiate<ProjectionError>(heatmap_error_sample);
           
            
        } else {
            Debug.Log("You've fucked up.");
            return null;
        }
        error_object.parent_object = m;
        error_object.err_x = error_std_x;
        error_object.err_y = error_std_y;
        error_object.err_z = error_std_z;
        error_object.error_type = error_type;
        return error_object.gameObject;
    }

    void loadData() {
        string filePath = Path.Combine(Application.dataPath, sceneDataFileName);
        if(File.Exists(filePath))
        {
            // Read the json from the file into a string
            string dataAsJson = File.ReadAllText(filePath);
            // Pass the json to JsonUtility, and tell it to create a StudyInfo object from it
            StudyInfo loadedData = JsonUtility.FromJson<StudyInfo>(dataAsJson);

            // Retrieve the experiments property of loadedData
            experiment_info = loadedData.experiments;
        }
        else
        {
            Debug.LogError("Cannot load game data!");
        }
    }
	
	// Update is called once per frame
	void Update () {
        // First cycle through positions 
        if (Valve.VR.SteamVR_Input._default.inActions.WPress.GetStateDown(Valve.VR.SteamVR_Input_Sources.Any)) {
            current_index = (current_index + 1) % experiment_info.Length;
            updateObjects();
            toggle_sound.PlayOneShot(toggle_sound.clip);
            GameObject.FindGameObjectWithTag("logger").GetComponent<CSVLogger>().Log("experiment_changed,"+ current_index.ToString());
        }
        
    }

    void LateUpdate ()
    {
       if (experiment_info.Length > 0)
        {
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("capsule_hand"))
            {
                g.GetComponent<Leap.Unity.CapsuleHand>().draw = experiment_info[current_index].hand_type;
            }
        }
        
    }
}
