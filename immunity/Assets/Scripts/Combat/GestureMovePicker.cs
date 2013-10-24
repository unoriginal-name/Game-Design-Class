using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Gestures))]
public class GestureMovePicker : MonoBehaviour {
	
	private Gestures gestures;
	private float last_move_time = 0;
	
	public CombatRules combat_rules;
	private PlaySpriteAnimation sprite_animator;
	
	public Stamina stamina;
	
	// Use this for initialization
	void Start () {
		gestures = (Gestures)GetComponent("Gestures");
		sprite_animator = (PlaySpriteAnimation)GetComponent("PlaySpriteAnimation");
	}
	
	// Update is called once per frame
	void Update () {
	
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
			
			if(last_move == Gestures.gesture.right)
			{
				if(stamina.stamina < 30)
				{
					combat_rules.SubmitMove(this.name, 0);	
				}
				combat_rules.networkView.RPC ("SubmitMove", RPCMode.All, this.name, 1);
				sprite_animator.networkView.RPC("ChangeAnimation", RPCMode.All, 1);
				stamina.ChangeStamina(-30);
			} else if(last_move == Gestures.gesture.left) {
				if(stamina.stamina < 10)
				{
					combat_rules.SubmitMove(this.name, 0);	
				}
				combat_rules.networkView.RPC ("SubmitMove", RPCMode.All, this.name, 2);
				sprite_animator.networkView.RPC("ChangeAnimation", RPCMode.All, 2);
				stamina.ChangeStamina(-10);
			} else {
				combat_rules.networkView.RPC ("SubmitMove", RPCMode.All, this.name, 0);
			}			
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
