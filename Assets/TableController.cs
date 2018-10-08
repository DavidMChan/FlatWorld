using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableController : MonoBehaviour {

    public float speed = 0.01f;
    public Transform controlled_object;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        if (Valve.VR.SteamVR_Input._default.inActions.YPress.GetState(Valve.VR.SteamVR_Input_Sources.Any)) {

            // Set the position of the table (Attached to the left controller)
            float dx = 0.0f;
            float dy = 0.0f;
            float dz = 0.0f;

            Vector3 current_position = controlled_object.position;
            if (Valve.VR.SteamVR_Input._default.inActions.PosJR.GetAxis(Valve.VR.SteamVR_Input_Sources.Any).x > 0.5) {
                dx += Valve.VR.SteamVR_Input._default.inActions.PosJR.GetAxis(Valve.VR.SteamVR_Input_Sources.Any).x * speed;
            }
            if (Valve.VR.SteamVR_Input._default.inActions.PosJR.GetAxis(Valve.VR.SteamVR_Input_Sources.Any).x < -0.5) {
                dx += Valve.VR.SteamVR_Input._default.inActions.PosJR.GetAxis(Valve.VR.SteamVR_Input_Sources.Any).x * speed;
            }
            if (Valve.VR.SteamVR_Input._default.inActions.PosJR.GetAxis(Valve.VR.SteamVR_Input_Sources.Any).y > 0.5) {
                dz += Valve.VR.SteamVR_Input._default.inActions.PosJR.GetAxis(Valve.VR.SteamVR_Input_Sources.Any).y * speed;
            }
            if (Valve.VR.SteamVR_Input._default.inActions.PosJR.GetAxis(Valve.VR.SteamVR_Input_Sources.Any).y < -0.5) {
                dz += Valve.VR.SteamVR_Input._default.inActions.PosJR.GetAxis(Valve.VR.SteamVR_Input_Sources.Any).y * speed;
            }
            if (Valve.VR.SteamVR_Input._default.inActions.WPress.GetState(Valve.VR.SteamVR_Input_Sources.Any)) {
                dy += 0.5f * speed;
            }
            if (Valve.VR.SteamVR_Input._default.inActions.ZPress.GetState(Valve.VR.SteamVR_Input_Sources.Any)) {
                dy -= 0.5f * speed;
            }
            controlled_object.position = new Vector3(dx + current_position.x, dy + current_position.y, dz + current_position.z);

        }

        
	}
}
