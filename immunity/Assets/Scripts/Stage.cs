using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Stage : FStage {
	
	string name;
	
	FSprite level_background_sprite;
	FSprite level_midback_sprite;
	FSprite level_mid_sprite;
	FSprite level_foremid_sprite;
	FSprite level_foreground_sprite;

	//List<FAnimatedSprite> level_animations = new List<FAnimatedSprite>();
	FAnimatedSprite level_animations;
	
	
	FAnimation level_animation_sprite;
	FAnimation level_animation_sprite2;
	FAnimation level_animation_sprite3;
	
	private FParallaxContainer background;
	private FParallaxContainer midBack;
	private FParallaxContainer mid;
	private FParallaxContainer foreMid;
	private FParallaxContainer foreground;
	private FParallaxContainer animation;
	private FParallaxContainer animation2;
	private FParallaxContainer animation3;
	
	public Rect worldBounds;
	public Rect cameraBounds;
	
	private HealthBar player_healthbar;
	
	private float last_animation_start;
	
	/*
	public Stage(string newName) : base()
	{
		name = newName;
	}
	*/

	// Use this for initialization
	void Start () {
		//cameraBounds = new Rect(-512, 384, 1024, 768);
		player_healthbar = new HealthBar();
		player_healthbar.x = Futile.screen.halfWidth - player_healthbar.width - 50.0f;
		player_healthbar.y = Futile.screen.halfHeight - player_healthbar.height/2.0f - 50.0f;
		last_animation_start = Time.time;
	}
	
	// Update is called once per frame
	public void HandleUpdate () {
		/*foreach(FAnimatedSprite animation in level_animations)
		{
			Debug.Log("animation paused: " + animation.isPaused);			
			if(animation.isPaused && Time.time - last_animation_start > 1.0f)
			{
				Debug.Log("starting another animation");
				last_animation_start = Time.time;
				animation.play("Bubble");
				break;
			}
		}*/
	}
	
	public void setStomach()
	{
		initializeContainers();

		setCameras();
		setSprites ("STOMACH");
		
		putSpritesInContainers();
		addChildren();
		
		worldBounds = new Rect (-1024f, 512, 2100, -1024);

	}
	
	public void setLungs()
	{
		initializeContainers();

		setCameras ();
		setSprites ("Lungs");
		
		putSpritesInContainers();
		addChildren();
		
		worldBounds = new Rect (-1024f, 512, 2100, -1024); //placeholder values; replace once the actual sprites show up

	}
	
	public void setBrain()
	{
		initializeContainers();

		setCameras ();
		setSprites ("brain");

		putSpritesInContainers();
		addChildren();
		
		worldBounds = new Rect (-1024f, 512, 2100, -1024); //placeholder values; replace once the actual sprites show up

	}
	
	private void setCameras()
	{
		background.camObject = ImmunityCombatManager.instance.camera_;
		midBack.camObject = ImmunityCombatManager.instance.camera_;
		mid.camObject = ImmunityCombatManager.instance.camera_;
		foreMid.camObject = ImmunityCombatManager.instance.camera_;
		foreground.camObject = ImmunityCombatManager.instance.camera_;
	}

	private void initializeContainers()
	{
		Debug.Log("Called initialize containers");
		background = new FParallaxContainer();
		midBack = new FParallaxContainer();
		mid = new FParallaxContainer();
		foreMid = new FParallaxContainer();
		foreground = new FParallaxContainer();
		
		AddChild(background);
		AddChild(midBack);
		AddChild(mid);
		AddChild(foreMid);
		AddChild(foreground);

		level_animations = new FAnimatedSprite("Bubble"); //new List<FAnimatedSprite>();
		
		Debug.Log("Finished calling initialize containers");
	}

	private void setSprites(string scene)
	{
		if (scene.ToUpper().Equals("BRAIN"))
		{
			level_background_sprite = new FSprite("Background");
			level_foreground_sprite = new FSprite("Forground");

			int[] neuron_frames = {1, 2, 3, 4, 5, 6, 7, 8, 9};

			FAnimation neurons_40 = new FAnimation("NeuronFast40", "NeuronFast40", neuron_frames, 100, true);
			FAnimation neurons_60 = new FAnimation("NeuronFast60", "NeuronFast60", neuron_frames, 100, true);
			FAnimation neurons_80 = new FAnimation("NeuronFast80", "NeuronFast80", neuron_frames, 100, true);

			level_animations.addAnimation(neurons_40);
			level_animations.addAnimation(neurons_60);
			level_animations.addAnimation(neurons_80);
			
			level_animations.scale = .2f;

			background.size = new Vector2(2000, 1000); //(x, y) such that the vector is between the world bounds (background size) and camera bounds (1024x768)
			mid.size = new Vector2 (1024, 768); //(x, y) such that the vector is smaller than the camera (1024x768)
			foreground.size = new Vector2 (4096, 2048); //(x, y) such that the vector is larger than the world bounds (background size)
		}
		else if (scene.ToUpper().Equals("LUNGS"))
		{
			level_background_sprite = new FSprite("Lungs_Background_1");
			//level_midback_sprite = new FSprite("LungsV2");
			level_mid_sprite = new FSprite("Lungs_Rear");
			level_foremid_sprite = new FSprite("Lungs_MiddleFore");
			level_foreground_sprite = new FSprite("Lungs_Fore");

			int[] dust_frames = { 1, 2, 3, 4};
			FAnimation dust = new FAnimation("Dust", "Dust", dust_frames, 1000, true);

			level_animations.addAnimation(dust);
			
			level_animations.alpha = .5f;

			background.size = new Vector2 (2000, 1000); //(x, y) such that the vector is between the world bounds (background size) and camera bounds (1024x768)
			midBack.size = new Vector2 (1024, 768);
			mid.size = new Vector2 (1024, 768); //(x, y) such that the vector is smaller than the camera (1024x768)
			foreMid.size = new Vector2 (4096, 2048);
			foreground.size = new Vector2  (4096, 2048); //(x, y) such that the vector is larger than the world bounds (background size)
		}

		//default case: set the stage to be the stomach
		else
		{
			level_background_sprite = new FSprite("stomach_back");
			level_mid_sprite = new FSprite("stomach_mid");
			level_foreground_sprite = new FSprite("stomach_fore");

			int[] bubble_frames = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15};
			int[] ripple_frames = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13};
			FAnimation bubbles = new FAnimation("Bubble", "Bubble", bubble_frames, 100, true);
			FAnimation ripples = new FAnimation("Ripple", "Ripple", ripple_frames, 100, true);
			
			FAnimatedSprite bubble1 = new FAnimatedSprite("Bubble");
			bubble1.addAnimation(bubbles);
			bubble1.addAnimation(ripples);
			
			FAnimatedSprite bubble2 = new FAnimatedSprite("Bubble");
			bubble2.addAnimation(bubbles);
			bubble2.addAnimation(ripples);
			//bubble2.pause();
			bubble2.x = Futile.screen.halfWidth*.2f;
			bubble2.y = Futile.screen.halfHeight*.1f;
			
			FAnimatedSprite bubble3 = new FAnimatedSprite("Bubble");
			bubble3.addAnimation(bubbles);
			bubble3.addAnimation(ripples);
			//bubble3.pause();
			bubble3.x = -Futile.screen.halfWidth*.5f;
			bubble3.y = -Futile.screen.halfHeight*.2f;
			
			FAnimatedSprite bubble4 = new FAnimatedSprite("Bubble");
			bubble4.addAnimation(bubbles);
			bubble4.addAnimation(ripples);
			//bubble4.pause();
			bubble4.x = Futile.screen.halfWidth*.4f;
			bubble4.y = -Futile.screen.halfHeight*.4f;
			
			/*level_animations.Add(bubble1);
			level_animations.Add(bubble2);
			level_animations.Add(bubble3);
			level_animations.Add(bubble4);*/

			background.size = new Vector2  (2000, 1000);
			mid.size = new Vector2  (1024, 768);
			foreground.size = new Vector2  (4096, 2048);
		}
	}

	private void putSpritesInContainers()
	{
		background.AddChild (level_background_sprite);
		foreground.AddChild (level_foreground_sprite);


		//check for things that not every stage has.  If they're present, add them.
		if (level_midback_sprite != null)
			midBack.AddChild(level_midback_sprite);
		if (level_foremid_sprite != null)
			foreMid.AddChild(level_foremid_sprite);
		if (level_mid_sprite != null)
			mid.AddChild (level_mid_sprite);
		
	}

	private void addChildren()
	{
		AddChild (background);
		AddChild (mid);
		AddChild (foreground);

		if (level_midback_sprite != null)
			AddChild(midBack);
		if (level_foremid_sprite != null)
			AddChild(foreMid);
		
		/*foreach(FAnimatedSprite animation in level_animations)
		{
			mid.AddChild(animation);
		}*/

		AddChild (level_animations);
	}

	public FParallaxContainer getBackground()
	{
		return background;
	}

	public FParallaxContainer getMidBack()
	{
		return midBack;
	}

	public FParallaxContainer getMid()
	{
		return mid;
	}

	public FParallaxContainer getForeMid()
	{
		return foreMid;
	}

	public FParallaxContainer getForeground()
	{
		return foreground;
	}

	/*public List<FAnimatedSprite> getAnimation()
	{
		return level_animations;
	}*/
	public FAnimatedSprite getAnimation()
	{
		return level_animations;	
	}

	public FParallaxContainer getAnimation2()
	{
		return animation2;
	}

	public FParallaxContainer getAnimation3()
	{
		return animation3;
	}
}
