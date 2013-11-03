using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ImmunityCombatManager : MonoBehaviour {
	
	public static ImmunityCombatManager instance;
	
	private FStage stage_;
	
	private ImmunityPage gamePage;
	
	public int score;
	
	public FCamObject camera;
	
	private FParallaxContainer background;
	private FParallaxContainer mid;
	private FParallaxContainer foreground;
	
	void Start () {
		instance = this;
		
		FutileParams fparams = new FutileParams(true, true, false, false);
		
		fparams.AddResolutionLevel(1280.0f, 1.0f, 1.0f, "");
		fparams.origin = new Vector2(0.5f, 0.5f);
		
		Futile.instance.Init(fparams);
		
		Futile.atlasManager.LoadAtlas("Atlases/CombatAtlas");
		Futile.atlasManager.LoadAtlas("Atlases/StomachBackAtlas");
		Futile.atlasManager.LoadAtlas("Atlases/StomachMidAtlas");
		Futile.atlasManager.LoadAtlas("Atlases/StomachForeAtlas");
		Futile.atlasManager.LoadFont("ImmunityFont", "ImmunityFont", "Atlases/ImmunityFont", 0.0f, 0.0f);
		
		Futile.atlasManager.LogAllElementNames();
		
		stage_ = Futile.stage;
		
		camera = new FCamObject();
		stage_.AddChild(camera);
		
		gamePage = new CombatPage();
		gamePage.Start();
		camera.AddChild(gamePage);	
		
		background.AddChild (CombatPage.instance.levelBack_);
		mid.AddChild (CombatPage.instance.levelMid_);
		foreground.AddChild (CombatPage.instance.levelFore_);
		
		background.camObject = ImmunityCombatManager.instance.camera;
		mid.camObject = ImmunityCombatManager.instance.camera;
		foreground.camObject = ImmunityCombatManager.instance.camera;
		
		Rect worldBounds = new Rect(0, 0, Futile.screen.pixelWidth, Futile.screen.pixelHeight);
		camera.setWorldBounds (worldBounds);
		camera.setBounds (new Rect(0, 0, Futile.screen.pixelWidth/2, Futile.screen.pixelHeight/2));
	}
	
	// Update is called once per frame
	void Update () {
	}
		
}