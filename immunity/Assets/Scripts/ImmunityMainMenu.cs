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
		Futile.atlasManager.LoadFont("ImmunityFont", "ImmunityFont", "Atlases/ImmunityFont", 0.0f, 0.0f);
		
		test_label = new FLabel("ImmunityFont", "Testing");
		test_sprite = new FSprite("CreatureButton");
		Futile.stage.AddChild(test_sprite);
		Futile.stage.AddChild(test_label);
	}
	
	// Update is called once per frame
	void Update () {
		//test_label.scale = test_label.scale*1.1f;
	}
}
