using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpriteAnimationController : MonoBehaviour {
	
	// animation played at start
	public SpriteAnimation idle_animation;
	
	// available animations
	public List<SpriteAnimation> animations;
	
	// queue of animations to play
	private Queue<int> animation_queue;
	
	// Use this for initialization
	void Start () {
		
		// if nothing was added in the editor
		if(animations == null)
			animations = new List<SpriteAnimation>();
		
		idle_animation.Init();
		foreach(SpriteAnimation animation in animations)
			animation.Init();
		
		// initialize the queue
		animation_queue = new Queue<int>();
		
		StartCoroutine(PlayAnimation());
	}
	
	IEnumerator PlayAnimation() {
		SpriteAnimation curr_animation = idle_animation;
		while(true)
		{
			// if there is a new animation in the queue
			if(animation_queue.Count > 0)
			{
				int animation_index = animation_queue.Dequeue(); // use it
				
				if(animation_index > animations.Count)
				{
					// out of bounds
					Debug.LogError("Animation index " + animation_index + " is greater than the number of animations specified");
					continue;
				}
				
				curr_animation = animations[animation_index];
			}
			else
				curr_animation = idle_animation; // otherwise play the idle animation
			
			if(curr_animation.num_frames <= 0)
			{
				Debug.LogError("Sprite animation with invalid number of frames: " + curr_animation.num_frames);
				yield return null;
				continue;
			}
			
			// set the size of this texture
			Vector2 size = new Vector2(1f/idle_animation.cols, 1f/idle_animation.rows);
			renderer.sharedMaterial.SetTextureScale("_MainTex", size);
			
			// go through each frame in the sequence
			for(int frame = 0; frame < curr_animation.num_frames; frame++)
			{
				// get the sprite index for this frame
				int sprite_index = curr_animation.sequence[frame];
				
				// calculate the UV offset
				Vector2 offset = new Vector2((float)sprite_index/curr_animation.cols - (sprite_index/curr_animation.cols), // x
					(1-(float)1/curr_animation.rows) - (sprite_index/curr_animation.cols)/(float)curr_animation.rows);
				renderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
				
				// wait for the next frame
				yield return new WaitForSeconds(1f/curr_animation.default_fps);
			}
		}
	}
}
