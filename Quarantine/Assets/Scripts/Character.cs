using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character : MonoBehaviour {
	
	public const int MAX_HEALTH = 100;
	private int current_health;
	
	public Stamina stamina;
	public HealthBar health_bar;
	
	private Gestures gestures;
	private float last_move_time = 0;
	
	public CombatRules combat_rules;
	
	private PlaySpriteAnimation sprite_animator;

	// Use this for initialization
	void Start () {
		current_health = MAX_HEALTH;
		gestures = (Gestures)GetComponent("Gestures");
		sprite_animator = (PlaySpriteAnimation)GetComponent("PlaySpriteAnimation");
	}
	
	// Update is called once per frame
	void Update () {
		// shouldn't have to do anything
	}
	
	void TimesUp()
	{
		// get the latest gesture
		Gestures.gesture last_move = gestures.LastGesture;
		
		// check the timestamp to make sure this is a new move
		if(gestures.LastTimeStamp <= last_move_time)
			combat_rules.SubmitMove(this.name, 0); // submit a do nothing move
		else
		{
			last_move_time = gestures.LastTimeStamp;
			combat_rules.SubmitMove(this.name, (int)last_move);
			
			sprite_animator.ChangeAnimation(1);
		}
	}
	
	void TimerPaused()
	{
		if(gestures != null)
			gestures.enabled = false;	
	}
	
	void TimerUnpaused()
	{
		if(gestures != null)
			gestures.enabled = true;
	}
}
