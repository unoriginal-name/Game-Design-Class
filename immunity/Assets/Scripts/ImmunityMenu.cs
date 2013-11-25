using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum PageType
{
	None,
    TitlePage,
	LevelSelectPage
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
		
		FutileParams fparams = new FutileParams(true, true, false, false);
		
		fparams.AddResolutionLevel(1280.0f, 1.0f, 1.0f, "");
		fparams.origin = new Vector2(0.5f, 0.5f);
		
		Futile.instance.Init(fparams);.5,
		
		Futile.atlasManager.LoadAtlas("Atlases/MainMenu");
		Futile.atlasManager.LoadAtlas("Atlases/DrawingAtlas");
		Futile.atlasManager.LoadAtlas("Atlases/LevelSelectMenu");

		//Futile.atlasManager.LoadFont("ImmunityFont", "ImmunityFont", "Atlases/ImmunityFont", 0.0f, 0.0f);

		GoToMenu (PageType.TitlePage);
		/*
		stage_ = Futile.stage;
		
		currentPageType = PageType.TitlePage;
		currentPage = new TitlePage();
		
		stage_.AddChild(currentPage);
		currentPage.Start();
		*/
	}
	
	// Update is called once per frame
	void Update () {
		//test_label.scale = test_label.scale*1.1f;
	}
		
	public void GoToMenu(PageType pageType) {
		if(currentPageType == pageType) return; // already on this menu

		stage_ = Futile.stage;

		if(currentPage != null && stage_ != null){
			stage_.RemoveChild(currentPage);
		}

		switch(pageType){
		case PageType.TitlePage:
			currentPageType = PageType.TitlePage;
			currentPage = new TitlePage();
			break;
		case PageType.LevelSelectPage:
			currentPageType = PageType.LevelSelectPage;
			currentPage = new LevelSelectPage();
			break;
		}

		stage_.AddChild(currentPage);
		currentPage.Start();
		
		//ImmunityPage menuToCreate = null;
	}
}
