using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum MenuType
{
	None,
    TitleMenu,
	NetworkMenu,
	SettingsMenu,
	AboutMenu
}


public class ImmunityMenu : MonoBehaviour {
	
	private MenuType currentMenuType = MenuType.None;
	private Menu currentMenu = null;
	
	// Use this for initialization
	void Start () {
		FutileParams fparams = new FutileParams(false, false, true, true);
		
		fparams.AddResolutionLevel(800.0f, 1.0f, 1.0f, "");
		fparams.origin = new Vector2(0.5f, 0.5f);
		
		Futile.instance.Init(fparams);
		
		Futile.atlasManager.LoadAtlas("Atlases/MainMenu");
		Futile.atlasManager.LoadFont("ImmunityFont", "ImmunityFont", "Atlases/ImmunityFont", 0.0f, 0.0f);

		
	}
	
	// Update is called once per frame
	void Update () {
		//test_label.scale = test_label.scale*1.1f;
	}
		
	public void GoToMenu(MenuType menuType) {
		if(currentMenuType == menuType) return; // already on this menu
		
		Menu menuToCreate = null;
		
		
	}
}
