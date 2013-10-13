using UnityEngine;
using System.Collections;

public class CombatRules : MonoBehaviour {
	
	public CombatTimer timer;
	
	// Use this for initialization
	void Start () {
		timer.StartTimer(10.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void SubmitMove(string name, int move) {
		// this is where health changes will be calculated
		// and turn types will be calculated.
	}
	
	void TimesUp() {
		// for now just restart the timer
		timer.StartTimer(10.0f);	
	}
	
	void TimerPaused() {
		
	}
	
	void TimerUnpaused() {
		
	}
}
