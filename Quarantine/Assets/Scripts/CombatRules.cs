using UnityEngine;
using System.Collections;

public class CombatRules : MonoBehaviour {
	
	public CombatTimer timer;
	
	float WAIT_TIME = 1;
	float wait_start = 0;
	
	bool waiting = false;
	
	// Use this for initialization
	void Start () {
		timer.StartTimer(10.0f);
	}
	
	// Update is called once per frame
	void Update () {
		if(!waiting)
			return;
		
		if(Time.time - wait_start > WAIT_TIME)
		{
			waiting = false;
			timer.StartTimer(10.0f);
		}
	}
	
	public void SubmitMove(string name, int move) {
		// this is where health changes will be calculated
		// and turn types will be calculated.
	}
	
	void TimesUp() {
		waiting = true;
		wait_start = Time.time;
		
	}
	
	void TimerPaused() {
		
	}
	
	void TimerUnpaused() {
		
	}
}
