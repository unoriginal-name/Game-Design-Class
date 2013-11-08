using UnityEngine;
using System.Collections;

public class Stage : FStage {
	
	string name;
	
	FSprite level_background_sprite;
	FSprite level_mid_sprite;
	FSprite level_foreground_sprite;
	FSprite level_animation_sprite;
	
	public FParallaxContainer background;
	public FParallaxContainer mid;
	public FParallaxContainer foreground;
	public FParallaxContainer animation;
	
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
		background = new FParallaxContainer();
		mid = new FParallaxContainer();
		foreground = new FParallaxContainer();
		animation = new FParallaxContainer();
		
		level_background_sprite = new FSprite("Stomach_Lake");
		level_mid_sprite = new FSprite("Stomach_Mid");
		level_foreground_sprite = new FSprite("Stomach_Fore");
		//level_animation_sprite = new FSprite("Bubble_Pop");
		
		background.AddChild (level_background_sprite);
		mid.AddChild (level_mid_sprite);
		foreground.AddChild (level_foreground_sprite);
		//animation.AddChild(level_animation_sprite);
		
		background.size.Set (2000, 1000);
		mid.size.Set (1024, 768);
		foreground.size.Set (4096, 2048);
		//animation.size.Set(float X, float Y)
		
		worldBounds = new Rect (-1024f, 512, 2100, -1024);
		setCameras();
	}
	
	public void setLungs()
	{
		background = new FParallaxContainer();
		mid = new FParallaxContainer();
		foreground = new FParallaxContainer();
		animation = new FParallaxContainer();
		
		level_background_sprite = new FSprite("Lung_Background");
		level_mid_sprite = new FSprite("Lung_Mid");
		level_foreground_sprite = new FSprite("Lung_Fore");
		//level_animation_sprite = new FSprite("Lung_Dust");
		
		background.AddChild (level_background_sprite);
		mid.AddChild (level_mid_sprite);
		foreground.AddChild (level_foreground_sprite);
		//animation.AddChild(level_animation_sprite);
		
		background.size.Set (2000, 1000); //(x, y) such that the vector is between the world bounds (background size) and camera bounds (1024x768)
		mid.size.Set (1024, 768); //(x, y) such that the vector is smaller than the camera (1024x768)
		foreground.size.Set (4096, 2048); //(x, y) such that the vector is larger than the world bounds (background size)
		//animation.size.Set(float X, float Y)
		
		worldBounds = new Rect (-1024f, 512, 2100, -1024); //placeholder values; replace once the actual sprites show up
		setCameras ();
	}
	
	public void setBrain()
	{
		background = new FParallaxContainer();
		mid = new FParallaxContainer();
		foreground = new FParallaxContainer();
		animation = new FParallaxContainer();
		
		level_background_sprite = new FSprite("Brain_Background");
		level_mid_sprite = new FSprite("Brain_Mid");
		level_foreground_sprite = new FSprite("Brain_Fore");
		//level_animation_sprite = new FSprite("Lung_Breathe");
		
		background.AddChild (level_background_sprite);
		mid.AddChild (level_mid_sprite);
		foreground.AddChild (level_foreground_sprite);
		//animation.AddChild(level_animation_sprite);
		
		background.size.Set (2000, 1000); //(x, y) such that the vector is between the world bounds (background size) and camera bounds (1024x768)
		mid.size.Set (1024, 768); //(x, y) such that the vector is smaller than the camera (1024x768)
		foreground.size.Set (4096, 2048); //(x, y) such that the vector is larger than the world bounds (background size)
		//animation.size.Set(float X, float Y)
		
		worldBounds = new Rect (-1024f, 512, 2100, -1024); //placeholder values; replace once the actual sprites show up
		setCameras ();
	}
	
	void setCameras()
	{
		background.camObject = ImmunityCombatManager.instance.camera;
		mid.camObject = ImmunityCombatManager.instance.camera;
		foreground.camObject = ImmunityCombatManager.instance.camera;
	}
}
