using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ImmunityCombatManager : MonoBehaviour {
	
	public static ImmunityCombatManager instance;
	
	private FStage stage_;
	public Stage stage;
	
	private ImmunityPage gamePage;
	public ImmunityPage GamePage
	{
		get { return gamePage; }	
	}
	
	public int score;
	
	public FCamObject camera_;
	
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
		
		FSoundManager.PreloadSound("player_hit");
		FSoundManager.PreloadSound("bacteria_pop");
		
		Futile.atlasManager.LogAllElementNames();
		
		stage_ = Futile.stage;
		
		camera_ = new FCamObject();
		stage_.AddChild(camera_);
		
		stage = new Stage();
		Debug.Log("calling setstomach");
		stage.setStomach ();
		Debug.Log("Finished calling setstomach");
		
		Rect worldBounds = stage.worldBounds;
		camera_.setWorldBounds (worldBounds);
		//camera_.setBounds(stage.cameraBounds);
		
		gamePage = new CombatPage();
		gamePage.Start();
		camera_.AddChild(gamePage);	
	}
	
	// Update is called once per frame
	void Update () {
	}
		
}