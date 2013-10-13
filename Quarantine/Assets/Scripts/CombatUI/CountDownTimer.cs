using UnityEngine;
using System.Collections;
using System;

public class CountDownTimer : MonoBehaviour {
	
	public float left, top, width, height;
	
	private ScreenRatio screen_ratio;
	
	public GUIStyle text_style;
	
	public CombatTimer timer;
	
	// Use this for initialization
	void Start () {
		screen_ratio = (ScreenRatio)GameObject.Find ("Main Camera").GetComponent("ScreenRatio");
	}
	
	void OnGUI() {
		float time_left = timer.GetTimeLeft();
		if(time_left > 0)
			GUI.Label (new Rect(left*screen_ratio.horiz, top*screen_ratio.vert, width*screen_ratio.horiz, height*screen_ratio.vert), String.Format("{0:F2}", time_left), text_style);
		else
			GUI.Label(new Rect(left*screen_ratio.horiz, top*screen_ratio.vert, width*screen_ratio.horiz, height*screen_ratio.vert), "Times Up!", text_style);
	}
}
