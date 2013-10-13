using UnityEngine;
using System.Collections;

public class Stamina : MonoBehaviour {
	
	public const int MAX_STAMINA = 100;
	public float stamina = 0;
	public float recharge_rate = 1; // per second
	
	public int left, top, width, height;
	
	private ScreenRatio screen_ratio;
	
	public GUIStyle text_style;
	
	private bool paused;
	
	// Use this for initialization
	void Start () {
		screen_ratio = (ScreenRatio)GameObject.Find ("Main Camera").GetComponent("ScreenRatio");
	}
	
	// this method will add the amount specified
	// to the stamina. (Use a negative # to subtract)
	public void ChangeStamina(int amount)
	{
		stamina += amount;
		if(stamina < 0)
			stamina = 0;
	}
	
	public int GetStamina()
	{
		return  (int)stamina;	
	}
	
	// Update is called once per frame
	void Update () {
		if(paused)
			return;
		
		stamina += recharge_rate*Time.deltaTime;
		
		if(stamina > MAX_STAMINA)
			stamina = MAX_STAMINA;
	}
	
	void OnGUI() {
		GUI.Label(new Rect(left*screen_ratio.horiz, top*screen_ratio.vert, width*screen_ratio.horiz, height*screen_ratio.vert), "" + (int)stamina, text_style);
	}
	
	void TimerPaused()
	{
		paused = true;	
	}
	
	void TimerUnpaused()
	{
		paused = false;
	}
}
