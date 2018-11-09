using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

public class CSVLogger : MonoBehaviour {

	StreamWriter sw = null;
	public string file_prefix = "hand_log_";
	private static readonly DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

	public static long CurrentTimeMillis() {
	    return (long) (DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
	}
	// Use this for initialization
	void Start () {
        string folderPath = Path.Combine(Application.dataPath, "Logs");
        string filePath = Path.Combine(folderPath, file_prefix + CurrentTimeMillis().ToString() + ".csv");
		sw = new StreamWriter(filePath);
        Debug.Log("Started Logging at:" + filePath);
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void Log(string msg) {
		sw.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff") + "," + msg);
	}

	void OnApplicationQuit() {
		sw.Close();
	}
}
