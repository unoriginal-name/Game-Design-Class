using UnityEngine;
using System.Collections;

public class PlayerCharacter : FSprite {
	
	public PlayerCharacter() : base("hhhh")
	{
		ListenForUpdate(HandleUpdate);
		
		scale = 0.15f;
		
		y = -Futile.screen.halfHeight + height/2.0f + 50.0f;
		x = -Futile.screen.halfWidth + width/2.0f + 150.0f;
	}
	
	void HandleUpdate () {
	}
}
