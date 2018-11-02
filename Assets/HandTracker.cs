using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class HandTracker : MonoBehaviour {

    Dictionary<string, Drawable> left_hand = new Dictionary<string, Drawable>();
    Dictionary<string, Drawable> right_hand = new Dictionary<string, Drawable>();

    public Material _sphere_material;
    public Material _cyl_material;
    

    public static Mesh _sphereMesh = null;
    public Mesh m;

    [System.Serializable]
    public class Hand
    {
        [System.NonSerialized]
        public Material _sphere_material;
        [System.NonSerialized]
        public Material _cyl_material;

        public List<Sphere> spheres = new List<Sphere>();
        public List<Cylinder> cylinders = new List<Cylinder>();

        public void Draw(Matrix4x4 trs)
        {
            foreach (Sphere s in spheres)
            {
                s.material = _sphere_material;
                s.Draw(trs);
            }
            foreach (Cylinder c in cylinders)
            {
                c.material = _cyl_material;
                c.Draw(trs);
            }
        }
    }

    [System.Serializable]
    public class Drawable : System.Object {
        public virtual void Draw(Matrix4x4 trs) {
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

        public override void Draw(Matrix4x4 trs) {
            if (_sphereMesh != null) {
                Graphics.DrawMesh(_sphereMesh,
                            trs * Matrix4x4.TRS(position,
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

        [System.NonSerialized]
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

        public override void Draw(Matrix4x4 trs) {
            Graphics.DrawMesh(Leap.Unity.CapsuleHand.getCylinderMeshStatic(this.length),
                        trs * Matrix4x4.TRS(this.position_a,
                                      Quaternion.LookRotation(this.position_b - this.position_a),
                                      new Vector3(this.lossy_x, this.lossy_x, 1)),
                        this.material,
                        this.layer,
                        null, 0, null, true);
        }
    }

    private void SaveHand(Leap.Unity.Chirality c)
    {   
        Dictionary<string, Drawable> save_dict; 
         if (c == Leap.Unity.Chirality.Left)
        {
            save_dict = left_hand;
        } else
        {
            save_dict = right_hand;
        }

        Hand h = new Hand();
        Vector3 offset_pos = (save_dict["palm"] as Sphere).position;
        foreach (KeyValuePair<string, Drawable> kvp in save_dict)
        {   
            if (kvp.Value is Sphere)
            {
                Sphere s = kvp.Value as Sphere;
                s.position -= offset_pos;
                h.spheres.Add(s);
            } else
            {
                Cylinder cyl = kvp.Value as Cylinder;
                cyl.position_a -= offset_pos;
                cyl.position_b -= offset_pos;
                h.cylinders.Add(cyl);
            }

        }
        string json_hand = JsonUtility.ToJson(h, true);

        string sceneDataFileName = "hand_" + System.DateTime.UtcNow.Millisecond.ToString() + ".json";
        string filePath = Path.Combine(Application.dataPath, sceneDataFileName);
        Debug.Log(sceneDataFileName);
        File.WriteAllText(filePath, json_hand);
    }

    public void SetSphereMesh(Mesh sphereMesh) {
        _sphereMesh = sphereMesh;
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
        _sphereMesh = m;
    }
	
	// Update is called once per frame
	void Update () {
    }

    public static Hand LoadHand(string filePath)
    {
        string dataAsJson = File.ReadAllText(filePath);
        return JsonUtility.FromJson<Hand>(dataAsJson);
    }

    void LateUpdate() {
        if (Input.GetKeyDown(KeyCode.G))
        {
            SaveHand(Leap.Unity.Chirality.Left);
            Debug.Log("Yay!! Saved left hand.");
        } else if (Input.GetKeyDown(KeyCode.H)) 
        {
            SaveHand(Leap.Unity.Chirality.Right);
            Debug.Log("Yay!! Saved right hand.");
        }
        // Check if the data should be saved, if so - write out the hand somehow
        // TODO: Somehow save the dictionary of joit positions so we can 
        // draw it again later. I've hacked the class so that we sort this out, but it's going to be tricky to 
        // get the details right.
    }
}
