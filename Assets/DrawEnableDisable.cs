﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawEnableDisable : MonoBehaviour {
    public string name;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        string partname = this.transform.parent.GetComponent<Leap.Unity.CapsuleHand>().draw;
        bool ghost = false;

        bool fromLeap = this.transform.parent.GetComponent<HandPositionManager>().from_leap;
        if (!fromLeap && this.name.Equals("tracker"))
        {
            this.transform.localEulerAngles = new Vector3(270, transform.localEulerAngles.y, transform.localEulerAngles.z);
        }
        // start by turning everything off
        this.GetComponent<MeshRenderer>().enabled = false;
        try
        {
            // see if this is a ghost case!
            if (partname.Substring(0, 5).Equals("ghost"))
            {
                ghost = true;
                partname = partname.Substring(5);
            }
        } catch (System.ArgumentOutOfRangeException e)
        {
            //lol
        }
        if (partname.Equals(this.name))
        {
            this.GetComponent<MeshRenderer>().enabled = true;
            if (ghost)
            {

            }
        }
       

    }
}
