using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectionError : MonoBehaviour
{

    public MovingBall parent_object;
    public float err_x;
    public float err_z;
    public float err_y;

    public string error_type;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (error_type == "shell")
        {
            this.transform.localScale = new Vector3(err_x, err_y, err_z) + parent_object.size;
            this.transform.position = new Vector3(this.parent_object.transform.position.x, this.parent_object.transform.position.y, this.parent_object.transform.position.z);
        }
        else if (error_type == "heatmap")
        {
            this.transform.localScale = new Vector3(err_x, err_z, 1) + parent_object.size;
            this.transform.position = new Vector3(this.parent_object.transform.position.x, 0.78f, this.parent_object.transform.position.z);
        }
    }
}
