using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTracker : MonoBehaviour {

    Dictionary<string, Drawable> left_hand;
    Dictionary<string, Drawable> right_hand;

    public Material _sphere_material;
    public Material _cyl_material;

    private Mesh _sphereMesh = null;

    [System.Serializable]
    public class Drawable : System.Object {
        public virtual void Draw() {
            return;
        }
    }

    [System.Serializable]
    public class Sphere : Drawable {
        public Vector3 position;
        public float radius;

        [System.NonSerialized]
        public Material material;
        
        public float lossy_x;
        public Sphere(Vector3 p, float r, Material mat, float lx) {
            this.position = p;
            this.radius = r;
            this.material = mat;
            this.lossy_x = lx;
        }

        public override void Draw() {
            if (_sphereMesh != null) {
                Graphics.DrawMesh(_sphereMesh,
                            Matrix4x4.TRS(position,
                                          Quaternion.identity,
                                          Vector3.one * radius * 2.0f * this.lossy_x),
                            this.material, 0,
                            null, 0, null, true);
            }
        }
    }

    [System.Serializable]
    public class Cylinder : Drawable {
        public Vector3 position_a;
        public Vector3 position_b;
        public float length;
        public Material material;
        public float lossy_x;
        public float lossy_y;
        public int layer;
        public Cylinder(Vector3 p, Vector3 q, float length, Material mat, float lx, float ly, int l) {
            this.position_a = p;
            this.position_b = q;
            this.length = length;
            this.material = mat;
            this.lossy_x = lx;
            this.lossy_y = ly;
            this.layer = l;
        }

        public override void Draw() {
            Graphics.DrawMesh(Leap.Unity.CapsuleHand.getCylinderMesh(this.length),
                        Matrix4x4.TRS(this.position_a,
                                      Quaternion.LookRotation(this.position_b - this.position_a),
                                      new Vector3(this.lossy_x, this.lossy_x, 1)),
                        this.material,
                        this.layer,
                        null, 0, null, true);
        }
    }

    public void SetSphereMesh(Mesh sphereMesh) {
        this._sphereMesh = sphereMesh;
    }


    public void RegisterSphere(string key, Vector3 position, float radius, Leap.Unity.Chirality c, float lossy_x, Mesh m) {
        if (c == Leap.Unity.Chirality.Left) {
            // Add to the left hand
            left_hand[key] = new Sphere(position, radius, _sphere_material, lossy_x);
        } else {
            // Add to the right hand
            right_hand[key] = new Sphere(position, radius, _sphere_material, lossy_x);
        }
    }
    public void RegisterCylinder(string key, Vector3 pos_a, Vector3 pos_b, Leap.Unity.Chirality c, int layer, float lossy_x, float lossy_y, float length) {
        if (c == Leap.Unity.Chirality.Left) {
            // Add to the left hand
            left_hand[key] = new Cylinder(pos_a, pos_b, length, _cyl_material, lossy_x, lossy_y, layer);
        } else {
            // Add to the right hand
            right_hand[key] = new Cylinder(pos_a, pos_b, length, _cyl_material, lossy_x, lossy_y, layer);
        }
    }

	// Use this for initialization
	void Start () {
        left_hand = new Dictionary<string, Drawable>();
        right_hand = new Dictionary<string, Drawable>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void LateUpdate() {

        // Check if the data should be saved, if so - write out the hand somehow
        // TODO: Somehow save the dictionary of joit positions so we can 
        // draw it again later. I've hacked the class so that we sort this out, but it's going to be tricky to 
        // get the details right.
    }
}
