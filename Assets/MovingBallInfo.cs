using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MovingBallInfo {
	public float x_offset;
    public float z_offset;
    public float y_offset;
    
    public float rotation;
    
    public string type;
    
    public float scale_x;
    public float scale_y;
    public float scale_z;

    public bool wireframe;
    public bool show_error;

    public string handPath;
    public string error_type;
    public bool enable_tracking;
}
