using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBall : MonoBehaviour {

    public float speed = 0.01f;
    public Transform table;


    public float y_offset = 0.0F, x_offset = 0.0F, z_offset = 0.0F;
    public bool is_wireframe;

    void Awake()
    {
        //this.table = GameObject.FindGameObjectWithTag("table").transform;
    }

    public void SetWireframe() {
        this.GetComponent<MeshRenderer>().material = GameObject.FindGameObjectWithTag("position_controller").GetComponent<PositionController>().wireframe_material;
    }

    // Update is called once per frame
    protected virtual void Update() {
        // Set the position offset vs the table
        Vector3 current_position = table.position;
        this.gameObject.transform.position = new Vector3(x_offset + current_position.x, y_offset + current_position.y, z_offset + current_position.z);
    }
}
