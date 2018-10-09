using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableController : MonoBehaviour {
  
    public Transform controlled_object;
    public Transform hmd;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (Valve.VR.SteamVR_Input._default.inActions.YPress.GetState(Valve.VR.SteamVR_Input_Sources.Any)) {
            controlled_object.position = new Vector3(hmd.position.x, hmd.position.y, hmd.position.z + .95f);
        }
        if (Valve.VR.SteamVR_Input._default.inActions.XPress.GetState(Valve.VR.SteamVR_Input_Sources.Any)) {
            controlled_object.rotation = Quaternion.Euler(controlled_object.rotation.eulerAngles.x, controlled_object.rotation.eulerAngles.y + 0.05f, controlled_object.rotation.eulerAngles.z);
        }
        if (Valve.VR.SteamVR_Input._default.inActions.ZPress.GetState(Valve.VR.SteamVR_Input_Sources.Any)) {
            controlled_object.rotation = Quaternion.Euler(controlled_object.rotation.eulerAngles.x, controlled_object.rotation.eulerAngles.y - 0.05f, controlled_object.rotation.eulerAngles.z);
        }
    }
}
