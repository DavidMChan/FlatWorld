using System.Collections;
using System.Collections.Generic;
using System.IO;       
using UnityEngine;

public class PositionController : MonoBehaviour {

    public Transform table;

    public ExperimentInfo[] experiment_infos;

    public List<MovingBall> tracked_game_objects;
    public MovingBall[] object_samples;

    // List<List<Vector3>> positions;
    public AudioSource save_sound;
    public AudioSource toggle_sound;

    public int current_index = 0;

	// Use this for initialization
	void Start () {
        StartCoroutine(LateStart(1));
	}

    IEnumerator LateStart(float waitTime) {
        yield return new WaitForSeconds(waitTime);

        // positions = new List<List<Vector3>>();
        tracked_game_objects = new List<MovingBall>();
        // List<Vector3> initial_positions = new List<Vector3>();
        // initial_positions.Add(table.position);
        loadData();
        updateObjects();

        
        /*
        foreach (MovingBall mb in tracked_game_objects) {
            initial_positions.Add(new Vector3(mb.x_offset, mb.y_offset, mb.z_offset));
        }
        positions.Add(initial_positions);
        */
    }

    void updateObjects() {
        foreach (MovingBall ext_object in tracked_game_objects) {
            Destroy(ext_object);
        }
        
        tracked_game_objects.Clear();

        MovingBallInfo[] object_infos = experiment_infos[current_index].data;
        foreach (MovingBallInfo info in object_infos) {
            MovingBall ref_object = object_samples[0];
            if (info.type == "sphere") {
                ref_object = object_samples[0];
            }
            if (info.type == "cylinder") {
                ref_object = object_samples[1];
            }
            if (info.wireframe) {
                GL.wireframe = true;
            }
            MovingBall new_object = Instantiate(ref_object);
            new_object.x_offset = info.x_offset;
            new_object.z_offset = info.z_offset;
            new_object.transform.eulerAngles = new Vector3(0, info.rotation, 0);
            new_object.transform.localScale += new Vector3(info.scale, info.scale, info.scale);

            tracked_game_objects.Add(new_object);
            GL.wireframe = false;
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
            experiment_infos = loadedData.experiments;
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
            current_index = (current_index + 1) % experiment_infos.Length;
            updateObjects();
            // table.position = new Vector3(positions[current_index][0].x, positions[current_index][0].y, positions[current_index][0].z);
            // Edit the offsets of each of the minor elements
            /*
            int j = 1;
            for (int i = 0; i < tracked_game_objects.Count; i++) {
                if (tracked_game_objects[i] != null) {
                    tracked_game_objects[i].x_offset = positions[current_index][j].x;
                    tracked_game_objects[i].y_offset = positions[current_index][j].y;
                    tracked_game_objects[i].z_offset = positions[current_index][j].z;
                    j += 1;
                }
            }
            */
            toggle_sound.PlayOneShot(toggle_sound.clip);
        }

        // Store the positions if the other button is pressed
        /*
        if (Valve.VR.SteamVR_Input._default.inActions.ZPress.GetStateDown(Valve.VR.SteamVR_Input_Sources.Any)) {
            current_index = positions.Count;
            List<Vector3> current_position_set = new List<Vector3>();
            current_position_set.Add(table.position);
            // Edit the offsets of each of the minor elements
            for (int i = 0; i < tracked_game_objects.Count; i++) {
                if (tracked_game_objects[i] != null) {
                    current_position_set.Add(new Vector3(tracked_game_objects[i].x_offset, tracked_game_objects[i].y_offset, tracked_game_objects[i].z_offset));
                }
            }
            positions.Add(current_position_set);
            save_sound.PlayOneShot(save_sound.clip);
        }
        */


    }
}
