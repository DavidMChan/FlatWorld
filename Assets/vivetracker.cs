using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vivetracker : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 position = Valve.VR.SteamVR_Input._default.inActions.Pose.GetLocalPosition((Valve.VR.SteamVR_Input_Sources)3);
        Debug.Log(position);

    }
}
