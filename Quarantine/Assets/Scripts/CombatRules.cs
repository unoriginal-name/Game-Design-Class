using UnityEngine;
using System.Collections;

public class CombatRules : MonoBehaviour {
	
	public CombatTimer timer;
	
	float WAIT_TIME = 1;
	float wait_start = 0;
	bool waiting = false;
		
	private int player_move = -1;
	private int enemy_move = -1;
	
	// Use this for initialization
	void Start () {
		timer.StartTimer(10.0f);
	}
	
	// Update is called once per frame
	void Update () {	
		if(player_move == -1 || enemy_move == -1)
			return; // still waiting for at least one character to submit a move
		else
		{	
			// start wait timer
			if(!waiting)
			{
				waiting = true;
				wait_start = Time.time;
			}
		}
		
		if(Time.time - wait_start > WAIT_TIME)
		{
			// stop waiting
			waiting = false;
			
			// update the players
			
			// reset the moves
			player_move = -1;
			enemy_move = -1;
			
			timer.StartTimer(10.0f);
		}
	}
	
	public void SubmitMove(string name, int move) {
		// this is where health changes will be calculated
		// and turn types will be calculated.
		
		if(name.Equals("Player"))
			player_move = move;
		else if(name.Equals ("Enemy"))
			enemy_move = move;
		else
			Debug.LogError("Unknown player: " + name);		
	}
	
	void TimesUp() {

	}
	
	void TimerPaused() {
		
	}
	
	void TimerUnpaused() {
		
	}
}
