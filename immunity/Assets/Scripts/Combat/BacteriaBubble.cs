using UnityEngine;
using System.Collections;

public class BacteriaBubble : FAnimatedSprite {
	
	public static float accel = Futile.screen.halfHeight*0.5f;
	
	private Vector2 velocity_;
			
	public BacteriaBubble(Vector2 location, Vector2 initial_velocity) : base("punchyswarm_idle") {
		scale = RXRandom.Range(0.25f, 0.75f);
		ListenForUpdate(HandleUpdate);
		
		velocity_ = initial_velocity;
		
		SetPosition(location);
		
		int[] idle_frames = {1, 2};
		int[] pop_frames = {1, 2, 3, 4};
		
		FAnimation idle_animation = new FAnimation("idle", "punchyswarm_idle", idle_frames, 100, true);
		FAnimation pop_animation = new FAnimation("pop", "punchyswarm_pop", pop_frames, 100, false);
		
		base.addAnimation(idle_animation);
		base.addAnimation(pop_animation);
	}
	
	public void HandleUpdate () {
		base.Update();
		
		this.x = this.x + velocity_.x*Time.deltaTime;
		this.y = this.y + velocity_.y*Time.deltaTime;
		velocity_.y = velocity_.y - accel*Time.deltaTime;
		
	}
}
