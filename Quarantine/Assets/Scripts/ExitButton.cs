using UnityEngine;
using System.Collections;

public class ExitButton : MonoBehaviour {
	
	public GUIStyle exit_btn_style;
	
	private ScreenRatio screen_ratio;
	
	public float left = 120;
	public float top = 630;
	public float width = 70;
	public float height = 40;
		
	void Start () {
		screen_ratio = (ScreenRatio)GameObject.Find ("Main Camera").GetComponent("ScreenRatio");
	}
	
	void OnGUI() {
		if(GUI.Button ( new Rect(left*screen_ratio.horiz, top*screen_ratio.vert, width*screen_ratio.horiz, height*screen_ratio.vert), "", exit_btn_style))
		{
			Debug.Log ("Exit button pressed");	
		}
	}
}