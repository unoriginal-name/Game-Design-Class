using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {
	
	public Texture health_bar_image;

	private ScreenRatio screen_ratio;
	
	public float left = 120;
	public float top = 470;
	public float width = 400;
	public float height = 400;
		
	void Start () {
		screen_ratio = (ScreenRatio)GameObject.Find ("Main Camera").GetComponent("ScreenRatio");
	}
	
	void OnGUI() {
		GUI.DrawTexture(new Rect(left*screen_ratio.horiz, top*screen_ratio.vert, width*screen_ratio.horiz, height*screen_ratio.vert), health_bar_image);
	}
}
