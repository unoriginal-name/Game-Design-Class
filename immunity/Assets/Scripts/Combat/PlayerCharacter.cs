using UnityEngine;
using System.Collections;

public class PlayerCharacter : FSprite {
	
	public const int MAX_HEALTH = 100;
	private float speed_;
	
	private int health_ = MAX_HEALTH;
	
	private HealthBar health_bar_;
		
	public PlayerCharacter(HealthBar health_bar) : base("hhhh")
	{
		ListenForUpdate(HandleUpdate);
		
		this.health_bar_ = health_bar;
				
		scale = 0.15f;
		
		speed_ = 0.5f;
		
		y = -Futile.screen.halfHeight + height/2.0f + 50.0f;
		x = -Futile.screen.halfWidth + width/2.0f + 150.0f;
	}
	
	// changes the health by the specified delta
	// to remove health make the delta negative
	public void ChangeHealth(int health_delta)
	{
		health_ += health_delta;
		health_bar_.Percentage = (float)health_/(float)MAX_HEALTH;
	}
	
	public int Health
	{
		get { return this.health_; }	
	}
	
	public float Speed
	{
		get {return speed_;}
		set {speed_ = value; }
	}
	
	public bool isDead
	{
		get { return (health_ <= 0); }	
	}
	
	void HandleUpdate () {
	}
}
