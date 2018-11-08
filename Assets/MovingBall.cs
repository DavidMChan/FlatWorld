using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBall : MonoBehaviour {

    public float speed = 0.01f;
    public Transform table;
    public bool use_vive = true;


    public float y_offset = 0.0F, x_offset = 0.0F, z_offset = 0.0F;
    public bool is_wireframe;
    public bool update_table = true;
    void Awake()
    {
        
    }

    public void SetWireframe() {
        this.GetComponent<MeshRenderer>().material = GameObject.FindGameObjectWithTag("position_controller").GetComponent<PositionController>().wireframe_material;
    }

    // Update is called once per frame
    protected virtual void Update(){
       
        if (update_table)
        {

            if (use_vive) {
                this.table = GameObject.FindGameObjectWithTag("vive_tracker_object").transform;
                Vector3 transform = this.gameObject.transform.position;
                Vector3 rotation = this.gameObject.transform.eulerAngles;
                GameObject.FindGameObjectWithTag("logger").GetComponent<Logger>().Log("object_6dof,"+ string.Join([transform.x.ToString(), transform.y.ToString(), transform.z.ToString(), rotation.x.ToString(), rotation.y.ToString(), rotation.z.ToString()] , ","));
            } else {
                this.table = GameObject.FindGameObjectWithTag("table").transform;
            }
        }


        // Set the position offset vs the table
        Vector3 current_position = table.position;
        this.gameObject.transform.position = new Vector3(x_offset + current_position.x, y_offset + current_position.y, z_offset + current_position.z);
      
    }
}
