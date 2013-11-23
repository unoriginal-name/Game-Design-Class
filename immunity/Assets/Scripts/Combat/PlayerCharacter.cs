using UnityEngine;
using System.Collections;

public class PlayerCharacter : FAnimatedSprite {
	
	public const int MAX_HEALTH = 100;
	private float speed_;
	
	private int health_ = MAX_HEALTH;
	
	private HealthBar health_bar_;
		
	public PlayerCharacter(HealthBar health_bar) : base("huro_idle")
	{
		ListenForUpdate(HandleUpdate);
		
		this.health_bar_ = health_bar;
		
		this.scaleX = .75f;
		this.scaleY = .75f;

		// idle animation
		int[] idle_frames = {1, 2, 3, 4};
		FAnimation idle_animation = new FAnimation("idle", "huro_idle", idle_frames, 150, true);
		base.addAnimation(idle_animation);
		
		// block animation
		int[] block_frames = {1, 2, 3, 4, 5, 6, 7, 8, 9};
		FAnimation block_animation = new FAnimation("block", "huro_block", block_frames, 150, false);
		base.addAnimation(block_animation);
		
		// punch animation
		int[] punch_frames = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12};
		FAnimation punch_animation = new FAnimation("punch", "huro_punch", punch_frames, 150, false);
		base.addAnimation(punch_animation);
		
		// hit animation
		int[] hit_frames = {1, 2, 3, 4, 5, 6};
		FAnimation hit_animation = new FAnimation("hit", "huro_hit", hit_frames, 150, false);
		base.addAnimation(hit_animation);
		
		// walk animation
		int[] walk_frames = {1, 2, 3, 4};
		FAnimation walk_animation = new FAnimation("walk", "huro_walk", walk_frames, 150, true);
		base.addAnimation(walk_animation);
		
		// backwards walk animation
		int[] backwards_walk_frames = {4, 3, 2, 1};
		FAnimation backwards_walk_animation = new FAnimation("backwards_walk", "huro_walk", backwards_walk_frames, 150, true);
		base.addAnimation(backwards_walk_animation);
		
		base.setDefaultAnimation("idle");
		
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
		base.Update();
	}

}
