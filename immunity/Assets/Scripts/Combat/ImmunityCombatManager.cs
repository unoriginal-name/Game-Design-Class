using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ImmunityCombatManager : MonoBehaviour {
	
	public static ImmunityCombatManager instance;
	
	private FStage stage_;
	
	private ImmunityPage gamePage;
	
	public int score;
	
	public FCamObject camera;
	
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
		
		//Rect worldBounds = new Rect(0, 0, 2000, 2000);
		//camera.setWorldBounds (worldBounds);
		//camera.setBounds (new Rect(0, 0, 1500, 1500));

	}
	
	// Update is called once per frame
	void Update () {
	}
		
}