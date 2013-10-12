using UnityEngine;
using System.Collections;

public class SettingsButton : MonoBehaviour {
	
	public GUIStyle btn_style;
	
	private ScreenRatio screen_ratio;
	
	public float left = 120;
	public float top = 630;
	public float width = 70;
	public float height = 40;
	
	public CombatTimer timer;
		
	void Start () {
		screen_ratio = (ScreenRatio)GameObject.Find ("Main Camera").GetComponent("ScreenRatio");
	}
	
	void OnGUI() {
		if(GUI.Button ( new Rect(left*screen_ratio.horiz, top*screen_ratio.vert, width*screen_ratio.horiz, height*screen_ratio.vert), "", btn_style))
		{
			Debug.Log ("Settings Button button pressed");
			timer.PauseTimer();
			
			// do real stuff here
		}
	}
}