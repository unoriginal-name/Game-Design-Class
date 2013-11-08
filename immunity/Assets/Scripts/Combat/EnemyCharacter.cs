using UnityEngine;
using System.Collections;

public class EnemyCharacter : FAnimatedSprite {
	
	public enum BehaviorType { 
		IDLE,
		MOVE_TOWARDS_PLAYER,
		MOVE_AWAY_FROM_PLAYER,
		PUNCH,
		SPAWN_SWARM
	}
	
	public const int MAX_HEALTH = 100;
	private int health_ = MAX_HEALTH;
	
	private HealthBar health_bar_;
	
	public BehaviorType curr_behavior_ = BehaviorType.IDLE;
	public float behavior_start_time_;
	
	public float speed_ = .001f;
	
	public int NUM_SPAWNED_SWARM = 10;
	public int spawn_count = 0;
		
	public EnemyCharacter(HealthBar health_bar) : base("punchy_idle")
	{
		ListenForUpdate(HandleUpdate);
		
		this.health_bar_ = health_bar;
		
		// set up animations
		// -------------------
		
		// idle animation
		int[] idle_frames = {1, 2, 3, 4};
		FAnimation idle_animation = new FAnimation("punchy_idle", idle_frames, 150, true);
		base.addAnimation(idle_animation);
		
		// punch animation
		int[] punch_frames = {1, 2, 3, 4, 5, 6, 7};
		FAnimation punch_animation = new FAnimation("punchy_punch", punch_frames, 100, false);
		base.addAnimation(punch_animation);
				
		y = -Futile.screen.halfHeight + height/2.0f + 50.0f;
		x = Futile.screen.halfWidth - width/2.0f - 150.0f;
		
	}
	
	// TODO: refactor this into a base class (Character class)
	public void ChangeHealth(int health_delta)
	{
		health_ += health_delta;
		health_bar_.Percentage = (float)health_/(float)MAX_HEALTH;
	}
	
	public int Health
	{
		get { return this.health_; }	
	}
	
	public bool isDead
	{
		get { return (health_ <= 0); }	
	}
	
	void HandleUpdate()
	{
		base.Update();
		
		// if not currently doing anything choose something to do with some probability
		if(curr_behavior_ == BehaviorType.IDLE)
		{
			float behavior_selection = Random.value;
			
			if(behavior_selection < .3f)
			{
				// switch to move_towards_player behavior
				curr_behavior_ = BehaviorType.MOVE_TOWARDS_PLAYER;
				Debug.Log("Behavior: Move towards player");
			}
			else if(behavior_selection < .6f)
			{
				// switch to move_away_from_player behavior
				curr_behavior_ = BehaviorType.MOVE_AWAY_FROM_PLAYER;
				Debug.Log("Behavior: Move away from player");
			}
			else if(behavior_selection < .7f)
			{
				// switch to punch behavior	
				curr_behavior_ = BehaviorType.PUNCH;
				Debug.Log("Behavior: Punch");
			}
			else if(behavior_selection < .9f)
			{
				// switch to swarm behavior	
				curr_behavior_ = BehaviorType.SPAWN_SWARM;
				spawn_count = 0;
				Debug.Log("Behavior: spawn swarm");
			}
			behavior_start_time_ = Time.time;
		}
	}
}
