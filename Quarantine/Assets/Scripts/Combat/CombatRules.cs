using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
	
	enum TurnMode { ATTACK, DEFEND };
	TurnMode current_turn_mode;
	
	bool game_over = false;
	bool player_won = false;
	
	public GUIStyle game_over_background;
	
	public List<GameObject> objects;
	
	public List<AudioClip> sounds;
	
	// Use this for initialization
	void Start () {
		current_turn_mode = TurnMode.ATTACK;
		turn_time = MAX_TURN_TIME;
		timer.StartTimer(turn_time);
	}
	
	// Update is called once per frame
	void Update () {
		if(Network.isClient)
		{
			return;
		}
		
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
					if(current_turn_mode == TurnMode.ATTACK)
						enemy.networkView.RPC ("ChangeHealth", RPCMode.All, -20);
					else
						enemy.networkView.RPC ("ChangeHealth", RPCMode.All, -10);
				}
				else if(enemy_move == 1) // player attacked and so did enemy
				{
					if(current_turn_mode == TurnMode.ATTACK)
					{
						player.networkView.RPC ("ChangeHealth", RPCMode.All, -10);
						enemy.networkView.RPC ("ChangeHealth", RPCMode.All, -20);
					} else {
						player.networkView.RPC ("ChangeHealth", RPCMode.All, -20);
						enemy.networkView.RPC ("ChangeHealth", RPCMode.All, -10);
					}
					
				} // do nothing
			}
			else if(player_move == 0)
			{
				if(enemy_move == 1) // player did nothing and enemy attacked
				{
					if(current_turn_mode == TurnMode.ATTACK)
						player.networkView.RPC ("ChangeHealth", RPCMode.All, -10);
					else
						player.networkView.RPC ("ChangeHealth", RPCMode.All, -20);
				}
			}
			
			// decide what sound to play
			if(player_move == 0)
			{
				if(enemy_move == 0)
				{
					// no sound
				} else if(enemy_move == 1) {
					// punch and grunt
					audio.PlayOneShot(sounds[1]);
					audio.PlayOneShot(sounds[0]);
				} else {
					// block
					audio.PlayOneShot(sounds[2]);
				}
			} else if(player_move == 1) {
				if(enemy_move == 0)
				{
					// punch and grunt
					audio.PlayOneShot(sounds[1]);
					audio.PlayOneShot(sounds[0]);
				} else if(enemy_move == 1) {
					// punch and block
					audio.PlayOneShot (sounds[1]);
					audio.PlayOneShot (sounds[0]);
				} else {
					// punch and block
					audio.PlayOneShot (sounds[1]);
					audio.PlayOneShot (sounds[2]);
				}
			} else {
				if(enemy_move == 0)
				{
					// block
					audio.PlayOneShot(sounds[2]);
				} else if(enemy_move == 1) {
					// punch and block
					audio.PlayOneShot(sounds[1]);
					audio.PlayOneShot(sounds[2]);
				} else {
					// block
					audio.PlayOneShot (sounds[2]);
				}
			}
					
			// reset the moves
			player_move = -1;
			enemy_move = -1;
			
			// check if any player has died
			if(((Character)player.GetComponent("Character")).GetHealth() <= 0)
			{
				Debug.Log ("Player has died");
				timer.PauseTimer();
				
				// display lose dialog here
				game_over = true;
				player_won = false;
				
				foreach(GameObject obj in objects)
				{
					obj.BroadcastMessage("GameOver", "Player");	
				}
			}
			
			/*if(((Character)enemy.GetComponent("Character")).GetHealth() <= 0)
			{
				Debug.Log ("Enemy has died");
				timer.PauseTimer ();
				
				// display win dialog here
				game_over = true;
				player_won = true;
				
				foreach(GameObject obj in objects)
				{
					obj.BroadcastMessage("GameOver", "Enemy");	
				}
			}*/
			
			turn_time -= .1f*(MAX_TURN_TIME - MIN_TURN_TIME);
			if(turn_time < MIN_TURN_TIME)
				turn_time = MIN_TURN_TIME;
			timer.networkView.RPC ("StartTimer", RPCMode.All, turn_time);
			
		}
	}
	
	[RPC]
	public void SubmitMove(string name, int move) {
		// this is where health changes will be calculated
		// and turn types will be calculated.
		
		if(name.Equals("Player") || name.Equals ("Player(Clone)"))
			player_move = move;
		else if(name.Equals ("Enemy") || name.Equals ("Enemy(Clone)"))
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
	
	void OnGUI() {
		if(!game_over) // don't display anything until the game has finished
			return;
		string message = "";
		if(player_won)
			message = "Congratulations you defeated the Thnaaake!";
		else
			message = "Oh no! You were defeated by Thnaaake :(";
		
		GUI.Label(new Rect(0, 0, Screen.width, Screen.height), message, game_over_background);
	}
	
	void OnNetworkInstantiate(NetworkMessageInfo info)
	{
		Debug.Log ("network instantiation: " + info);	
	}
}
