using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBall : MonoBehaviour {

    public float speed = 0.01f;
    public Vector3 size;
    public Transform renderedObject;
    public bool use_vive = true;


    public float y_offset = 0.0F, x_offset = 0.0F, z_offset = 0.0F;
    public float yrot_offset = 0.0F, xrot_offset = 0.0F, zrot_offset = 0.0F;
   

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


                this.renderedObject = GameObject.FindGameObjectWithTag("vive_tracker_object").transform;
                Vector3 transform = this.gameObject.transform.position;
                Vector3 rotation = this.gameObject.transform.eulerAngles;

                string[] data =
                {
                    transform.x.ToString(),
                    transform.y.ToString(),
                    transform.z.ToString(),
                    rotation.x.ToString(),
                    rotation.y.ToString(),
                    rotation.z.ToString()
                };

                GameObject.FindGameObjectWithTag("logger").GetComponent<CSVLogger>().Log("object_6dof,"+ string.Join(",", data));
                Vector3 current_rotation = renderedObject.eulerAngles;
                Debug.Log("BEFORE: " + this.gameObject.transform.eulerAngles);
                this.gameObject.transform.eulerAngles = new Vector3(current_rotation.x + xrot_offset, current_rotation.y + yrot_offset, current_rotation.z + zrot_offset);
                this.gameObject.transform.Rotate(new Vector3(1, 0, 0), 90f, Space.Self);
                Debug.Log("AFTER: " + this.gameObject.transform.eulerAngles);
            } else {
                this.renderedObject = GameObject.FindGameObjectWithTag("table").transform;
            }
        }


        // Set the position offset vs the table
        Vector3 current_position = renderedObject.position;
        this.gameObject.transform.position = new Vector3(x_offset + current_position.x, y_offset + current_position.y, z_offset + current_position.z);
        MeshRenderer mr = this.GetComponent<MeshRenderer>();
        if (!mr)
        {
            mr = this.GetComponentInChildren<MeshRenderer>();
        }
        size = mr.bounds.size;
    }
}
