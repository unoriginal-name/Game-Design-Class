using UnityEngine;
using System.Collections;

public class ImmunityMainMenu : MonoBehaviour {
	
	public FLabel test_label;
	public FSprite test_sprite;
	
	// Use this for initialization
	void Start () {
		FutileParams fparams = new FutileParams(false, false, true, true);
		
		fparams.AddResolutionLevel(800.0f, 1.0f, 1.0f, "");
		fparams.origin = new Vector2(0.5f, 0.5f);
		
		Futile.instance.Init(fparams);
		
		
		
		Futile.atlasManager.LoadAtlas("Atlases/TestAtlas");
		
		test_sprite = new FSprite("CreatureButton");
		Futile.stage.AddChild(test_sprite);
		
		//Futile.atlasManager.LoadAtlas("Atlases/MainMenuAtlas");
		
		
		//Futile.atlasManager.LoadFont("ImmunityFont", "ImmunityFont", "Atlases/MainMenuAtlas", 0.0f, 0.0f);
				
		//test_label = new FLabel("ImmunityFont", "TESTING!");
		
		//test_sprite = new FSprite("CreatureButton.png");
		
		//Futile.stage.AddChild(test_label);
		//Futile.stage.AddChild(test_sprite);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
