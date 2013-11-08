using UnityEngine;
using System.Collections;

public class BacteriaBubble : FAnimatedSprite {
	
	private float speed_ = 5.0f;
	
	float spawn_time_;
	
	private Vector2 initial_direction_;
	
	private enum BacteriaPhase
	{
		LAUNCH,
		TRACK_PLAYER
	}
	
	private BacteriaPhase curr_phase_;
		
	public BacteriaBubble(Vector2 location, Vector2 initial_direction) : base("punchyswarm_idle") {
		scale = RXRandom.Range(0.25f, 0.75f);
		ListenForUpdate(HandleUpdate);
		
		initial_direction_ = initial_direction;
		
		SetPosition(location);
		
		int[] idle_frames = {1, 2};
		int[] pop_frames = {1, 2, 3, 4};
		
		FAnimation idle_animation = new FAnimation("punchyswarm_idle", idle_frames, 250, true);
		FAnimation pop_animation = new FAnimation("punchyswarm_pop", pop_frames, 250, false);
		
		base.addAnimation(idle_animation);
		base.addAnimation(pop_animation);
		
		spawn_time_ = Time.time;
	}
	
	public void HandleUpdate () {
		base.Update();
		
		if(curr_phase_ == BacteriaPhase.LAUNCH)
		{
			this.x += speed_*initial_direction_.x;
			this.y += speed_*initial_direction_.y;
			
			// if we've spawned over 1 second ago
			if(Time.time - spawn_time_ > 1.0f)
				curr_phase_ = BacteriaPhase.TRACK_PLAYER;
		}
		else
		{
			CombatPage game = ImmunityCombatManager.instance.GamePage as CombatPage;
			Vector2 direction = game.Player.GetPosition() - this.GetPosition();
			
			float magnitude = Mathf.Sqrt(direction.x*direction.x + direction.y*direction.y);
			
			direction.x /= magnitude;
			direction.y /= magnitude;
			
			// TODO: Give the bacteria some inertia so that accelerate and overshoot
			this.x += speed_*direction.x;
			this.y += speed_*direction.y;
		}
		
	}
}
