using UnityEngine;
using System.Collections;

public class RegistrationMenu : MonoBehaviour {
	
	float horizRatio;
	float vertRatio;
	
	bool loading = false;
	
	public GUIStyle regularBtnStyle;
	
	private Rect registerBtnRect;
	private Rect entryRect;
	private Rect instrRect;
	
	private string entryText = "username";
	
	private MainMenu main_menu;
	
	// Use this for initialization
	void Start () {
		horizRatio = GlobalScreenResolution.SharedInstance.widthRatio;
		vertRatio = GlobalScreenResolution.SharedInstance.heightRatio;
		
		main_menu = (MainMenu)FindObjectOfType(typeof(MainMenu));
		regularBtnStyle = main_menu.regularBtnStyle;
		
		Vector2 labelSizes = new Vector2(500, 50);
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
		if(!PlayerPrefs.HasKey("username"))
		{
			if(!loading) {
				GUI.Label(instrRect, "Please enter your username", main_menu.centeredTextStyle);
				entryText = GUI.TextField(entryRect, entryText);
				if(GUI.Button (registerBtnRect, "Register", regularBtnStyle))
				{	
					Debug.Log ("register button pressed: " + entryText);
					loading = true;
				}
			} else {
				GUI.Label (instrRect, "Creating account...");
				var server = new Webserver("immunitygame390.appspot.com");
				var result = server.registerUser (entryText);
				
				if(result == null)
					Debug.LogError ("ERROR: registerUser failed");
				
				PlayerPrefs.SetString("username", entryText);
				
				Destroy (this);
			}
		}
	}
}
