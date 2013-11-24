using UnityEngine;
using System.Collections;

public class Stage : FStage {
	
	string name;
	
	FSprite level_background_sprite;
	FSprite level_midback_sprite;
	FSprite level_mid_sprite;
	FSprite level_foremid_sprite;
	FSprite level_foreground_sprite;

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
	}
	
	// Update is called once per frame
	void Update () {
	
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

		level_animations = new FAnimatedSprite("Bubble");

		Debug.Log("Finished calling initialize containers");
	}

	private void setSprites(string scene)
	{
		if (scene.ToUpper().Equals("BRAIN"))
		{
			level_background_sprite = new FSprite("Background");
			level_foreground_sprite = new FSprite("Forground");

			int[] neuron_frames = {1, 2, 3, 4, 5, 6, 7, 8, 9};

			FAnimation neurons_40 = new FAnimation("NeuronFast40", neuron_frames, 100, true);
			FAnimation neurons_60 = new FAnimation("NeuronFast60", neuron_frames, 100, true);
			FAnimation neurons_80 = new FAnimation("NeuronFast80", neuron_frames, 100, true);

			level_animations.addAnimation(neurons_40);
			level_animations.addAnimation(neurons_60);
			level_animations.addAnimation(neurons_80);

			background.size = new Vector2(2000, 1000); //(x, y) such that the vector is between the world bounds (background size) and camera bounds (1024x768)
			mid.size = new Vector2 (1024, 768); //(x, y) such that the vector is smaller than the camera (1024x768)
			foreground.size = new Vector2 (4096, 2048); //(x, y) such that the vector is larger than the world bounds (background size)
		}
		else if (scene.ToUpper().Equals("LUNGS"))
		{
			level_background_sprite = new FSprite("Lungs_Background_00089");
			level_midback_sprite = new FSprite("LungsV2");
			level_mid_sprite = new FSprite("LungsMidBack");
			level_foremid_sprite = new FSprite("LungsRear");
			level_foreground_sprite = new FSprite("Lungs_MiddleFore");

			int[] dust_frames = { 1, 2, 3, 4};
			FAnimation dust = new FAnimation("Dust", dust_frames, 100, true);

			level_animations.addAnimation(dust);

			background.size = new Vector2 (2000, 1000); //(x, y) such that the vector is between the world bounds (background size) and camera bounds (1024x768)
			midBack.size = new Vector2 (1024, 768);
			mid.size = new Vector2 (1024, 768); //(x, y) such that the vector is smaller than the camera (1024x768)
			foreMid.size = new Vector2 (4096, 2048);
			foreground.size = new Vector2  (4096, 2048); //(x, y) such that the vector is larger than the world bounds (background size)
		}

		//default case: set the stage to be the stomach
		else
		{
			level_background_sprite = new FSprite("Stomach_Lake");
			level_mid_sprite = new FSprite("Stomach_Mid");
			level_foreground_sprite = new FSprite("Stomach_Fore");

			int[] bubble_frames = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15};
			int[] ripple_frames = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13};
			FAnimation bubbles = new FAnimation("Bubble", bubble_frames, 100, true);
			FAnimation ripples = new FAnimation("Ripple", ripple_frames, 100, true);

			level_animations.addAnimation(bubbles);
			level_animations.addAnimation(ripples);

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
