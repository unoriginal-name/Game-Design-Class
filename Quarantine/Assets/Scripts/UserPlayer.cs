using UnityEngine;
using System.Collections;

public class UserPlayer : Player {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public override void TurnUpdate()
	{
		TurnManager.instance.nextTurn();
		base.TurnUpdate();
	}
}
