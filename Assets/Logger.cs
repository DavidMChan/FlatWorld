using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logger : MonoBehaviour {

	StreamWriter sw = null;
	public string file_prefix = "hand_log";
	private static readonly DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

	public static long CurrentTimeMillis() {
	    return (long) (DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
	}
	// Use this for initialization
	void Start () {
        string filePath = Path.Combine(Application.dataPath, file_prefix + CurrentTimeMillis().ToString());
		sw = new StreamWriter(filePath);
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void Log(string msg) {
		sw.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "," + msg);
	}

	void OnApplicationQuit() {
		sw.Close();
	}
}
