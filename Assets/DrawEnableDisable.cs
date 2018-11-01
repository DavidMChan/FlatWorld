using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawEnableDisable : MonoBehaviour {
    public string name;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        bool shouldDraw = this.transform.parent.GetComponent<Leap.Unity.CapsuleHand>().draw.Equals(this.name);
        this.GetComponent<MeshRenderer>().enabled = shouldDraw;
    }
}
