using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MovingBallInfo {
	public float x_offset = 0.0f;
    public float z_offset = 0.0f;
    public float y_offset = 0.0f;
    
    public float rotation = 0.0f;
    
    public string type = "sphere";
    
    public float scale_x = 1.0f;
    public float scale_y = 1.0f;
    public float scale_z = 1.0f;

    public bool wireframe = false;
    public bool show_error = true;

    public string handPath = null;
    public string error_type = "heatmap";
    public bool enable_tracking = false;
}
