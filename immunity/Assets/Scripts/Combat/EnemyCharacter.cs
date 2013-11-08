using UnityEngine;
using System.Collections;

public class EnemyCharacter : FAnimatedSprite {
	
	public EnemyCharacter() : base("punchy_idle")
	{
		ListenForUpdate(HandleUpdate);
		
		// set up animations
		// -------------------
		
		// idle animation
		int[] idle_frames = {1, 2, 3, 4};
		FAnimation idle_animation = new FAnimation("punchy_idle", idle_frames, 150, true);
		base.addAnimation(idle_animation);
		
		// punch animation
		int[] punch_frames = {1, 2, 3, 4, 5, 6, 7};
		FAnimation punch_animation = new FAnimation("punchy_punch", punch_frames, 100, true);
		base.addAnimation(punch_animation);
				
		y = -Futile.screen.halfHeight + height/2.0f + 50.0f;
		x = Futile.screen.halfWidth - width/2.0f - 150.0f;
	}

	void HandleUpdate()
	{
		base.Update();	
	}
}
