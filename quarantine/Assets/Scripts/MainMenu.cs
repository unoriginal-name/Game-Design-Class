using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void OnGUI() {
		if(GUI.Button (new Rect(20, 20, Screen.width-40, (Screen.height-40)/3), "Pat's Demo"))
		{
			Application.LoadLevel ("TestScene");	
		}
		
		if(GUI.Button (new Rect(20, 20 + (Screen.height-40)/3, Screen.width-40, (Screen.height-40)/3), "Rachid's Demo"))
		{
			// load Rachid's demo here
		}
		
		if(GUI.Button (new Rect(20, 20+(Screen.height-40)*2/3, Screen.width-40, (Screen.height-40)/3), "Devin's Demo"))
		{
			// load Devin's demo here
			Application.LoadLevel("native_sockets_demo");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
		{
			Application.LoadLevel("TestScene");	
		}
	}
}
