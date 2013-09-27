using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	private float horizRatio;
	private float vertRatio;
		
	public Vector2 top_button_sizes;
	public Vector2 top_button_offsets;
	
	public GUIStyle friendButtonStyle;
	private Rect friendButtonRect;
	public GUIStyle messagesButtonStyle;
	private Rect messagesButtonRect;
	
	public Texture topBarTexture;
	private Rect topBarRect;
	
	public Vector2 btmBtnSizes;
	public Vector2 btmBtnOffsets;
	
	public GUIStyle creatureBtnStyle;
	private Rect creatureBtnRect;
	public GUIStyle inventoryBtnStyle;
	private Rect inventoryBtnRect;
	public GUIStyle playersBtnStyle;
	private Rect playersBtnRect;
	public GUIStyle attacksBtnStyle;
	private Rect attacksBtnRect;
	
	public Texture btmBarTexture;
	private Rect btmBarRect;
	
	public GUIStyle regularBtnStyle;
	
	private Rect registerBtnRect;
	private Rect entryRect;
	private Rect instrRect;
	
	private string entryText = "username";
	
	// Use this for initialization
	void Start () {
		horizRatio = GlobalScreenResolution.SharedInstance.widthRatio;
		vertRatio = GlobalScreenResolution.SharedInstance.heightRatio;
		
		Debug.Log ("horizRatio: " + horizRatio);
		Debug.Log ("vertRatio: " + vertRatio);
		
		topBarRect = new Rect(0, 0, Screen.width, (2*top_button_offsets.y + top_button_sizes.y)*vertRatio);

		friendButtonRect = new Rect(top_button_offsets.x*horizRatio, 
			top_button_offsets.y*vertRatio, 
			top_button_sizes.x*horizRatio, 
			top_button_sizes.y*vertRatio);
		
		messagesButtonRect = new Rect(friendButtonRect.xMax +  (top_button_offsets.x*horizRatio), 
			top_button_offsets.y*vertRatio, 
			top_button_sizes.x*horizRatio, 
			top_button_sizes.y*vertRatio);
		
		btmBarRect = new Rect(0, Screen.height-(2*btmBtnOffsets.y + btmBtnSizes.y)*vertRatio, Screen.width, (2*btmBtnOffsets.y + btmBtnSizes.y)*vertRatio);
		
		float btmBtnTop = (btmBtnOffsets.y + btmBtnSizes.y)*vertRatio;
		creatureBtnRect = new Rect(btmBtnOffsets.x*horizRatio, Screen.height - btmBtnTop, btmBtnSizes.x*horizRatio, btmBtnTop);
		inventoryBtnRect = new Rect(creatureBtnRect.xMax + btmBtnOffsets.x*horizRatio, Screen.height - btmBtnTop, btmBtnSizes.x*horizRatio, btmBtnTop);
		playersBtnRect = new Rect(inventoryBtnRect.xMax + btmBtnOffsets.x*horizRatio, Screen.height - btmBtnTop, btmBtnSizes.x*horizRatio, btmBtnTop);
		attacksBtnRect = new Rect(playersBtnRect.xMax + btmBtnOffsets.x*horizRatio, Screen.height - btmBtnTop, btmBtnSizes.x*horizRatio, btmBtnTop);
		
		Vector2 labelSizes = new Vector2(350, 50);
		instrRect = new Rect((Screen.width - labelSizes.x*horizRatio)/2,
			(Screen.height - labelSizes.y*vertRatio)/2 - labelSizes.y*vertRatio,
			labelSizes.x*horizRatio,
			labelSizes.y*vertRatio);
		
		Vector2 entrySizes = new Vector2(300, 45);
		entryRect = new Rect((Screen.width - entrySizes.x*horizRatio)/2,
			(Screen.height - entrySizes.y*vertRatio)/2,
			entrySizes.x*horizRatio,
			entrySizes.y*vertRatio);
		
		Vector2 registerBtnSizes = new Vector2(300, 60);
		registerBtnRect = new Rect((Screen.width - registerBtnSizes.x*horizRatio)/2,
			(Screen.height - registerBtnSizes.y*vertRatio)/2 + registerBtnSizes.y*vertRatio, 
			registerBtnSizes.x*horizRatio, 
			registerBtnSizes.y*vertRatio);
	}
	
	void OnGUI() {
		GUI.DrawTexture(topBarRect, topBarTexture);
		GUI.DrawTexture(btmBarRect, btmBarTexture);
		
		// if the user doesn't exist
		// only display the user creation gui
		if(!PlayerPrefs.HasKey ("username"))
		{
			GUI.Label(instrRect, "Please enter your username");
			entryText = GUI.TextField(entryRect, entryText);
			if(GUI.Button (registerBtnRect, "Register", regularBtnStyle))
			{
				Debug.Log ("register button pressed: " + entryText);
			}
			
			
		} else {
			if(GUI.Button (friendButtonRect, "", friendButtonStyle))
			{
				Debug.Log ("friends button pressed");
			}
			if(GUI.Button (messagesButtonRect, "", messagesButtonStyle))
			{
				Debug.Log ("messages button pressed");
			}
			if(GUI.Button (creatureBtnRect, "", creatureBtnStyle))
			{
				Debug.Log ("creature button pressed");
			}
			if(GUI.Button (inventoryBtnRect, "", inventoryBtnStyle))
			{
				Debug.Log ("inventory button pressed");
			}
			if(GUI.Button (playersBtnRect, "", playersBtnStyle))
			{
				Debug.Log ("players button pressed");
			}
			if(GUI.Button (attacksBtnRect, "", attacksBtnStyle))
			{
				Debug.Log ("attacks button pressed");
			}
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
