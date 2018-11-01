using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMovingBall : MovingBall {

    public HandTracker.Hand h;
    public Material material = null;
    public HandMovingBall(HandTracker.Hand h)
    {
        this.h = h;
    }

    void Awake()
    {
        if (material != null)
        {
            h._cyl_material = material;
            h._sphere_material = material;
        }
    }
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
        Matrix4x4 trs = Matrix4x4.TRS(this.gameObject.transform.position, Quaternion.Euler(this.gameObject.transform.eulerAngles), this.gameObject.transform.localScale);

        if (this.GetComponent<MeshRenderer>().enabled)
        {
            h.Draw(trs);
        }
       
	}
}
