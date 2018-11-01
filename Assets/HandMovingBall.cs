using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMovingBall : MovingBall {

    public HandTracker.Hand h;
    public HandMovingBall(HandTracker.Hand h)
    {
        this.h = h;
    }
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
        Matrix4x4 trs = Matrix4x4.TRS(this.gameObject.transform.position, Quaternion.Euler(this.gameObject.transform.eulerAngles), this.gameObject.transform.localScale);
        h.Draw(trs);
	}
}
