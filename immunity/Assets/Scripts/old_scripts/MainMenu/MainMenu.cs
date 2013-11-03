using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
	public GUIStyle centeredTextStyle;
	
	public Rect contextMenuRect;
	
	public bool loading = false;
	
	public List<GameObject> menu_objects;
	private GameObject current_menu;
		
	void Awake() {
		// Debugging purposes
		//PlayerPrefs.DeleteAll();	
	}
	
	// Use this for initialization
	void Start () {
        Screen.orientation = ScreenOrientation.Portrait;

		current_menu = null;
		
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
		
		contextMenuRect = new Rect(0, topBarRect.yMax, Screen.width, btmBarRect.yMin - topBarRect.yMax);
	}
	
	void OnGUI() {
		GUI.DrawTexture(topBarRect, topBarTexture);
		GUI.DrawTexture(btmBarRect, btmBarTexture);
		
		// if the user doesn't exist
		// only display the user creation gui
		if(PlayerPrefs.HasKey ("username")) {
			//GUI.Label (instrRect, "Welcome back " + PlayerPrefs.GetString("username"));
			if(GUI.Button (friendButtonRect, "", friendButtonStyle))
			{
				Debug.Log ("friends button pressed");
				
				switchMenus(0);
			}
			if(GUI.Button (messagesButtonRect, "", messagesButtonStyle))
			{
				Debug.Log ("messages button pressed");
				switchMenus(1);
			}
			if(GUI.Button (creatureBtnRect, "", creatureBtnStyle))
			{
				Debug.Log ("creature button pressed");
				Application.LoadLevel ("SpriteTest");
			}
			if(GUI.Button (inventoryBtnRect, "", inventoryBtnStyle))
			{
				Debug.Log ("inventory button pressed");
			}
			if(GUI.Button (playersBtnRect, "", playersBtnStyle))
			{
				Debug.Log ("players button pressed");
				Application.LoadLevel("SinglePlayer");
			}
			if(GUI.Button (attacksBtnRect, "", attacksBtnStyle))
			{
				Debug.Log ("attacks button pressed");
                //Application.LoadLevel("GestureDemo");
                switchMenus(2);
			}
		}
		
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void switchMenus(int menu_index) {
		if(current_menu != null)
			current_menu.SetActive(false);
		
		current_menu = menu_objects[menu_index];
		current_menu.SetActive(true);
	}
}
