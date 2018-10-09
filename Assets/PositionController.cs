using System.Collections;
using System.Collections.Generic;
using System.IO;       
using UnityEngine;

public class PositionController : MonoBehaviour {

    public Transform table;

    public ExperimentInfo[] experiment_info;

    private List<GameObject> tracked_game_objects;
    public MovingBall[] object_samples;

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

        // Construct the object
        foreach (MovingBallInfo info in object_infos) {
            MovingBall ref_object = null;
            if (info.type == "sphere") {
                ref_object = object_samples[0];
            } else if (info.type == "cylinder") {
                ref_object = object_samples[1];
            } else if (info.type == "cube") {
                ref_object = object_samples[2];
            } else if (info.type == "flat-cylinder") {
                ref_object = object_samples[3];
            } else {
                Debug.Log("Error. Invalid object type.");
                continue;
            }

            // Instantiate the new object based on the reference object
            MovingBall new_object = Instantiate(ref_object);

            // Set the x, y and z offset from the info
            new_object.x_offset = info.x_offset;
            new_object.z_offset = info.z_offset;
            new_object.y_offset = info.y_offset;
            if (info.wireframe) {
                new_object.SetWireframe();
            }

            // Set the rotation and scale from the info
            new_object.transform.eulerAngles = new Vector3(new_object.transform.eulerAngles.x, info.rotation, new_object.transform.eulerAngles.z);
            new_object.transform.localScale = new Vector3(info.scale_x, info.scale_y, info.scale_z);

            // Add the object to the tracked game objects class
            tracked_game_objects.Add(new_object.gameObject);
        }

    }

    void loadData() {
        string sceneDataFileName = "data.json";
        string filePath = Path.Combine(Application.dataPath, sceneDataFileName);
        if(File.Exists(filePath))
        {
            // Read the json from the file into a string
            string dataAsJson = File.ReadAllText(filePath);
            // Pass the json to JsonUtility, and tell it to create a GameData object from it
            StudyInfo loadedData = JsonUtility.FromJson<StudyInfo>(dataAsJson);

            // Retrieve the allRoundData property of loadedData
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
        }
    }
}
