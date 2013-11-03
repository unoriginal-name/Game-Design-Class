﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class CombatPage : ImmunityPage, FMultiTouchableInterface {
	
	private FSprite levelFore_;
	private FSprite levelMid_;
	private FSprite levelBack_;
	
	private int totalBacterialCreated_ = 0;
	private FContainer bacteriaContainer_;
	private List<BacteriaBubble> bacterias_ = new List<BacteriaBubble>();
	
	private FContainer dyingBacteriaHolder_;
	private List<BacteriaBubble> dyingBacterias_ = new List<BacteriaBubble>();
	
	private FLabel scoreLabel_;
	
	private int frameCount_ = 0;
	private int maxFramesTillNextBacteria_ = 22;
	private int framesTillNextBacteria_ = 0;
	
	private PlayerCharacter player_;
	private HealthBar player_healthbar;
		
	private Tween current_movement = null;
	
	public CombatPage()
	{
		EnableMultiTouch();
		ListenForUpdate(HandleUpdate);
	}
	// Use this for initialization
	override public void Start () {
	
		levelBack_ = new FSprite("Stomach_Lake");
		levelMid_ = new FSprite("Stomach_Mid");
		levelFore_ = new FSprite("Stomach_Fore");
		AddChild(levelBack_);
		AddChild(levelMid_);
		AddChild(levelFore_);
		
		player_healthbar = new HealthBar();
		player_healthbar.x = Futile.screen.halfWidth - player_healthbar.width - 20.0f;
		player_healthbar.y = Futile.screen.halfHeight - player_healthbar.height/2.0f - 20.0f;
		player_ = new PlayerCharacter(player_healthbar);
		AddChild(player_);
		
		bacteriaContainer_ = new FContainer();
		AddChild(bacteriaContainer_);
		
		dyingBacteriaHolder_ = new FContainer();
		AddChild(dyingBacteriaHolder_);
		
		ImmunityCombatManager.instance.score = 0;
		
		scoreLabel_ = new FLabel("ImmunityFont", "0 Bacteria");
		scoreLabel_.anchorX = 0.0f;
		scoreLabel_.anchorY = 1.0f;
		scoreLabel_.x = -Futile.screen.halfWidth + 50.0f;
		scoreLabel_.y = Futile.screen.halfHeight - 50.0f;
		scoreLabel_.color = Color.white;
		AddChild(scoreLabel_);
		
		AddChild(player_healthbar);
	}
	
	public void HandleGotBacteria(BacteriaBubble bacteria)
	{
		
		bacteriaContainer_.RemoveChild(bacteria);
		bacterias_.Remove(bacteria);
		
		dyingBacteriaHolder_.AddChild(bacteria);
		dyingBacterias_.Add(bacteria);
		bacteria.play("punchyswarm_pop");		

		FSoundManager.PlaySound("bacteria_pop");
		
		ImmunityCombatManager.instance.score++;
		
		if(ImmunityCombatManager.instance.score == 1)
		{
			scoreLabel_.text = "1 Bacteria";
		}
		else
		{
			scoreLabel_.text = ImmunityCombatManager.instance.score+" Bacterias";	
		}
	}
	
	public void CreateBacteria()
	{
		BacteriaBubble bacteria = new BacteriaBubble();
		bacteriaContainer_.AddChild(bacteria);
		bacteria.x = RXRandom.Range(-Futile.screen.width/2 + 50, Futile.screen.width/2-50); // padded inside the screen width
		bacteria.y = Futile.screen.height/2 + 60; // above the screen
		bacteria.play("punchyswarm_idle");
		bacterias_.Add(bacteria);
		totalBacterialCreated_++;
	}
	
	protected void HandleUpdate()
	{
		framesTillNextBacteria_--;
		
		if(framesTillNextBacteria_ <= 0)
		{
			framesTillNextBacteria_ = maxFramesTillNextBacteria_;
			
			CreateBacteria();
		}
		
		for(int b = bacterias_.Count-1; b >= 0; b--)
		{
			BacteriaBubble bacteria = bacterias_[b];
			
			// remove a bacteria if it falls off screen
			if(bacteria.y < -Futile.screen.halfHeight - 50)
			{
				bacterias_.Remove(bacteria);
				bacteriaContainer_.RemoveChild(bacteria);
			}
		}
		
		// check if player was hit
		bool bacteriaHit = false;
		Rect playerRect = player_.localRect.CloneAndScaleThenOffset(Math.Abs(player_.scaleX), player_.scaleY, player_.x, player_.y);
		for(int b = bacterias_.Count-1; b >= 0; b--)
		{
			BacteriaBubble bacteria = bacterias_[b];
			Rect bacteriaRect = bacteria.localRect.CloneAndScaleThenOffset(Math.Abs(bacteria.scaleX), bacteria.scaleY, bacteria.x, bacteria.y);
			
			if(playerRect.CheckIntersect(bacteriaRect))
			{
				bacteriaHit = true;
				HandleGotBacteria(bacteria);
			}
		}
		if(bacteriaHit)
		{
			Debug.Log("Shaking!");
			player_.ChangeHealth((int)(-PlayerCharacter.MAX_HEALTH*0.1f));
			FSoundManager.PlaySound("player_hit");
			ImmunityCombatManager.instance.camera.shake(100.0f, 0.25f);
			
			if(player_.isDead)
			{
				Debug.Log("Game Over!");
				Application.LoadLevel("ImmunityMainMenu");
			}
		}
		
		
		for(int b = dyingBacterias_.Count-1; b >= 0; b--)
		{
			BacteriaBubble bacteria = dyingBacterias_[b];
			
			if(bacteria.isFinished)
			{
				dyingBacterias_.Remove(bacteria);
				dyingBacteriaHolder_.RemoveChild(bacteria);
			}
		}
		
		frameCount_++;
		
	}
	
	public void HandleMultiTouch(FTouch[] touches)
	{
		foreach(FTouch touch in touches)
		{
			if(touch.phase == TouchPhase.Began)
			{
				bool touchedEmptySpace = true;
				// go in reverse order so if bacteria is removed it doesn't matter
				// also checks sprites in front to back order
				for(int b = bacterias_.Count-1; b >= 0; b--)
				{
					BacteriaBubble bacteria = bacterias_[b];
					
					Vector2 touchPos = bacteria.GlobalToLocal(touch.position);
					
					if(bacteria.textureRect.Contains(touchPos))
					{
						HandleGotBacteria(bacteria);
						touchedEmptySpace = false;
						break; // a touch can only hit one bacteria at a time
					}
				}
				
				if(touchedEmptySpace && touch.position.y < -Futile.screen.halfHeight/2.0f)
				{
					// if already executing a move, first stop it
					if(current_movement != null)
					{
						current_movement.destroy();
					}
										
					// flip the player if the movement is behind the player
					if(touch.position.x - player_.x < 0)
						player_.scaleX = -1*Math.Abs(player_.scaleX);
					else
						player_.scaleX = Math.Abs(player_.scaleX);
					
					// calculate movement time based on player's speed attribute
					float tween_time = Math.Abs(player_.x - touch.position.x)/(Futile.screen.width*player_.Speed);
					
					current_movement = Go.to(player_, tween_time, new TweenConfig().floatProp("x", touch.position.x));
				}
			}
		}
	}
}