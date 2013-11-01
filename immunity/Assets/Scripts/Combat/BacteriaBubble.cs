using UnityEngine;
using System.Collections;

public class BacteriaBubble : FSprite {
	
	private float _speedY;
		
	public BacteriaBubble() : base("punchyswarm_idle1") {
		scale = 0.5f;
		ListenForUpdate(HandleUpdate);
	}
	
	public void HandleUpdate () {
		_speedY -= 0.013f;
		
		this.y += _speedY;
	}
}
