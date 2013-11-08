using UnityEngine;
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
	
	private FContainer bubbleContainer_;
	private List<PlayerBubble> bubbles_ = new List<PlayerBubble>(); 
		
	private int frameCount_ = 0;
	private int maxFramesTillNextBacteria_ = 22;
	private int framesTillNextBacteria_ = 0;

	private FParallaxContainer background;
	private FParallaxContainer mid;
	private FParallaxContainer foreground;	

	private PlayerCharacter player_;
	private FContainer playerContainer;
	private Vector2 playerPosition;
	public PlayerCharacter Player
	{
		get { return player_; }
	}
	private HealthBar player_healthbar;
	
	private EnemyCharacter enemy_;
	private HealthBar enemy_healthbar_;
		
	private Tween current_movement = null;
	
	Dictionary<int, FTouch> touch_starts = new Dictionary<int, FTouch>();
	
	public CombatPage()
	{
		EnableMultiTouch();
		ListenForUpdate(HandleUpdate);
	}
	// Use this for initialization
	override public void Start () {
		
		Stage stage = new Stage();
		stage.setStomach();
		/*
		background = new FParallaxContainer();
		mid = new FParallaxContainer();
		foreground = new FParallaxContainer();
		
		background.size.Set (Futile.screen.width /2, Futile.screen.height / 2);
		mid.size.Set (Futile.screen.width, Futile.screen.height);
		foreground.size.Set (Futile.screen.width * 2, Futile.screen.height * 2);
	
		levelBack_ = new FSprite("Stomach_Lake");
		levelMid_ = new FSprite("Stomach_Mid");
		levelFore_ = new FSprite("Stomach_Fore");
		
		//levelBack_.scaleX = 1.5f;
		//levelMid_.scaleX = 1.5f;
		//levelFore_.scaleX = 1.5f;
		
		background.AddChild (levelBack_);
		mid.AddChild (levelMid_);
		foreground.AddChild (levelFore_);
		*/

		AddChild(stage.background);
		AddChild(stage.mid);
		AddChild(stage.foreground);

		FSprite enemy_headshot = new FSprite("punchy_headshot");
		enemy_headshot.x = Futile.screen.halfWidth - enemy_headshot.width/2.0f - 50.0f;
		enemy_headshot.y = Futile.screen.halfHeight - enemy_headshot.height/2.0f - 50.0f;
		enemy_healthbar_ = new HealthBar();
		enemy_healthbar_.scaleX = -.8f;
		enemy_healthbar_.x = enemy_headshot.x - enemy_headshot.width/2.0f - 25.0f;
		enemy_healthbar_.y = enemy_headshot.y;
		enemy_ = new EnemyCharacter(enemy_healthbar_);
		AddChild(enemy_);
		
		FSprite player_headshot = new FSprite("hero_headshot");
		player_headshot.scale = .2f;
		player_headshot.x = -Futile.screen.halfWidth + player_headshot.width/2.0f + 50.0f;
		player_headshot.y = Futile.screen.halfHeight - player_headshot.height/2.0f - 50.0f;
		player_healthbar = new HealthBar();
		player_healthbar.scaleX = .8f;
		player_healthbar.x = player_headshot.x + player_headshot.width/2.0f + 25.0f;
		player_healthbar.y = player_headshot.y;
		player_ = new PlayerCharacter(player_healthbar);

		
		playerContainer = new FContainer();
		//Debug.Log ("the player is at " + playerPosition.x + "," + playerPosition.y);
		
		playerContainer.AddChild (player_);
		
		//Debug.Log ("playerContainer at x is " + playerContainer.x);
		//Debug.Log ("playerContainer at y is " + playerContainer.y);
		
		AddChild(playerContainer);
		
		bacteriaContainer_ = new FContainer();
		AddChild(bacteriaContainer_);
		
		bubbleContainer_ = new FContainer();
		AddChild(bubbleContainer_);
		
		dyingBacteriaHolder_ = new FContainer();
		AddChild(dyingBacteriaHolder_);
		
		ImmunityCombatManager.instance.camera.follow(playerContainer);
		AddChild(player_headshot);
		AddChild(player_healthbar);
		AddChild(enemy_headshot);
		AddChild(enemy_healthbar_);
	}
	
	public void HandleGotBacteria(BacteriaBubble bacteria)
	{
		
		bacteriaContainer_.RemoveChild(bacteria);
		bacterias_.Remove(bacteria);
		
		dyingBacteriaHolder_.AddChild(bacteria);
		dyingBacterias_.Add(bacteria);
		
		bacteria.play("punchyswarm_pop");
		
		FSoundManager.PlaySound("bacteria_pop");
	}
	
	public void CreateBacteria(Vector2 location, Vector2 direction)
	{
		BacteriaBubble bacteria = new BacteriaBubble(location, direction);
		bacteriaContainer_.AddChild(bacteria);
		bacteria.play("punchyswarm_idle");
		bacterias_.Add(bacteria);
		totalBacterialCreated_++;
	}
	
	public void CreateBubble(Vector2 direction)
	{
		PlayerBubble bubble = new PlayerBubble(direction);
		bubbleContainer_.AddChild(bubble);
		bubble.x = player_.x;
		bubble.y = player_.y;
		bubble.play("player_cell_swim");
		bubbles_.Add(bubble);
	}
	
	public void MoveTowardsPlayerBehavior()
	{
		// if we've been moving towards the player for more than 2 seconds
		if(Time.time - enemy_.behavior_start_time_ > 2.0f)
		{
			enemy_.curr_behavior_ = EnemyCharacter.BehaviorType.IDLE;
			return;
		}

		// if we're within 50 of the player don't move anymore
		if(enemy_.x - player_.x < 50.0f + enemy_.width/2.0f + player_.width/2.0f)
		{
			enemy_.curr_behavior_ = EnemyCharacter.BehaviorType.IDLE;
			return;
		}
		
		enemy_.x -= enemy_.speed_*Futile.screen.width;
		if(enemy_.x - player_.x < 50.0f + enemy_.width/2.0f + player_.width/2.0f)
			enemy_.x = player_.x + 50.0f + enemy_.width/2.0f + player_.width/2.0f;
	}
	
	public void MoveAwayFromPlayerBehavior()
	{
		// if we've been moving away from the player for more than 2 seconds
		if(Time.time - enemy_.behavior_start_time_ > 2.0f)
		{
			enemy_.curr_behavior_ = EnemyCharacter.BehaviorType.IDLE;
			return;
		}
		
		// if we're within 50 of the border don't move anymore
		if(Futile.screen.halfWidth - enemy_.x < enemy_.width/2.0f)
		{
			enemy_.curr_behavior_ = EnemyCharacter.BehaviorType.IDLE;
			return;
		}
		
		enemy_.x += enemy_.speed_*Futile.screen.width;
		if(Futile.screen.halfWidth - enemy_.x < enemy_.width/2.0f)
			enemy_.x = Futile.screen.halfWidth - enemy_.width/2.0f;
	}
	
	public void PunchBehavior()
	{
		if(enemy_.isFinished)
		{
			enemy_.curr_behavior_ = EnemyCharacter.BehaviorType.IDLE;
			enemy_.play("punchy_idle");
			return;
		}
		
		if(!enemy_.currentAnim.name.Equals("punchy_punch"))
			enemy_.play("punchy_punch");
	}
	
	public void SpawnSwarmBehavior()
	{
		framesTillNextBacteria_--;
		
		if(framesTillNextBacteria_ <= 0)
		{
			float angle = Mathf.PI/(float)enemy_.NUM_SPAWNED_SWARM * (enemy_.NUM_SPAWNED_SWARM - enemy_.spawn_count++);
			Vector2 bacteria_direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
			CreateBacteria(enemy_.GetPosition(), bacteria_direction);
		
			framesTillNextBacteria_ = maxFramesTillNextBacteria_;
		}
		
		if(enemy_.spawn_count >= enemy_.NUM_SPAWNED_SWARM)
		{
			enemy_.curr_behavior_ = EnemyCharacter.BehaviorType.IDLE;
			
			enemy_.spawn_count = 0;
		}
	}
	
	protected void HandleUpdate()
	{
		
		switch(enemy_.curr_behavior_)
		{
		case EnemyCharacter.BehaviorType.MOVE_TOWARDS_PLAYER:
			MoveTowardsPlayerBehavior();
			break;
		case EnemyCharacter.BehaviorType.MOVE_AWAY_FROM_PLAYER:
			MoveAwayFromPlayerBehavior();
			break;
		case EnemyCharacter.BehaviorType.PUNCH:
			PunchBehavior();
			break;
		case EnemyCharacter.BehaviorType.SPAWN_SWARM:
			SpawnSwarmBehavior();
			break;
		default:
			break;
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
		
		for(int b = bubbles_.Count-1; b >= 0; b--)
		{
			PlayerBubble bubble = bubbles_[b];
			
			// remove a bubble if it falls off screen
			if(bubble.y < -Futile.screen.halfHeight - 50 || 
				bubble.y > Futile.screen.halfHeight + 50)
			{
				if(bubble.x < -Futile.screen.halfWidth - 50 ||
					bubble.x > Futile.screen.halfWidth + 50)
				{
					bubbles_.Remove(bubble);
					bubbleContainer_.RemoveChild(bubble);
				}
			}
		}
		
		// check if any of the bubbles have collided with any of the bacteria
		// TODO: Make this algorithm more efficient
		for(int b = bubbles_.Count-1; b >= 0; b--)
		{
			PlayerBubble bubble = bubbles_[b];
			
			Rect bubbleRect = bubble.localRect.CloneAndScaleThenOffset(bubble.scale, bubble.scale, bubble.x, bubble.y);
			for(int j = bacterias_.Count-1; j >= 0; j--)
			{
				BacteriaBubble bacteria = bacterias_[j];
				
				Rect bacteriaRect = bacteria.localRect.CloneAndScaleThenOffset(Math.Abs(bacteria.scaleX), bacteria.scaleY, bacteria.x, bacteria.y);
				
				if(bubbleRect.CheckIntersect(bacteriaRect))
				{
					HandleGotBacteria(bacteria);
					bubbles_.Remove(bubble);
					bubbleContainer_.RemoveChild(bubble);
					break;
				}
			}
			
			Rect enemyRect = enemy_.localRect.CloneAndScaleThenOffset(Mathf.Abs(enemy_.scaleX), enemy_.scaleY, enemy_.x, enemy_.y);
			if(bubbleRect.CheckIntersect(enemyRect))
			{
				enemy_.ChangeHealth((int)(-.01f*EnemyCharacter.MAX_HEALTH));
				// TODO: Play hit sound
				// TODO: Play hit animation
				if(enemy_.isDead)
				{
					// TODO: Show win screen
					Debug.Log("You win!!");
					Application.LoadLevel("ImmunityMainMenu");
				}
				
				bubbles_.Remove(bubble);
				bubbleContainer_.RemoveChild(bubble);
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
				// TODO: Show lose screen
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
				touch_starts.Add(touch.tapCount, touch);	
			}
			else if(touch.phase == TouchPhase.Ended)
			{
				FTouch touch_start = touch_starts[touch.tapCount];
				
				Vector2 swipe_vector = touch.position - touch_start.position;
				
				// normalize the swipe vector
				float swipe_magnitude = Mathf.Sqrt(swipe_vector.x*swipe_vector.x + swipe_vector.y*swipe_vector.y);
				
				// TODO: Change this to a reasonable threshold
				if(swipe_magnitude <= 30.0f)
				{
					// This is a tap
					Debug.Log("Detected a tap");
					
					if(touch.position.y < -Futile.screen.halfHeight/2.0f)
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
				else
				{
					Debug.Log("Detected a swipe");
					swipe_vector.x /= swipe_magnitude;
					swipe_vector.y /=swipe_magnitude;
					
					Debug.Log("Swipe vector: <" + swipe_vector.x + ", " + swipe_vector.y + "> Magnitude: " + swipe_magnitude);
					
					CreateBubble(swipe_vector);
				}
				
				touch_starts.Remove(touch.tapCount);
			}
		}
	}
}
