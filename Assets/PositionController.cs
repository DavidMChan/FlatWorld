using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionController : MonoBehaviour {

    public Transform table;
    public MovingBall[] tracked_game_objects;
    List<List<Vector3>> positions;
    public AudioSource save_sound;
    public AudioSource toggle_sound;

    public int current_index = 0;

	// Use this for initialization
	void Start () {
        StartCoroutine(LateStart(1));
	}

    IEnumerator LateStart(float waitTime) {
        yield return new WaitForSeconds(waitTime);

        positions = new List<List<Vector3>>();
        List<Vector3> initial_positions = new List<Vector3>();
        initial_positions.Add(table.position);
        foreach (MovingBall mb in tracked_game_objects) {
            initial_positions.Add(new Vector3(mb.x_offset, mb.y_offset, mb.z_offset));
        }
        positions.Add(initial_positions);
    }
	
	// Update is called once per frame
	void Update () {
        // First cycle through positions 
        if (Valve.VR.SteamVR_Input._default.inActions.WPress.GetStateDown(Valve.VR.SteamVR_Input_Sources.Any)) {
            current_index = (current_index + 1) % positions.Count;
            table.position = new Vector3(positions[current_index][0].x, positions[current_index][0].y, positions[current_index][0].z);
            // Edit the offsets of each of the minor elements
            int j = 1;
            for (int i = 0; i < tracked_game_objects.Length; i++) {
                if (tracked_game_objects[i] != null) {
                    tracked_game_objects[i].x_offset = positions[current_index][j].x;
                    tracked_game_objects[i].y_offset = positions[current_index][j].y;
                    tracked_game_objects[i].z_offset = positions[current_index][j].z;
                    j += 1;
                }
            }
            toggle_sound.PlayOneShot(toggle_sound.clip);
        }

        // Store the positions if the other button is pressed
        if (Valve.VR.SteamVR_Input._default.inActions.ZPress.GetStateDown(Valve.VR.SteamVR_Input_Sources.Any)) {
            current_index = positions.Count;
            List<Vector3> current_position_set = new List<Vector3>();
            current_position_set.Add(table.position);
            // Edit the offsets of each of the minor elements
            for (int i = 0; i < tracked_game_objects.Length; i++) {
                if (tracked_game_objects[i] != null) {
                    current_position_set.Add(new Vector3(tracked_game_objects[i].x_offset, tracked_game_objects[i].y_offset, tracked_game_objects[i].z_offset));
                }
            }
            positions.Add(current_position_set);
            save_sound.PlayOneShot(save_sound.clip);
        }


    }
}
