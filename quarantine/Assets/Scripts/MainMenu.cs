using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	
	public GUIStyle friendButtonStyle;
	private Rect friendButtonRect;
	public GUIStyle messagesButtonStyle;
	public GUIStyle regularButtonStyle;
	
	// Use this for initialization
	void Start () {
		friendButtonRect =  new Rect(20,20, 1000*friendButtonStyle.normal.background.texelSize.x + 20, 1000*friendButtonStyle.normal.background.texelSize.y + 20);
	}
	
	void OnGUI() {
		/*
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
			Application.LoadLevel("WebRequester");
		}*/
		if(GUI.Button (friendButtonRect, "", friendButtonStyle))
		{
			Debug.Log ("friends button pressed");
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
