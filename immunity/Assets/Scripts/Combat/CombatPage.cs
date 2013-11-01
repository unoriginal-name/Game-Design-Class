using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CombatPage : ImmunityPage, FMultiTouchableInterface {
	
	private FSprite levelFore_;
	private FSprite levelMid_;
	private FSprite levelBack_;
	
	private int totalBacterialCreated_ = 0;
	private FContainer bacteriaContainer_;
	private List<BacteriaBubble> bacterias_ = new List<BacteriaBubble>();
	
	private FLabel scoreLabel_;
	
	private int frameCount_ = 0;
	private int maxFramesTillNextBacteria_ = 22;
	private int framesTillNextBacteria_ = 0;
	
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
		
		bacteriaContainer_ = new FContainer();
		AddChild(bacteriaContainer_);
		
		ImmunityCombatManager.instance.score = 0;
		
		scoreLabel_ = new FLabel("ImmunityFont", "0 Bacteria");
		scoreLabel_.anchorX = 0.0f;
		scoreLabel_.anchorY = 1.0f;
		scoreLabel_.x = -Futile.screen.halfWidth + 50.0f;
		scoreLabel_.y = Futile.screen.halfHeight - 50.0f;
		scoreLabel_.color = Color.white;
		AddChild(scoreLabel_);
		
	}
	
	public void HandleGotBacteria(BacteriaBubble bacteria)
	{
		
		// TODO: create the pop animation here
		
		bacteriaContainer_.RemoveChild(bacteria);
		bacterias_.Remove(bacteria);
		
		ImmunityCombatManager.instance.score++;
		
		if(ImmunityCombatManager.instance.score == 1)
		{
			scoreLabel_.text = "1 Bacteria";
		}
		else
		{
			scoreLabel_.text = ImmunityCombatManager.instance.score+" Bacterias";	
		}
		
		// show particle effect
	}
	
	public void CreateBacteria()
	{
		BacteriaBubble bacteria = new BacteriaBubble();
		bacteriaContainer_.AddChild(bacteria);
		bacteria.x = RXRandom.Range(-Futile.screen.width/2 + 50, Futile.screen.width/2-50); // padded inside the screen width
		bacteria.y = Futile.screen.height/2 + 60; // above the screen
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
		
		frameCount_++;
		
	}
	
	public void HandleMultiTouch(FTouch[] touches)
	{
		foreach(FTouch touch in touches)
		{
			if(touch.phase == TouchPhase.Began)
			{
				// go in reverse order so if bacteria is removed it doesn't matter
				// also checks sprites in front to back order
				for(int b = bacterias_.Count-1; b >= 0; b--)
				{
					BacteriaBubble bacteria = bacterias_[b];
					
					Vector2 touchPos = bacteria.GlobalToLocal(touch.position);
					
					if(bacteria.textureRect.Contains(touchPos))
					{
						HandleGotBacteria(bacteria);
						break; // a touch can only hit one bacteria at a time
					}
				}
			}
		}
	}
}



/*


using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BInGamePage : BPage, FMultiTouchableInterface
{
       
        private FLabel _scoreLabel;
        private FLabel _timeLabel;
        
        private float _secondsLeft = 15.9f;
        
        private FContainer _effectHolder;
        
        private GameObject _particlePrefab;

        public BInGamePage()
        {
                EnableMultiTouch();
                ListenForUpdate(HandleUpdate);
                ListenForResize(HandleResize);
        }

        public void HandleGotBanana(BBanana banana)
        {
                CreateBananaExplodeEffect(banana);
                
                _bananaContainer.RemoveChild (banana);
                _bananas.Remove(banana);

                BMain.instance.score++;
                
                if(BMain.instance.score == 1)
                {
                        _scoreLabel.text = "1 Banana";        
                }
                else 
                {
                        _scoreLabel.text = BMain.instance.score+" Bananas";        
                }
                
                FUnityParticleSystemNode particleNode = new FUnityParticleSystemNode(_particlePrefab, true);
                
                AddChild (particleNode);
                
                particleNode.x = banana.x;
                particleNode.y = banana.y;
                
                FSoundManager.PlaySound("BananaSound", 1.0f);
        }

        public void CreateBanana ()
        {
                BBanana banana = new BBanana();
                _bananaContainer.AddChild(banana);
                banana.x = RXRandom.Range(-Futile.screen.width/2 + 50, Futile.screen.width/2 - 50); //padded inside the screen width
                banana.y = Futile.screen.height/2 + 60; //above the screen
                _bananas.Add(banana);
                _totalBananasCreated++;
        }
        
        
        protected void HandleUpdate ()
        {
                _secondsLeft -= Time.deltaTime;
                
                if(_secondsLeft <= 0)
                {
                        FSoundManager.PlayMusic("VictoryMusic",0.5f);
                        BMain.instance.GoToPage(BPageType.ScorePage);
                        return;
                }
                
                _timeLabel.text = ((int)_secondsLeft) + " Seconds Left";
                
                if(_secondsLeft < 10) //make the timer red with 10 seconds left
                {
                        _timeLabel.color = new Color(1.0f,0.2f,0.0f);
                }
                
                _framesTillNextBanana--;
                
                if(_framesTillNextBanana <= 0)
                {
                        if(_totalBananasCreated % 4 == 0) //every 4 bananas, make the bananas come a little bit sooner
                        {
                                _maxFramesTillNextBanana--;
                        }
                        
                        _framesTillNextBanana = _maxFramesTillNextBanana;
                        
                        CreateBanana();
                }
                
                
                //loop backwards so that if we remove a banana from _bananas it won't cause problems
                for (int b = _bananas.Count-1; b >= 0; b--) 
                {
                        BBanana banana = _bananas[b];
                        
                        //remove a banana if it falls off screen
                        if(banana.y < -Futile.screen.halfHeight - 50)
                        {
                                _bananas.Remove(banana);
                                _bananaContainer.RemoveChild(banana);
                        }
                }
                
                _frameCount++;
        }
        
        public void HandleMultiTouch(FTouch[] touches)
        {
                foreach(FTouch touch in touches)
                {
                        if(touch.phase == TouchPhase.Began)
                        {
                                
                                //we go reverse order so that if we remove a banana it doesn't matter
                                //and also so that that we check from front to back
                                
                                for (int b = _bananas.Count-1; b >= 0; b--) 
                                {
                                        BBanana banana = _bananas[b];
                                        
                                        Vector2 touchPos = banana.GlobalToLocal(touch.position);
                                        
                                        if(banana.textureRect.Contains(touchPos))
                                        {
                                                HandleGotBanana(banana);        
                                                break; //break so that a touch can only hit one banana at a time
                                        }
                                }
                        }
                }
        }
        
        private void CreateBananaExplodeEffect(BBanana banana)
        {
                //we can't just get its x and y, because they might be transformed somehow
                Vector2 bananaPos = _effectHolder.OtherToLocal(banana,Vector2.zero);
                
                FSprite explodeSprite = new FSprite("Banana");
                _effectHolder.AddChild(explodeSprite);
                explodeSprite.shader = FShader.Additive;
                explodeSprite.x = bananaPos.x;
                explodeSprite.y = bananaPos.y;
                explodeSprite.rotation = banana.rotation;
                
                Go.to (explodeSprite, 0.3f, new TweenConfig().floatProp("scale",1.3f).floatProp("alpha",0.0f).onComplete(HandleExplodeSpriteComplete));
        }
        
        private static void HandleExplodeSpriteComplete (AbstractTween tween)
        {
                FSprite explodeSprite = (tween as Tween).target as FSprite;
                explodeSprite.RemoveFromContainer();
        }
}*/