using UnityEngine;
using System.Collections;

public class StomachBubble : FAnimatedSprite {
	
	bool finished_;
	int finished_count_;

	public StomachBubble() : base("Bubble")
	{
		scale = RXRandom.Range(0.25f, 1.0f);
		
		finished_ = false;
		finished_count_ = 0;
		
		ListenForUpdate(HandleUpdate);
		
		int[] bubble_frames = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15};
		int[] ripple_frames = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13};
		
		FAnimation bubble_animation = new FAnimation("bubble", "Bubble", bubble_frames, 100, false);
		FAnimation ripple_animation = new FAnimation("ripple", "Ripple", ripple_frames, 100, false);
		
		base.addAnimation(bubble_animation);
		base.addAnimation(ripple_animation);
		
		base.play("bubble");
	}
	
	// Update is called once per frame
	public void HandleUpdate () {
		base.Update();
		if(base.FinishedCount >= 1)
			finished_count_++;
		
		switch(finished_count_)
		{
		case 0:
			base.play("bubble");
			break;
		case 1:
			base.play("ripple");
			break;
		default:
			finished_ = true;
			isVisible = false;
			break;
		}
	}
	
	public bool Finished {
		get { return finished_; }	
	}
}
