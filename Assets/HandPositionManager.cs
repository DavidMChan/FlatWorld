using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPositionManager : MonoBehaviour {

    public bool from_leap = true;

    public Vector3 position;
    public Vector3 rotation;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
    void Update () {
        if (from_leap) {
            this.position = this.gameObject.GetComponent<Leap.Unity.CapsuleHand>().palm_position;
            this.rotation = this.gameObject.GetComponent<Leap.Unity.CapsuleHand>().palm_rotation;
        } else {
            this.position = GameObject.FindGameObjectWithTag("vive_tracker_hand").transform.position;
            this.rotation = GameObject.FindGameObjectWithTag("vive_tracker_hand").transform.eulerAngles;
        }	
        this.gameObject.transform.position = position;
        this.gameObject.transform.eulerAngles = rotation;

        string[] hand_data =
        {
            this.position.x.ToString(),
            this.position.y.ToString(),
            this.position.z.ToString(),
            this.rotation.x.ToString(),
            this.rotation.y.ToString(),
            this.rotation.z.ToString()
        };

        GameObject.FindGameObjectWithTag("logger").GetComponent<CSVLogger>().Log("hand_6dof,"+ string.Join(",",  hand_data));
        
	}
}
