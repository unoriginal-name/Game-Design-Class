using UnityEngine;
using System.Collections;
using System;

public class CountDownTimer : MonoBehaviour {
	
	public float left, top, width, height;
	
	private ScreenRatio screen_ratio;
	
	public GUIStyle text_style;
	
	public CombatTimer timer;
	
	private bool game_over = false;
	
	// Use this for initialization
	void Start () {
		screen_ratio = (ScreenRatio)GameObject.Find ("Main Camera").GetComponent("ScreenRatio");
	}
	
	void OnGUI() {
		if(game_over)
			return;
		
		float time_left = timer.GetTimeLeft();
		
		// change to yellow when less than 4 seconds left
		// change to red when less than 2 seconds left
		if(time_left < 2)
			text_style.normal.textColor = new Color(1, 0, 0, 1);
		else if(time_left < 4)
			text_style.normal.textColor = new Color(1, 1, 0, 1);
		else
			text_style.normal.textColor = new Color(0, 1, 0, 1);
		
		if(time_left > 0)
			GUI.Label (new Rect(left*screen_ratio.horiz, top*screen_ratio.vert, width*screen_ratio.horiz, height*screen_ratio.vert), String.Format("{0:F2}", time_left), text_style);
		else
		{
			text_style.normal.textColor = new Color(1,1,1,1);
			GUI.Label(new Rect(left*screen_ratio.horiz, top*screen_ratio.vert, width*screen_ratio.horiz, height*screen_ratio.vert), "Times Up!", text_style);
		}
	}
	
	void GameOver(string name)
	{
		game_over = true;	
	}
}
