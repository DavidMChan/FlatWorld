using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableController : MonoBehaviour {
  
    public Transform controlled_object;
    public Transform hmd;

    public float x_off = 0.0f;
    public float y_off = 0.0f;
    public float z_off = 0.93f;

    public float start_pos_x = 0.0f;
    public float start_pos_y = 0.0f;
    public float start_pos_z = 0.0f;

    public float start_rot_x = -90.0f;
    public float start_rot_y = 0.0f;
    public float start_rot_z = 90.0f;


	// Use this for initialization
	void Start () {
        controlled_object.position = new Vector3(start_pos_x, start_pos_y, start_pos_z);
        controlled_object.eulerAngles = new Vector3(start_rot_x, start_rot_y, start_rot_z);
	}
	
	// Update is called once per frame
	void Update () {
        if (Valve.VR.SteamVR_Input._default.inActions.YPress.GetState(Valve.VR.SteamVR_Input_Sources.Any)) {
            controlled_object.position = new Vector3(hmd.position.x + x_off, hmd.position.y + y_off, hmd.position.z + z_off);
            Debug.Log(controlled_object.position);
        }
        if (Valve.VR.SteamVR_Input._default.inActions.XPress.GetState(Valve.VR.SteamVR_Input_Sources.Any)) {
            controlled_object.rotation = Quaternion.Euler(controlled_object.rotation.eulerAngles.x, controlled_object.rotation.eulerAngles.y + 0.05f, controlled_object.rotation.eulerAngles.z);
            Debug.Log(controlled_object.rotation.eulerAngles);
        }
        if (Valve.VR.SteamVR_Input._default.inActions.ZPress.GetState(Valve.VR.SteamVR_Input_Sources.Any)) {
            controlled_object.rotation = Quaternion.Euler(controlled_object.rotation.eulerAngles.x, controlled_object.rotation.eulerAngles.y - 0.05f, controlled_object.rotation.eulerAngles.z);
            Debug.Log(controlled_object.rotation.eulerAngles);
        }
    }
}
