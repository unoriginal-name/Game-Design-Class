using UnityEngine;
using System.Collections;

public class BacteriaBubble : FAnimatedSprite {
	
	private float speedY_;
		
	public BacteriaBubble() : base("punchyswarm_idle") {
		scale = RXRandom.Range(0.25f, 0.75f);
		ListenForUpdate(HandleUpdate);
		
		int[] idle_frames = {1, 2};
		int[] pop_frames = {1, 2, 3, 4};
		
		FAnimation idle_animation = new FAnimation("punchyswarm_idle", idle_frames, 250, true);
		FAnimation pop_animation = new FAnimation("punchyswarm_pop", pop_frames, 250, false);
		
		base.addAnimation(idle_animation);
		base.addAnimation(pop_animation);		
	}
	
	public void HandleUpdate () {
		base.Update();
		
		speedY_ -= 0.013f;
		
		this.y += speedY_;
	}
}
