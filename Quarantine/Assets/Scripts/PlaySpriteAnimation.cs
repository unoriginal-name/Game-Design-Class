using UnityEngine;
using System.Collections;

public class PlaySpriteAnimation : MonoBehaviour {
	public int animation = 0;
		
	void Awake() {
		BroadcastMessage("PrePlay");
		BroadcastMessage("PlayAnimation", animation);
	}
	
	void TimerPaused()
	{
		BroadcastMessage("PrePlay");
		BroadcastMessage("PlayAnimation", -1);
	}
	
	void TimerUnpaused()
	{
		BroadcastMessage("PrePlay");
		BroadcastMessage("PlayAnimation", animation);
	}
}
