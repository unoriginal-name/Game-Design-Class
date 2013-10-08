using UnityEngine;
using System.Collections;

public class WebRequesterMenu : MonoBehaviour {
	
	public GUIStyle MyStyle;
	
	public static string hostname = "www.google.com";
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnGUI () {
		GUI.Label(new Rect(20, 20, Screen.width-40, 100), "Enter a hostname to send an HTTP Get Request", MyStyle);
		hostname = GUI.TextField(new Rect(20, 120, Screen.width-40, 50), hostname);
		
		if(GUI.Button (new Rect(20, 220, Screen.width-40, 100), "Go!"))
		{
			// load level here
			Application.LoadLevel("native_sockets_demo");
		}
	}
}
