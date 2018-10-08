using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBall : MonoBehaviour {

    public float speed = 0.01f;
    public Transform table;


    public float y_offset, x_offset, z_offset;
    public enum Selector {Up, Down, Left, Right, X};
    public Selector selector;

    // Use this for initialization
    void Start() {
        this.y_offset = this.transform.position.y - table.position.y;
        this.x_offset = this.transform.position.x - table.position.x;
        this.z_offset = this.transform.position.z - table.position.z;
    }

    void update_motion() {
        if (Valve.VR.SteamVR_Input._default.inActions.PosJR.GetAxis(Valve.VR.SteamVR_Input_Sources.Any).x > 0.5) {
            x_offset += Valve.VR.SteamVR_Input._default.inActions.PosJR.GetAxis(Valve.VR.SteamVR_Input_Sources.Any).x * speed;
        }
        if (Valve.VR.SteamVR_Input._default.inActions.PosJR.GetAxis(Valve.VR.SteamVR_Input_Sources.Any).x < -0.5) {
            x_offset += Valve.VR.SteamVR_Input._default.inActions.PosJR.GetAxis(Valve.VR.SteamVR_Input_Sources.Any).x * speed;
        }
        if (Valve.VR.SteamVR_Input._default.inActions.PosJR.GetAxis(Valve.VR.SteamVR_Input_Sources.Any).y > 0.5) {
            z_offset += Valve.VR.SteamVR_Input._default.inActions.PosJR.GetAxis(Valve.VR.SteamVR_Input_Sources.Any).y * speed;
        }
        if (Valve.VR.SteamVR_Input._default.inActions.PosJR.GetAxis(Valve.VR.SteamVR_Input_Sources.Any).y < -0.5) {
            z_offset += Valve.VR.SteamVR_Input._default.inActions.PosJR.GetAxis(Valve.VR.SteamVR_Input_Sources.Any).y * speed;
        }
    }

    // Update is called once per frame
    void Update() {
        Vector3 current_position = table.position;
        if (selector == Selector.Up && Valve.VR.SteamVR_Input._default.inActions.PoseJL.GetAxis(Valve.VR.SteamVR_Input_Sources.Any).y > 0.9) {
            update_motion();          
        } else if (selector == Selector.X && Valve.VR.SteamVR_Input._default.inActions.XPress.GetState(Valve.VR.SteamVR_Input_Sources.Any)) {
            update_motion();
        } else if (selector == Selector.Down && Valve.VR.SteamVR_Input._default.inActions.PoseJL.GetAxis(Valve.VR.SteamVR_Input_Sources.Any).y < -0.9) {
            update_motion();
        } else if (selector == Selector.Left && Valve.VR.SteamVR_Input._default.inActions.PoseJL.GetAxis(Valve.VR.SteamVR_Input_Sources.Any).x < -0.9) {
            update_motion();
        } else if (selector == Selector.Right && Valve.VR.SteamVR_Input._default.inActions.PoseJL.GetAxis(Valve.VR.SteamVR_Input_Sources.Any).x > 0.9) {
            update_motion();
        }

        // Set the position offset vs the table
        this.gameObject.transform.position = new Vector3(x_offset + current_position.x, y_offset + current_position.y, z_offset + current_position.z);
    }
}
