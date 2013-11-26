using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class CombatPage : ImmunityPage, FMultiTouchableInterface {

	// Victory defeat menus
	private bool displayEndScreen = true;
	private FSprite victory;
	private FSprite defeat;
	private FButton yesButton;
	private FButton noButton;
	private FButton nextLevelButton;
	private FButton levelSelectButton;

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
		
	private Tween curr_player_movement = null;
	private Tween curr_enemy_movement = null;
	
	private bool player_won_ = false;
	private bool player_lost_ = false;
	
	Dictionary<int, FTouch> touch_starts = new Dictionary<int, FTouch>();
	float touch_start_time;
	
	private bool player_punch_did_damage = true;
	private bool enemy_punch_did_damage = true;
	
	public Rect level_bounding_box;
			
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
        team/master
		background.AddChild (levelBack_);
		mid.AddChild (levelMid_);
		foreground.AddChild (levelFore_);
		*/
		
		FSoundManager.StopMusic();
		FSoundManager.UnloadAllSoundsAndMusic();
		//FSoundManager.PlayMusic("stomach_ambience");
		FSoundManager.PlayMusic("battle_music", .204f);

		//addComponentsToStage();
		
		AddChild(ImmunityCombatManager.instance.stage);

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
		
		level_bounding_box = new Rect(-Futile.screen.halfWidth*.9f, Futile.screen.halfHeight*.9f, Futile.screen.halfWidth*1.8f, Futile.screen.halfHeight*1.8f);
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
		if(bubbles_.Count >= 3)
			return;
		PlayerBubble bubble = new PlayerBubble(direction);
		bubbleContainer_.AddChild(bubble);
		bubble.x = player_.x;
		bubble.y = player_.y;
		bubble.play("idle");
		bubbles_.Add(bubble);
	}
	
	public void HandlePlayerHit(float damage)
	{
		player_.ChangeHealth((int)(-damage));
		FSoundManager.PlaySound("player_hit");
		ImmunityCombatManager.instance.camera_.shake(100.0f, 0.25f);
		player_.CurrentState = PlayerCharacter.PlayerState.HIT;
		player_.play("hit", true);
			
		if(player_.isDead)
		{
			player_lost_ = true;
			player_.play("death");
			
			FSoundManager.StopMusic();
			FSoundManager.PlaySound("player_lose");
			Debug.Log("Game Over!");
		}	
	}
	
	public void HandleEnemyHit(float damage)
	{
		enemy_.ChangeHealth((int)(-damage));
		// TODO: Play hit sound
		enemy_.play("hit", true);
		enemy_.curr_behavior_ = EnemyCharacter.BehaviorType.HIT;
		if(enemy_.isDead)
		{
			// TODO: Show win screen
			player_won_ = true;
			enemy_.play("death");
			
			FSoundManager.StopMusic();
			FSoundManager.PlaySound("player_victory");
			Debug.Log("You win!!");
		}
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
			}
			return;
		}
				
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
				
		if(enemy_.x > level_bounding_box.xMax - enemy_.width/2.0f)
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
		{
			enemy_punch_did_damage = false;
			enemy_.play("punch");
		}
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

	/*private void addComponentsToStage()
	{
		AddChild(ImmunityCombatManager.instance.stage.getBackground());
		AddChild(ImmunityCombatManager.instance.stage.getMidBack ());
		AddChild(ImmunityCombatManager.instance.stage.getMid ());
		AddChild(ImmunityCombatManager.instance.stage.getForeMid ());
		AddChild(ImmunityCombatManager.instance.stage.getForeground());
		List<FAnimatedSprite> animations = ImmunityCombatManager.instance.stage.getAnimation();
		foreach(FAnimatedSprite animation in animations)
			AddChild(animation);
	}*/
	
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
					HandleEnemyHit(0);
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
			HandlePlayerHit(PlayerCharacter.MAX_HEALTH*0.01f);
		}
	}
	
	void checkForPlayerEnemyCollision()
	{
		List<Rect> enemy_rects = enemy_.getCollisionRects();
		List<Rect> player_rects = player_.getCollisionRects();
		
		foreach(Rect player_rect in player_rects)
		{
			foreach(Rect enemy_rect in enemy_rects)
			{
				if(player_rect.CheckIntersect(enemy_rect))
				{
					if(player_.CurrentState == PlayerCharacter.PlayerState.PUNCH)
					{
						if(!player_punch_did_damage)
						{
							if(enemy_.curr_behavior_ != EnemyCharacter.BehaviorType.BLOCK && enemy_.curr_behavior_ != EnemyCharacter.BehaviorType.HIT)
							{
								HandleEnemyHit(EnemyCharacter.MAX_HEALTH*0.1f);
							}
							player_punch_did_damage = true;
							enemy_.x = player_rect.xMax + enemy_rect.width/2.0f + Futile.screen.halfWidth*.14f;
							if(enemy_.x + enemy_rect.width/2.0f > level_bounding_box.xMax)
								enemy_.x = level_bounding_box.xMax - enemy_rect.width/2.0f;
						}
					}
					
					if(enemy_.curr_behavior_ == EnemyCharacter.BehaviorType.PUNCH)
					{
						if(!enemy_punch_did_damage)
						{
							if(player_.CurrentState != PlayerCharacter.PlayerState.BLOCK && player_.CurrentState != PlayerCharacter.PlayerState.HIT)
							{
								HandlePlayerHit(PlayerCharacter.MAX_HEALTH*0.1f);							
							}
							
							enemy_punch_did_damage = true;
							player_.x = enemy_rect.xMin - player_rect.width/2.0f - Futile.screen.halfWidth*.01f;
							if(player_rect.x - player_rect.width/2.0f < level_bounding_box.xMin)
								player_.x = level_bounding_box.xMin + player_rect.width/2.0f;
							if(curr_player_movement != null)
								curr_player_movement.destroy();
						}
					}

					if(player_.CurrentState != PlayerCharacter.PlayerState.PUNCH)
					{
						if(player_.CurrentState != PlayerCharacter.PlayerState.BLOCK && player_.CurrentState != PlayerCharacter.PlayerState.HIT)
							HandlePlayerHit(PlayerCharacter.MAX_HEALTH*.05f);
						player_.x = enemy_rect.xMin - player_rect.width/2.0f - Futile.screen.halfWidth*.01f;
						if(player_rect.x - player_rect.width/2.0f < level_bounding_box.xMin)
								player_.x = level_bounding_box.xMin + player_rect.width/2.0f;
						if(curr_player_movement != null)
							curr_player_movement.destroy();
					}
					
				}
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

	private void HandleYesButton(FButton button){
		Application.LoadLevel(ImmunityCombatManager.instance.stage_name);
	}

	private void HandleNoButton(FButton button){
		Application.LoadLevel("ImmunityMainMenu");
	}

	private void HandleLevelSelectButton(FButton button){
		Application.LoadLevel("ImmunityMainMenu");
	}

	private void HandleNextLevelButton(FButton button){
		if(ImmunityCombatManager.instance.stage_name.Equals("stomach"))
			Application.LoadLevel("Lungs");
		else if(ImmunityCombatManager.instance.stage_name.Equals("lung"))
			Application.LoadLevel("Brain");
		else
			Application.LoadLevel("ImmunityMainMenu");
	}


	protected void HandleUpdate()
	{	
		if(player_won_)
		{
			if(ImmunityCombatManager.instance.stage_name.Equals("stomach"))
			{
				if(!PlayerPrefs.GetString("highest_level").Equals("brain"))
					PlayerPrefs.SetString("highest_level", "lung");
			}
			else if(ImmunityCombatManager.instance.stage_name.Equals("lung"))
				PlayerPrefs.SetString("highest_level", "brain");
			
			player_.play("idle");
			if(enemy_.FinishedCount >= 1)
			{
				if(displayEndScreen){
					victory = new FSprite("victory screen final");
					nextLevelButton = new FButton("NextLevel", "NextLevelPressed");

				
					levelSelectButton = new FButton("LevelSelect","LevelSelectPressed");
					
					AddChild(victory);
					AddChild(nextLevelButton);
					AddChild(levelSelectButton);

					nextLevelButton.SignalRelease += HandleNextLevelButton;
					levelSelectButton.SignalRelease += HandleLevelSelectButton;
					
					nextLevelButton.SetPosition(new Vector2(200, -210));
					levelSelectButton.SetPosition(new Vector2(-200,-300));
					displayEndScreen = false;
					
					
					if(ImmunityCombatManager.instance.stage_name.Equals("brain"))
					{
						nextLevelButton.isVisible = false;
					}
					
					AddChild(player_);
					player_.x = -Futile.screen.halfWidth*.6f;
					player_.y = -Futile.screen.halfHeight*.05f;
					player_.scale = 1.4f;
					player_.play("idle");
				}

			}
			return;
		}
		
		if(player_lost_)
		{
			enemy_.play("idle");
			if(player_.FinishedCount >= 1)
			{
				if(displayEndScreen){
					defeat = new FSprite("try again screen final");
					yesButton = new FButton("Yes", "YesPressed");
					noButton = new FButton("No","NoPressed");
					
					AddChild(defeat);
					AddChild(yesButton);
					AddChild(noButton);

					yesButton.SignalRelease += HandleYesButton;
					noButton.SignalRelease += HandleNoButton;

					yesButton.SetPosition(new Vector2(-120, -100));
					noButton.SetPosition(new Vector2(150,-100));
					displayEndScreen = false;
					
					AddChild(player_);
					player_.x = -Futile.screen.halfWidth*.6f;
					player_.y = -Futile.screen.halfHeight*.12f;
					player_.scale = 1.4f;
					player_.play("idle");
				}
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
	
	public void HandleMultiTouch(FTouch[] touches)
	{
		// don't process input if the game is over
		if(player_lost_ || player_won_)
			return;
		
		
		
		foreach(FTouch touch in touches)
		{
			
			if(touch.phase == TouchPhase.Began)
			{
				if(!touch_starts.ContainsKey(touch.tapCount))
					touch_starts.Add(touch.tapCount, touch);
				else
					touch_starts[touch.tapCount] = touch; // update the touch otherwise
				
				touch_start_time = Time.time;
			}
			else if(touch.phase == TouchPhase.Ended)
			{
				
				// don't process input if the player is currently doing something else
				/*if(player_.CurrentState != PlayerCharacter.PlayerState.IDLE)
					return;*/
				
				FTouch touch_start = touch_starts[touch.tapCount];
				
				Vector2 swipe_vector = touch.position - touch_start.position;
				
				// normalize the swipe vector
				float swipe_magnitude = Mathf.Sqrt(swipe_vector.x*swipe_vector.x + swipe_vector.y*swipe_vector.y);
				
				// TODO: Change this to a reasonable threshold
				if(swipe_magnitude <= 30.0f)
				{
					// This is a tap
					Debug.Log("Detected a tap");
					
					Debug.Log("Tap delta time: " + (Time.time - touch_start_time));
					
					if(Time.time - touch_start_time < .2)
					{
						if(touch.position.y < -Futile.screen.halfHeight/2.0f)
						{
							// if already executing a move, first stop it
							if(curr_player_movement != null)
							{
								curr_player_movement.destroy();
							}
							
							float final_position = touch.position.x;
							if(touch.position.x < 0 && touch.position.x - player_.width/2.0f < level_bounding_box.xMin)
								final_position = level_bounding_box.xMin + player_.width/2.0f;
							
							if(touch.position.x > 0 && touch.position.x + player_.width/2.0f > level_bounding_box.xMax)
								final_position = level_bounding_box.xMax - player_.width/2.0f;
							
							if(final_position == player_.x)
								return;
							
							player_.scaleX = Math.Abs(player_.scaleX);
							
							if(final_position - player_.x < 0)
								player_.play("backwards_walk");
							else
								player_.play("walk");
							
							player_.CurrentState = PlayerCharacter.PlayerState.WALK;
							
							// calculate movement time based on player's speed attribute
							float tween_time = Math.Abs(player_.x - final_position)/(Futile.screen.width*player_.Speed);
							
							curr_player_movement = Go.to(player_, tween_time, new TweenConfig().floatProp("x", final_position).onComplete(originalTween => { player_.play("idle"); 
								player_.CurrentState = PlayerCharacter.PlayerState.IDLE; }));
						}
					}
					else
					{
						Vector2 bacteria_direction = new Vector2();
						bacteria_direction.x = touch.position.x - player_.x;
						bacteria_direction.y = touch.position.y - player_.y;
						
						float magnitude = Mathf.Sqrt(bacteria_direction.x*bacteria_direction.x + bacteria_direction.y*bacteria_direction.y);
						
						bacteria_direction.x /= magnitude;
						bacteria_direction.y /= magnitude;
						
						CreateBubble(bacteria_direction);
						
						if((float)player_.Health/(float)PlayerCharacter.MAX_HEALTH >= .5f)
						{
							bacteria_direction.x *= 1.2f;
							bacteria_direction.y *= .8f;
						
							CreateBubble(bacteria_direction);
						}
						
						if((float)player_.Health/(float)PlayerCharacter.MAX_HEALTH >= .75f)
						{
							bacteria_direction.x *= .6f;
							bacteria_direction.y *= 1.4f;
						
							CreateBubble(bacteria_direction);
							
						}
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
					else if(touch.position.x > enemy_.x && touch_start.position.x < enemy_.x &&
						touch.position.y < (enemy_.y + enemy_.height/2.0f) && touch.position.y > (enemy_.y - enemy_.height/2.0f) &&
						touch_start.position.y < (enemy_.y + enemy_.height/2.0f) && touch.position.y > (enemy_.y - enemy_.height/2.0f))
					{
						// this a punch
						Debug.Log("player punch");
						player_.play("punch");
						player_.CurrentState = PlayerCharacter.PlayerState.PUNCH;
						player_punch_did_damage = false;
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


