using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum PageType
{
	None,
    TitlePage,
	LevelSelectPage,
	CreditsPage
}


public class ImmunityMenu : MonoBehaviour {
	
	public static ImmunityMenu instance;
	
	private PageType currentPageType = PageType.None;
	private ImmunityPage currentPage = null;
	
	private FStage stage_;
	
	public FSoundManager sound_manager_;
	
	// Use this for initialization
	void Start () {
		instance = this;
		
		if(!PlayerPrefs.HasKey("highest_level"))
			PlayerPrefs.SetString("highest_level", "stomach");
		
		FutileParams fparams = new FutileParams(true, true, false, false);
		
		fparams.AddResolutionLevel(1280.0f, 1.0f, 1.0f, "");
		fparams.origin = new Vector2(0.5f, 0.5f);
		
		Futile.instance.Init(fparams);
		
		Futile.atlasManager.LoadAtlas("Atlases/MainMenu");
		Futile.atlasManager.LoadAtlas("Atlases/DrawingAtlas");
		Futile.atlasManager.LoadAtlas("Atlases/LevelSelectMenu");
		Futile.atlasManager.LoadAtlas("Atlases/CreditsAtlas");

		//Futile.atlasManager.LoadFont("ImmunityFont", "ImmunityFont", "Atlases/ImmunityFont", 0.0f, 0.0f);

		GoToMenu (PageType.TitlePage);
	}
	
	// Update is called once per frame
	void Update () {
		//test_label.scale = test_label.scale*1.1f;
		if(Input.GetKeyDown(KeyCode.Escape)){
			switch(currentPageType){
			case PageType.TitlePage:
				Application.Quit();
				break;
			case PageType.LevelSelectPage:
				GoToMenu(PageType.TitlePage);
				break;
			case PageType.CreditsPage:
				GoToMenu(PageType.TitlePage);
				break;
			}
		}
	}
		
	public void GoToMenu(PageType pageType) {
		if(currentPageType == pageType) return; // already on this menu

		// Get the stage
		stage_ = Futile.stage;

		// Remove the current page if it exists
		if(currentPage != null && stage_ != null){
			stage_.RemoveChild(currentPage);
		}

		// Set new page and page type
		switch(pageType){
		case PageType.TitlePage:
			currentPageType = PageType.TitlePage;
			currentPage = new TitlePage();
			break;
		case PageType.LevelSelectPage:
			currentPageType = PageType.LevelSelectPage;
			currentPage = new LevelSelectPage();
			break;
		case PageType.CreditsPage:
			currentPageType = PageType.CreditsPage;
			currentPage = new CreditsPage();
			break;
		}

		// Add the page and init it
		stage_.AddChild(currentPage);
		currentPage.Start();
	}
}
