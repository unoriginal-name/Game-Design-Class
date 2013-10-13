using UnityEngine;
using System.Collections;

public class CombatRules : MonoBehaviour {
	
	public CombatTimer timer;
	
	float WAIT_TIME = 1;
	float wait_start = 0;
	bool waiting = false;
	
	public float MAX_TURN_TIME = 10;
	public float MIN_TURN_TIME = .5f;
	float turn_time;
		
	private int player_move = -1;
	private int enemy_move = -1;
	
	public Character player;
	public Character enemy;
	
	// Use this for initialization
	void Start () {
		turn_time = MAX_TURN_TIME;
		timer.StartTimer(turn_time);
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
			if(player_move == 1)
			{
				if(enemy_move == 0) // player attacked and enemy did nothing
				{
					enemy.ChangeHealth(-10);
				}
				else if(enemy_move == 1) // player attacked and so did enemy
				{
					player.ChangeHealth(-10);
					enemy.ChangeHealth(-10);	
				} // if enemy blocked do nothing
			}
			else if(player_move == 0)
			{
				if(enemy_move == 1) // player did nothing and enemy attacked
					player.ChangeHealth(-10);
			}
			
			// check if any player has died
			if(((Character)player.GetComponent("Character")).GetHealth() <= 0)
			{
				Debug.Log ("Player has died");
			}
			
			if(((Character)enemy.GetComponent("Character")).GetHealth() <= 0)
			{
				Debug.Log ("Enemy has died");	
			}
			
			// reset the moves
			player_move = -1;
			enemy_move = -1;
			
			
			turn_time -= .1f*(MAX_TURN_TIME - MIN_TURN_TIME);
			if(turn_time < MIN_TURN_TIME)
				turn_time = MIN_TURN_TIME;
			timer.StartTimer(turn_time);
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
