using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPositionManager : MonoBehaviour {

    public bool from_leap = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
    void Update () {
        if (from_leap) {
            this.gameObject.transform.position = this.gameObject.GetComponent<Leap.Unity.CapsuleHand>().palm_position;
        } else {
            this.gameObject.transform.position = GameObject.FindGameObjectWithTag("vive_tracker_hand").transform.position;
        }
		
	}
}
