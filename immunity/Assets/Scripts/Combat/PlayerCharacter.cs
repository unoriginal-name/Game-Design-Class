using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlayerCharacter : FAnimatedSprite {
	
	public enum PlayerState { IDLE, HIT, BLOCK, PUNCH, WALK };
	
	public const int MAX_HEALTH = 100;
	private float speed_;
	
	private int health_ = MAX_HEALTH;
	
	private HealthBar health_bar_;
	
	protected PlayerState current_state_;
		
	//protected Vector2 centroid_ = new Vector2(.806/8.333, 1.778/6.250);
	
	public PlayerCharacter(HealthBar health_bar) : base("huro_idle")
	{
		ListenForUpdate(HandleUpdate);
		
		this.health_bar_ = health_bar;
		
		this.scaleX = .75f;
		this.scaleY = .75f;
		
		this.anchorX = .243f;
		this.anchorY = .4f;

		// idle animation
		int[] idle_frames = {1, 2, 3, 4};
		FAnimation idle_animation = new FAnimation("idle", "huro_idle", idle_frames, 150, true);
		base.addAnimation(idle_animation);
		
		// block animation
		int[] block_frames = {1, 2, 3, 4, 5, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 7, 8, 9};
		FAnimation block_animation = new FAnimation("block", "huro_block", block_frames, 100, false);
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
		
		// death animation
		int[] death_frames = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12};
		FAnimation death_animation = new FAnimation("death", "huro_death", death_frames, 150, false);
		base.addAnimation(death_animation);
		
		base.setDefaultAnimation("idle");
		
		speed_ = 0.5f;
		
		y = -Futile.screen.halfHeight + height/2.0f + 25.0f;
		x = -Futile.screen.halfWidth + width/2.0f + 150.0f;
	}
	
	// changes the health by the specified delta
	// to remove health make the delta negative
	public void ChangeHealth(int health_delta)
	{
		health_ += health_delta;
		if(health_ > MAX_HEALTH)
			health_ = MAX_HEALTH;
		if(health_ < 0)
			health_ = 0;
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
	
	public float width 
	{
		get { return base.width*.33f; }	
	}
	
	public float height
	{
		get { return base.height*.613f; }	
	}
	
	public PlayerState CurrentState
	{
		get { return current_state_; }
		set { current_state_ = value; }
	}
	
	public List<Rect> getCollisionRects()
	{
		List<Rect> rects = new List<Rect>();
		if(current_state_ == PlayerState.PUNCH)
			rects.Add(localRect.CloneAndScaleThenOffset(scaleX, scaleY, x, y));
		else
			rects.Add(localRect.CloneAndScaleThenOffset(scaleX*.75f, scaleY*.75f, x, y));
		
		return rects;
	}
	
	void HandleUpdate () {
		base.Update();
	}

}
