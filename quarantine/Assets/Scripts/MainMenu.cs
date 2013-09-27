using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	private float horizRatio;
	private float vertRatio;
	
	public GUIStyle friendButtonStyle;
	public GUIStyle messagesButtonStyle;
	public GUIStyle regularButtonStyle;
	
	public Vector2 top_button_sizes;
	public Vector2 top_button_offsets;
	private Rect friendButtonRect;
	private Rect messagesButtonRect;
	
	public Texture topBarTexture;
	public Texture btmBarTexture;
	private Rect topBarRect;
	private Rect btmBarRect;
	
	// Use this for initialization
	void Start () {
		horizRatio = GlobalScreenResolution.SharedInstance.widthRatio;
		vertRatio = GlobalScreenResolution.SharedInstance.heightRatio;
		
		Debug.Log ("horizRatio: " + horizRatio);
		Debug.Log ("vertRatio: " + vertRatio);
		
		friendButtonRect = new Rect(top_button_offsets.x*horizRatio, 
			top_button_offsets.y*vertRatio, 
			top_button_sizes.x*horizRatio, 
			top_button_sizes.y*vertRatio);
		
		messagesButtonRect = new Rect(friendButtonRect.xMax +  (top_button_offsets.x*horizRatio), 
			top_button_offsets.y*vertRatio, 
			top_button_sizes.x*horizRatio, 
			top_button_sizes.y*vertRatio);
		
		topBarRect = new Rect(0, 0, Screen.width, (2*top_button_offsets.y + top_button_sizes.y)*vertRatio);
	}
	
	void OnGUI() {
		GUI.DrawTexture(topBarRect, topBarTexture);
		
		if(GUI.Button (friendButtonRect, "", friendButtonStyle))
		{
			Debug.Log ("friends button pressed");
		}
		if(GUI.Button (messagesButtonRect, "", messagesButtonStyle))
		{
			Debug.Log ("messages button pressed");
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
