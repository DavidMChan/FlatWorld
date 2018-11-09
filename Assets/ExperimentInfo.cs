using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ExperimentInfo {
	public MovingBallInfo[] data;
    public bool show_hands = true;
    public string hand_type = "leap";
    public bool use_vive_tracker = false;
	public float kinect_x_offset = 0.0f;
	public float kinect_y_offset = 1.0f;
	public float kinect_z_offset = 0.0f;
}
