using UnityEngine;
using System.Collections;

public class PlayerCharacter : FSprite {
	
	private float speed_;
	
	public PlayerCharacter() : base("hhhh")
	{
		ListenForUpdate(HandleUpdate);
		
		scale = 0.15f;
		
		speed_ = 0.5f;
		
		y = -Futile.screen.halfHeight + height/2.0f + 50.0f;
		x = -Futile.screen.halfWidth + width/2.0f + 150.0f;
	}
	
	public float Speed
	{
		get {return speed_;}
		set {speed_ = value; }
	}
	
	void HandleUpdate () {
	}
}
