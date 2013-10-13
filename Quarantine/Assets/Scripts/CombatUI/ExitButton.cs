using UnityEngine;
using System.Collections;

public class ExitButton : MonoBehaviour {
	
	public GUIStyle exit_btn_style;
	
	private ScreenRatio screen_ratio;
	
	public float left = 120;
	public float top = 630;
	public float width = 70;
	public float height = 40;
	
	public string level_name = "MainMenu";
	
	private bool game_over = false;
		
	void Start () {
		screen_ratio = (ScreenRatio)GameObject.Find ("Main Camera").GetComponent("ScreenRatio");
	}
	
	void OnGUI() {
		
		if(game_over)
		{
			if(GUI.Button (new Rect(Screen.width*.25f, Screen.height*.125f, Screen.width*.5f, Screen.height*.25f), "", exit_btn_style))
			{
				Application.LoadLevel(level_name);	
			}
		}
		else
		{
		
			if(GUI.Button ( new Rect(left*screen_ratio.horiz, top*screen_ratio.vert, width*screen_ratio.horiz, height*screen_ratio.vert), "", exit_btn_style))
			{
				Application.LoadLevel(level_name);	
			}
		}
		
		if(Input.GetKeyDown(KeyCode.Escape))
			Application.LoadLevel (level_name);
	}
	
	void GameOver(string name) {
		game_over = true;	
	}
}