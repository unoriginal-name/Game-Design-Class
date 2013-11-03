using UnityEngine;
using System.Collections;

public class PlayerCharacter : FSprite {
	
	public const int MAX_HEALTH = 100;
	private float speed_;
	
	private int health = MAX_HEALTH;
	
	public PlayerCharacter() : base("hhhh")
	{
		ListenForUpdate(HandleUpdate);
		
		scale = 0.15f;
		
		speed_ = 0.5f;
		
		y = -Futile.screen.halfHeight + height/2.0f + 50.0f;
		x = -Futile.screen.halfWidth + width/2.0f + 150.0f;
	}
	
	// changes the health by the specified delta
	// to remove health make the delta negative
	public void ChangeHealth(int health_delta)
	{
		health += health_delta;	
	}
	
	public int Health
	{
		get { return this.health; }	
	}
	
	public float Speed
	{
		get {return speed_;}
		set {speed_ = value; }
	}
	
	void HandleUpdate () {
	}
}
