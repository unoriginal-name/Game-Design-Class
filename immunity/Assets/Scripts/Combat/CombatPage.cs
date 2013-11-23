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
	private bool player_being_punched = false;
	
	private bool player_won_ = false;
	private bool player_lost_ = false;
	
	Dictionary<int, FTouch> touch_starts = new Dictionary<int, FTouch>();
		
	//private Stage stage;
	
	public CombatPage()
	{
		EnableMultiTouch();
		ListenForUpdate(HandleUpdate);
	}
	// Use this for initialization
	override public void Start () {
		

		/*stage = new Stage();
		Debug.Log("calling setstomach");
		//stage.setStomach();
		Debug.Log("Finished calling setstomach");*/
		
		/*
		background.AddChild (levelBack_);
		mid.AddChild (levelMid_);
		foreground.AddChild (levelFore_);
		*/
		
		FSoundManager.StopMusic();
		FSoundManager.UnloadAllSoundsAndMusic();
		FSoundManager.PlayMusic("stomach_ambience");

		addComponentsToStage();

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

		
		AddChild(playerContainer);
		
		bacteriaContainer_ = new FContainer();
		AddChild(bacteriaContainer_);
		
		bubbleContainer_ = new FContainer();
		AddChild(bubbleContainer_);
		
		dyingBacteriaHolder_ = new FContainer();
		AddChild(dyingBacteriaHolder_);
		
		ImmunityCombatManager.instance.camera_.follow(playerContainer);
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
		
		bacteria.play("pop");
		
		FSoundManager.PlaySound("bacteria_pop");
	}
	
	public void CreateBacteria(Vector2 location, Vector2 direction)
	{
		BacteriaBubble bacteria = new BacteriaBubble(location, direction);
		bacteriaContainer_.AddChild(bacteria);
		bacteria.play("idle");
		bacterias_.Add(bacteria);
		totalBacterialCreated_++;
	}
	
	public void CreateBubble(Vector2 direction)
	{
		PlayerBubble bubble = new PlayerBubble(direction);
		bubbleContainer_.AddChild(bubble);
		bubble.x = player_.x;
		bubble.y = player_.y;
		bubble.play("idle");
		bubbles_.Add(bubble);
	}
	
	public void MoveTowardsPlayerBehavior()
	{
		// if we're within 50 of the player don't move anymore
		if(enemy_.x - player_.x < 50.0f + enemy_.width/2.0f + player_.width/2.0f)
		{
			Debug.Log("Too close to player");
			
			if(!enemy_.currentAnim.name.Equals("idle") && enemy_.FinishedCount >= 1)
			{
				enemy_.curr_behavior_ = EnemyCharacter.BehaviorType.IDLE;
				enemy_.play("idle");
				Debug.Log("player_center1: " + player_.GetPosition());
			}
			return;
		}
		
		Debug.Log("player_center2: " + player_.GetPosition());
		
		// if we've been moving towards the player for more than 2 seconds
		if(Time.time - enemy_.behavior_start_time_ > 2.0f)
		{
			enemy_.curr_behavior_ = EnemyCharacter.BehaviorType.IDLE;
			enemy_.play("idle");
			return;
		}
		
		Debug.Log("decreasing x");
		enemy_.x -= enemy_.speed_*Futile.screen.width;
		
		if(!enemy_.currentAnim.name.Equals("walk"));
			enemy_.play("walk");
	}
	
	public void MoveAwayFromPlayerBehavior()
	{
				
		if(enemy_.x > Futile.screen.halfWidth - enemy_.width/2.0f)
		{
			Debug.Log("Stop moving backwards");
			enemy_.curr_behavior_ = EnemyCharacter.BehaviorType.IDLE;
			enemy_.play("idle");
			return;
		}
		
		// if we've been moving away from the player for more than 2 seconds
		if(Time.time - enemy_.behavior_start_time_ > 2.0f)
		{
			enemy_.curr_behavior_ = EnemyCharacter.BehaviorType.IDLE;
			enemy_.play("idle");
			return;
		}

		enemy_.x += enemy_.speed_*Futile.screen.width;
		
		if(!enemy_.currentAnim.name.Equals("backwards_walk"))
			enemy_.play("backwards_walk");
	}
	
	public void PunchBehavior()
	{
		Debug.Log("in punch behavior");
		Debug.Log("punch finished count: " + enemy_.FinishedCount);
		if(enemy_.FinishedCount >= 1)
		{
			enemy_.curr_behavior_ = EnemyCharacter.BehaviorType.IDLE;
			enemy_.play("idle", true);
			return;
		}
		
		if(!enemy_.currentAnim.name.Equals("punch"))
			enemy_.play("punch");
	}
	
	public void SpawnSwarmBehavior()
	{
		framesTillNextBacteria_--;
		
		if(framesTillNextBacteria_ <= 0)
		{
			float max_height = Futile.screen.halfHeight;
			float min_height = max_height/4.0f;
			
			float height = UnityEngine.Random.value*(max_height - min_height) + min_height;
			
			Vector2 initial_velocity = new Vector2(0,0);
			
			initial_velocity.y = Mathf.Sqrt(2.0f*BacteriaBubble.accel*height);
			
			float air_time = (2.0f*initial_velocity.y)/BacteriaBubble.accel;
			
			initial_velocity.x = (player_.x - enemy_.x)/air_time;
			
			Debug.Log("initial_velocity: " + initial_velocity);
			
			CreateBacteria(enemy_.GetPosition(), initial_velocity);
			
			framesTillNextBacteria_ = maxFramesTillNextBacteria_;
			enemy_.spawn_count++;
		}
		
		if(enemy_.spawn_count >= enemy_.NUM_SPAWNED_SWARM)
		{
			enemy_.curr_behavior_ = EnemyCharacter.BehaviorType.IDLE;
			
			enemy_.spawn_count = 0;
		}
	}
	
	public void PunchyBlockBehavior()
	{
		if(enemy_.FinishedCount >= 1)
		{
			enemy_.curr_behavior_ = EnemyCharacter.BehaviorType.IDLE;
			enemy_.play("idle", true);
			return;
		}
		
		if(!enemy_.currentAnim.name.Equals("block"))
		{
			
			enemy_.play("block");
			
		}
	}
	
	public void PunchyHitBehavior()
	{
		if(enemy_.FinishedCount >= 1)
		{
			enemy_.curr_behavior_ = EnemyCharacter.BehaviorType.IDLE;
			enemy_.play("idle", true);
			return;
		}
	}

	private void addComponentsToStage()
	{
		AddChild(ImmunityCombatManager.instance.stage.getBackground());
		AddChild(ImmunityCombatManager.instance.stage.getMidBack ());
		AddChild(ImmunityCombatManager.instance.stage.getMid ());
		AddChild(ImmunityCombatManager.instance.stage.getForeMid ());
		AddChild(ImmunityCombatManager.instance.stage.getForeground());
		AddChild(ImmunityCombatManager.instance.stage.getAnimation ());
	}
	
	private void checkForBacteriaOffScreen()
	{
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
	}
	
	private void checkForPlayerBubbleOffScreen()
	{
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
	}
	
	private void checkForPlayerBubbleAndBacteriaCollision()
	{
		// check if any of the bubbles have collided with any of the bacteria
		// TODO: Make this algorithm more efficient
		for(int b = bubbles_.Count-1; b >= 0; b--)
		{
			PlayerBubble bubble = bubbles_[b];
			
			Rect bubbleRect = bubble.localRect.CloneAndScaleThenOffset(.2f, .2f, bubble.x, bubble.y); //bubble.scale, bubble.scale, bubble.x, bubble.y);
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
		}
	}
	
	void checkForPlayerBubbleAndEnemyCollision()
	{
		for(int b = bubbles_.Count-1; b >= 0; b--)
		{
			PlayerBubble bubble = bubbles_[b];
			Rect bubbleRect = bubble.localRect.CloneAndScaleThenOffset(.2f, .2f, bubble.x, bubble.y); //bubble.scale, bubble.scale, bubble.x, bubble.y);

			Rect enemyRect = enemy_.localRect.CloneAndScaleThenOffset(.25f, enemy_.scaleY, enemy_.x, enemy_.y); //Mathf.Abs(enemy_.scaleX)*.25f, enemy_.scaleY, enemy_.x, enemy_.y);
			if(bubbleRect.CheckIntersect(enemyRect))
			{
				// if the enemy is currently being hit or its blocking then don't bother taking away more health
				if(enemy_.curr_behavior_ != EnemyCharacter.BehaviorType.BLOCK && enemy_.curr_behavior_ != EnemyCharacter.BehaviorType.HIT)
				{
					enemy_.ChangeHealth((int)(-.1f*EnemyCharacter.MAX_HEALTH));
					// TODO: Play hit sound
					enemy_.play("hit", true);
					enemy_.curr_behavior_ = EnemyCharacter.BehaviorType.HIT;
					if(enemy_.isDead)
					{
						// TODO: Show win screen
						player_won_ = true;
						enemy_.play("death");
						Debug.Log("You win!!");
					}
				}
				
				bubbles_.Remove(bubble);
				bubbleContainer_.RemoveChild(bubble);
			}
		}
	}
	
	void checkForBacteriaAndPlayerCollision()
	{	
		// check if player was hit
		bool playerHit = false;
		Rect playerRect = player_.localRect.CloneAndScaleThenOffset(Math.Abs(player_.scaleX), player_.scaleY, player_.x, player_.y);
		for(int b = bacterias_.Count-1; b >= 0; b--)
		{
			BacteriaBubble bacteria = bacterias_[b];
			Rect bacteriaRect = bacteria.localRect.CloneAndScaleThenOffset(Math.Abs(bacteria.scaleX), bacteria.scaleY, bacteria.x, bacteria.y);
			
			if(playerRect.CheckIntersect(bacteriaRect))
			{
				// only do more damage if the player isn't already hit and isn't blocking
				if(player_.CurrentState != PlayerCharacter.PlayerState.BLOCK && player_.CurrentState != PlayerCharacter.PlayerState.HIT)
					playerHit = true;
				HandleGotBacteria(bacteria);
			}
		}
		if(playerHit)
		{
			player_.ChangeHealth((int)(-PlayerCharacter.MAX_HEALTH*0.1f));
			FSoundManager.PlaySound("player_hit");
			ImmunityCombatManager.instance.camera_.shake(100.0f, 0.25f);
			player_.play("hit", true);
			
			if(player_.isDead)
			{
				player_lost_ = true;
				player_.play("death");
				Debug.Log("Game Over!");
			}
		}
	}
	
	void checkForPlayerEnemyCollision()
	{
		Rect enemyCollisionRect;
		if(enemy_.curr_behavior_ == EnemyCharacter.BehaviorType.PUNCH)
			enemyCollisionRect = enemy_.localRect.CloneAndScaleThenOffset(enemy_.scaleX, enemy_.scaleY, enemy_.x, enemy_.y);
		else
			enemyCollisionRect = enemy_.localRect.CloneAndScaleThenOffset(enemy_.scaleX, enemy_.scaleY, enemy_.x, enemy_.y);
		
		Rect playerRect = player_.localRect.CloneAndScaleThenOffset(Math.Abs(player_.scaleX), player_.scaleY, player_.x, player_.y);
		if(player_.CurrentState != PlayerCharacter.PlayerState.BLOCK && player_.CurrentState != PlayerCharacter.PlayerState.HIT && playerRect.CheckIntersect(enemyCollisionRect))
		{
			Debug.Log("Player hit enemy");
			player_.ChangeHealth((int)(-PlayerCharacter.MAX_HEALTH*0.2f));
			FSoundManager.PlaySound("player_hit");
			ImmunityCombatManager.instance.camera_.shake(100.0f, 0.25f);
			
			current_movement.destroy();
			
			if(enemy_.curr_behavior_ == EnemyCharacter.BehaviorType.PUNCH)
			{
				float endX = enemy_.x - (1.2f*enemy_.width)/2.0f;
				float tween_time = Math.Abs(player_.x - endX)/1000f;
				Debug.Log("Punch tween for " + tween_time + " seconds");
				player_.CurrentState = PlayerCharacter.PlayerState.HIT;
				current_movement = Go.to(player_, tween_time, new TweenConfig().floatProp("x", endX).onComplete(originalTween => player_.CurrentState = PlayerCharacter.PlayerState.IDLE));
			}
			else
				player_.x = enemy_.x - (.5f*enemy_.width)/2.0f;
			
			if(player_.isDead)
			{
				player_lost_ = true;
				player_.play("death");
				Debug.Log("Game Over!");
			}
		}
	}
	
	void checkForDeadBacteria()
	{
		for(int b = dyingBacterias_.Count-1; b >= 0; b--)
		{
			BacteriaBubble bacteria = dyingBacterias_[b];
			
			if(bacteria.FinishedCount >= 1)
			{
				dyingBacterias_.Remove(bacteria);
				dyingBacteriaHolder_.RemoveChild(bacteria);
			}
		}
	}
	
	protected void HandleUpdate()
	{
		
		if(player_won_ && enemy_.FinishedCount >= 1)
		{
			if(enemy_.FinishedCount >= 1)
			{
				// TODO: show victory menu	
			}
			return;
		}
		
		if(player_lost_)
		{
			if(player_.FinishedCount >= 1)
			{
				// TODO: show game over menu
			}
			
			return;
		}
		
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
		case EnemyCharacter.BehaviorType.HIT:
			PunchyHitBehavior();
			break;
		case EnemyCharacter.BehaviorType.BLOCK:
			PunchyBlockBehavior();
			break;
		default:
			break;
		}
		
		switch(player_.CurrentState)
		{
		case PlayerCharacter.PlayerState.BLOCK:
			if(player_.FinishedCount >= 1)
			{
				player_.CurrentState = PlayerCharacter.PlayerState.IDLE;
				player_.play("idle");	
			}
			break;
		case PlayerCharacter.PlayerState.PUNCH:
			if(player_.FinishedCount >= 1)
			{
				player_.CurrentState = PlayerCharacter.PlayerState.IDLE;
				player_.play("idle");
			}
			break;
		case PlayerCharacter.PlayerState.HIT:
			if(player_.FinishedCount >= 1)
			{
				player_.CurrentState = PlayerCharacter.PlayerState.IDLE;
				player_.play("idle");
			}
			break;
		}
		
		checkForBacteriaOffScreen();
		
		checkForPlayerBubbleOffScreen();
		
		checkForPlayerBubbleAndBacteriaCollision();
		
		checkForPlayerBubbleAndEnemyCollision();
		
		checkForBacteriaAndPlayerCollision();

		checkForPlayerEnemyCollision();
		
		checkForDeadBacteria();
		
		frameCount_++;
		
	}
	
	public void onPunchComplete()
	{
		player_being_punched = false;
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
						
						player_.scaleX = Math.Abs(player_.scaleX);
						
						if(touch.position.x - player_.x < 0)
							player_.play("backwards_walk");
						else
							player_.play("walk");
						
						player_.CurrentState = PlayerCharacter.PlayerState.WALK;
						
						// calculate movement time based on player's speed attribute
						float tween_time = Math.Abs(player_.x - touch.position.x)/(Futile.screen.width*player_.Speed);
						
						current_movement = Go.to(player_, tween_time, new TweenConfig().floatProp("x", touch.position.x).onComplete(originalTween => { player_.play("idle"); 
							player_.CurrentState = PlayerCharacter.PlayerState.IDLE; }));
					}
				}
				else
				{
					Debug.Log("Detected a swipe");
					swipe_vector.x /= swipe_magnitude;
					swipe_vector.y /=swipe_magnitude;
					
					if(touch.position.x < player_.x && touch_start.position.x > player_.x &&
						touch.position.y < (player_.y + player_.height/2.0f) && touch.position.y > (player_.y - player_.height/2.0f) &&
						touch_start.position.y < (player_.y + player_.width/2.0f) && touch_start.position.y > (player_.y - player_.height/2.0f))
					{
						// this is a block
						Debug.Log("player block");
						player_.play("block");
						player_.CurrentState = PlayerCharacter.PlayerState.BLOCK;
					}
					else if(touch.position.x > player_.x && touch_start.position.x < player_.x &&
						touch.position.y < (player_.y + player_.height/2.0f) && touch.position.y > (player_.y - player_.height/2.0f) &&
						touch_start.position.y < (player_.y + player_.height/2.0f) && touch.position.y > (player_.y - player_.height/2.0f))
					{
						// this a punch
						Debug.Log("player punch");
						player_.play("punch");
						player_.CurrentState = PlayerCharacter.PlayerState.PUNCH;
					}
					else
					{
						CreateBubble(swipe_vector);
					}
					
				}
				
				touch_starts.Remove(touch.tapCount);
			}
		}
	}
}


