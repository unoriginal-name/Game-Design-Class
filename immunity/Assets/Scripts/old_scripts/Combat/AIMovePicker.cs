using UnityEngine;
using System.Collections;

public class AIMovePicker : MonoBehaviour {

	public CombatRules combat_rules;
	private PlaySpriteAnimation sprite_animator;

	
	// Use this for initialization
	void Start () {
		sprite_animator = (PlaySpriteAnimation)GetComponent("PlaySpriteAnimation");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void TimesUp()
	{
		int move = Random.Range(0,3); // pick a random move from the ones available
		sprite_animator.ChangeAnimation(move);
		
		combat_rules.SubmitMove(this.name, move);
	}

	
	void TimerPaused()
	{
	}
	
	void TimerUnpaused()
	{
	}
}
