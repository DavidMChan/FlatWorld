using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMovingBall : MovingBall {

    public HandTracker.Hand h;
    public Material material = null;
    public Material ghost_material;
    public HandMovingBall(HandTracker.Hand h)
    {
        this.h = h;
    }

    void Awake()
    {
        
    }
	
	// Update is called once per frame
	protected override void Update () {
        string partname = this.transform.parent.GetComponent<Leap.Unity.CapsuleHand>().draw;
        Debug.Log(partname);
        bool ghost = false;
        try
        {
            // see if this is a ghost case!
            if (partname.Substring(0, 5).Equals("ghost"))
            {
                ghost = true;
            }
        }
        catch (System.ArgumentOutOfRangeException e)
        {
            //lol
        }
        if (material != null)
        {
            h._cyl_material = material;
            h._sphere_material = material;
            if (ghost)
            {
                Debug.Log("dis is a ghost lol");
                h._sphere_material = ghost_material;
                h._cyl_material = ghost_material;

            }
        }

        base.Update();
        Matrix4x4 trs = Matrix4x4.TRS(this.gameObject.transform.position, Quaternion.Euler(this.gameObject.transform.eulerAngles), this.gameObject.transform.localScale);

        if (this.GetComponent<MeshRenderer>().enabled)
        {
            h.Draw(trs);
        }
       
	}
}
