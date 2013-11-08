﻿using UnityEngine;
using System;
using System.Collections;

public class PlayerBubble : FAnimatedSprite {
	
	float speed_ = .01f;
	
	Vector2 direction_;
	
	// TODO: Add pop animation
	public PlayerBubble(Vector2 direction) : base ("player_cell_swim")
	{
		ListenForUpdate(HandleUpdate);
		
		direction_ = direction;
		
		// turn sprite to the specified direction
		rotation = Mathf.Atan2(direction.x, direction.y);
		
		// set up the swim animation
		int[] swim_frames = {1, 2, 3, 4, 5, 6, 7, 8};
		FAnimation swim_animation = new FAnimation("player_cell_swim", swim_frames, 250, true);
		base.addAnimation(swim_animation);
	}
	
	public void HandleUpdate()
	{
		base.Update(); // this is required for the animation
		
		// update the sprite position based on the direction and the speed
		x += direction_.x*speed_*Futile.screen.width;
		y += direction_.y*speed_*Futile.screen.height;
	}
}