using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ImmunityCombatManager : MonoBehaviour {
	
	public static ImmunityCombatManager instance;
	
	private FStage stage_;
	private Stage stage;
	
	private ImmunityPage gamePage;
	public ImmunityPage GamePage
	{
		get { return gamePage; }	
	}
	
	public int score;
	
	public FCamObject camera;
	
	void Start () {
		stage = new Stage();
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
		
		FSoundManager.PreloadSound("player_hit");
		FSoundManager.PreloadSound("bacteria_pop");
		
		Futile.atlasManager.LogAllElementNames();
		
		stage_ = Futile.stage;
		
		camera = new FCamObject();
		stage_.AddChild(camera);
		
		gamePage = new CombatPage();
		gamePage.Start();
		camera.AddChild(gamePage);	
		
		
		Rect worldBounds = stage.worldBounds;
		camera.setWorldBounds (worldBounds);
		//camera.setBounds(stage.cameraBounds);	
	}
	
	// Update is called once per frame
	void Update () {
	}
		
}