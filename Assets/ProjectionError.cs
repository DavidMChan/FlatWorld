using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectionError : MonoBehaviour
{

    public MovingBall parent_object;
    public float err_x;
    public float err_z;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localScale = new Vector3(err_x, 1, err_z);
        this.transform.position = new Vector3(this.parent_object.transform.position.x, 0.08f, this.parent_object.transform.position.z);
    }
}
