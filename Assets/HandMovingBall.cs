using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMovingBall : MovingBall {

    public HandTracker.Hand h;
    public Material material = null;
    public Material ghost_material;

    private static readonly Vector3[] cup_rotations = {new Vector3(42.2f, 22.5f, 104.43f), new Vector3(-42.2f, -22.5f, -104.43f)};
    private static readonly Vector3[] cont_rotations = { new Vector3(14.3f, 41.2f, 163.8f), new Vector3(6.6f, -27.7f, -187.4f)};
    private static readonly Vector3[] book_rotations = { new Vector3(36.4f, -9.9f, 79.78f), new Vector3(6.19f, 8.32f, -78.39f)};

    public Vector3 vive_rotations;
    public Vector3 leap_rotations;


    public HandMovingBall(HandTracker.Hand h)
    {
        this.h = h;
    }

    void Start()
    {
        StartCoroutine(LateStart(1.1f));
    }

    void Awake()
    {
    }
    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if (!update_table)
        {
            bool fromLeap = this.transform.parent.GetComponent<HandPositionManager>().from_leap;
            if (fromLeap)
            {
                this.gameObject.transform.localEulerAngles = leap_rotations;
            }
            else
            {
                this.gameObject.transform.localEulerAngles = vive_rotations;
            }

        }
    }

    // Update is called once per frame
    protected override void Update () {
        string partname = this.transform.parent.GetComponent<Leap.Unity.CapsuleHand>().draw;
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
        }
        if (material != null)
        {
            h._cyl_material = material;
            h._sphere_material = material;
            if (ghost)
            {
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
