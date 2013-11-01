using UnityEngine;
using System.Collections;

public class BacteriaBubble : FSprite {
	
	private float speedY_;
		
	public BacteriaBubble() : base("punchyswarm_idle_1") {
		scale = RXRandom.Range(0.25f, 0.75f);
		ListenForUpdate(HandleUpdate);
	}
	
	public void HandleUpdate () {
		speedY_ -= 0.013f;
		
		this.y += speedY_;
	}
}
