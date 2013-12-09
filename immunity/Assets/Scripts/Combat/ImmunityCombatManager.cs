using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ImmunityCombatManager : MonoBehaviour {
	
	public static ImmunityCombatManager instance;
	
	public string stage_name;
	
	private FStage stage_;
	
	private ImmunityPage gamePage;
	public ImmunityPage GamePage
	{
		get { return gamePage; }	
	}
	
	public int score;
	
	public FCamObject camera_;

	Rect worldBounds = new Rect(-Futile.screen.width * 2f, -Futile.screen.height * 2f, Futile.screen.width * 2f, Futile.screen.height * 2f);
	//Rect cameraBounds = new Rect(Futile.screen.halfWidth * -1f, Futile.screen.halfHeight * -1f, Futile.screen.width, Futile.screen.height);
	Rect cameraBounds = new Rect(0f, 0f, 1f, 1f);

	void Start () {
		instance = this;
		
		FutileParams fparams = new FutileParams(true, true, false, false);
		
		fparams.AddResolutionLevel(1280.0f, 1.0f, 1.0f, "");
		fparams.origin = new Vector2(0.5f, 0.5f);
		
		Futile.instance.Init(fparams);
		
		Futile.atlasManager.LoadAtlas("Atlases/CombatAtlas");
		
		
		Futile.atlasManager.LoadAtlas("Atlases/stomach_atlas");

		Futile.atlasManager.LoadAtlas("Atlases/lung_background");
		Futile.atlasManager.LoadAtlas("Atlases/lung_layers");
		Futile.atlasManager.LoadAtlas("Atlases/lung_dust");

		Futile.atlasManager.LoadAtlas("Atlases/BrainAtlas");
		Futile.atlasManager.LoadAtlas("Atlases/neuron_fast_80_animation");
		Futile.atlasManager.LoadAtlas("Atlases/neuron_fast_60_animation");
		Futile.atlasManager.LoadAtlas("Atlases/neuron_fast_40_animation");
		Futile.atlasManager.LoadAtlas("Atlases/neuron_slow_80_animation");
		Futile.atlasManager.LoadAtlas("Atlases/neuron_slow_60_animation");
		Futile.atlasManager.LoadAtlas("Atlases/neuron_slow_40_animation");

		Futile.atlasManager.LoadAtlas ("Atlases/Victory-DefeatAtlas");

		FSoundManager.PreloadSound("player_hit");
		FSoundManager.PreloadSound("bacteria_pop");
		
		Futile.atlasManager.LogAllElementNames();
		
		stage_ = Futile.stage;
		
		camera_ = new FCamObject();
		stage_.AddChild(camera_);
		
		//Rect worldBounds = stage.worldBounds;
		camera_.setWorldBounds (worldBounds);
		camera_.setBounds(cameraBounds);
		
		gamePage = new CombatPage();
		gamePage.Start();
		camera_.AddChild(gamePage);	
	}
	
	// Update is called once per frame
	void Update () {
	}
		
}
