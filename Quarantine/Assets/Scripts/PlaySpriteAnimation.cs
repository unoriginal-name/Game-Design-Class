using UnityEngine;
using System.Collections;

public class PlaySpriteAnimation : MonoBehaviour {
	public int idle_animation = 0;
	
	private int curr_animation = 0;
	
	private float animation_start = 0;
		
	void Awake() {
		BroadcastMessage("PrePlay");
		BroadcastMessage("PlayAnimation", idle_animation);
	}
	
	void TimerPaused()
	{
		BroadcastMessage("PrePlay");
		BroadcastMessage("PlayAnimation", -1);
	}
	
	void TimerUnpaused()
	{
		BroadcastMessage("PrePlay");
		BroadcastMessage("PlayAnimation", curr_animation);
	}
	
/*	public void PlayAnimation(int animation)
	{
		curr_animation = animation;
		BroadcastMessage("PrePlay");
		BroadcastMessage("PlayAnimation", animation);
		
		animation_start = Time.time;
	}*/
	
	public void ChangeAnimation(int animation)
	{
		
	}
	
	public void Update()
	{
		if(curr_animation != idle_animation)
		{
			if(Time.time - animation_start > 1)
			{
				BroadcastMessage("PrePlay");
				BroadcastMessage("PlayAnimation", animation);
			}
		}
	}
}
