using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ExperimentInfo {
	public MovingBallInfo[] data;
    public bool show_hands;
    public string hand_type;
	public float kinect_x_offset;
	public float kinect_y_offset;
	public float kinect_z_offset;
}
