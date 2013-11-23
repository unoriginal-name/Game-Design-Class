using UnityEngine;
using System.Collections;

public class Stage : FStage {
	
	string name;
	
	FSprite level_background_sprite;
	FSprite level_midback_sprite;
	FSprite level_mid_sprite;
	FSprite level_foremid_sprite;
	FSprite level_foreground_sprite;
	FSprite level_animation_sprite;
	FSprite level_animation_sprite2;
	FSprite level_animation_sprite3;
	
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
		
		setSprites ("STOMACH");
		
		putSpritesInContainers();
		
		worldBounds = new Rect (-1024f, 512, 2100, -1024);
		setCameras();
	}
	
	public void setLungs()
	{
		initializeContainers();
		
		setSprites ("Lungs");
		
		putSpritesInContainers();
		
		worldBounds = new Rect (-1024f, 512, 2100, -1024); //placeholder values; replace once the actual sprites show up
		setCameras ();
	}
	
	public void setBrain()
	{
		initializeContainers();
		
		setSprites ("brain");

		putSpritesInContainers();
		
		worldBounds = new Rect (-1024f, 512, 2100, -1024); //placeholder values; replace once the actual sprites show up
		setCameras ();
	}
	
	private void setCameras()
	{
		background.camObject = ImmunityCombatManager.instance.camera;
		mid.camObject = ImmunityCombatManager.instance.camera;
		foreground.camObject = ImmunityCombatManager.instance.camera;
	}

	private void initializeContainers()
	{
		background = new FParallaxContainer();
		midBack = new FParallaxContainer();
		mid = new FParallaxContainer();
		foreMid = new FParallaxContainer();
		foreground = new FParallaxContainer();
		animation = new FParallaxContainer();
		animation2 = new FParallaxContainer();
		animation3 = new FParallaxContainer();
	}

	private void setSprites(String scene)
	{
		if (scene.toUpper().equals("BRAIN"))
		{
			level_background_sprite = new FSprite("Brain_Background");
			level_mid_sprite = new FSprite("Brain_Mid");
			level_foreground_sprite = new FSprite("Brain_Fore");
			level_animation_sprite = new FSprite("neuron_fast_60_animation");
			level_animation_sprite2 = new FSprite("neuron_fast_80_animation");
			level_animation_sprite3 = new FSprite("neuron_fast_40_animation");


			background.size.Set (2000, 1000); //(x, y) such that the vector is between the world bounds (background size) and camera bounds (1024x768)
			mid.size.Set (1024, 768); //(x, y) such that the vector is smaller than the camera (1024x768)
			foreground.size.Set (4096, 2048); //(x, y) such that the vector is larger than the world bounds (background size)
			animation.size.Set(2000, 1000);
		}
		else if (scene.toUpper().equals("LUNGS"))
		{
			level_background_sprite = new FSprite("LungsBackground");
			level_midback_sprite = new FSprite("LungsMidBack2");
			level_mid_sprite = new FSprite("LungsMidBack");
			level_foremid_sprite = new FSprite("LungsForeMid");
			level_foreground_sprite = new FSprite("LungsFore");
			level_animation_sprite = new FSprite("lung_dust");

			background.size.Set (2000, 1000); //(x, y) such that the vector is between the world bounds (background size) and camera bounds (1024x768)
			midBack.size.Set(1024, 768);
			mid.size.Set (1024, 768); //(x, y) such that the vector is smaller than the camera (1024x768)
			foreMid.size.Set(4096, 2048);
			foreground.size.Set (4096, 2048); //(x, y) such that the vector is larger than the world bounds (background size)
			animation.size.Set(2000, 1000);
		}

		//default case: set the stage to be the stomach
		else
		{
			level_background_sprite = new FSprite("Stomach_Lake");
			level_mid_sprite = new FSprite("Stomach_Mid");
			level_foreground_sprite = new FSprite("Stomach_Fore");
			level_animation_sprite = new FSprite("stomach_animations");

			background.size.Set (2000, 1000);
			mid.size.Set (1024, 768);
			foreground.size.Set (4096, 2048);
			animation.size.Set(2000, 1000);
		}
	}

	private void putSpritesInContainers()
	{
		background.AddChild (level_background_sprite);
		midBack.AddChild(level_midback_sprite);
		mid.AddChild (level_mid_sprite);
		foreMid.addChild(level_foremid_sprite);
		foreground.AddChild (level_foreground_sprite);
		animation.AddChild(level_animation_sprite);
		animation2.AddChild (level_animation_sprite2);
		animation3.AddChild (level_animation_sprite3);
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

	public FParallaxContainer getAnimation()
	{
		return animation;
	}

	public FParallaxContainer getAnimation2()
	{
		return animation2;
	}

	public FParallaxContainer getAnimation2()
	{
		return animation3;
	}
}
