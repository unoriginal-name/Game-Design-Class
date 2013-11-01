using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CombatPage : ImmunityPage, FMultiTouchableInterface {
	
	private FSprite levelFore_;
	private FSprite levelMid_;
	private FSprite levelBack_;
	
	private int totalBacterialCreated_ = 0;
	private FContainer bacteriaContainer_;
	private List<BacteriaBubble> bacterias_ = new List<BacteriaBubble>();
	
	private FLabel scoreLabel_;
	
	private int frameCount_ = 0;
	private int maxFramesTillNextBacteria = 22;
	private int framesTillNextBacteria = 0;
	
	public CombatPage()
	{
		EnableMultiTouch();
		ListenForUpdate(HandleUpdate);
	}
	
	// Use this for initialization
	override public void Start () {
	
		levelBack_ = new FSprite("Stomach_Lake");
		levelMid_ = new FSprite("Stomach_Mid");
		levelFore_ = new FSprite("Stomach_Fore");
		AddChild(levelBack_);
		AddChild(levelMid_);
		AddChild(levelFore_);
		
		bacteriaContainer_ = new FContainer();
		AddChild(bacteriaContainer_);
		
		ImmunityCombatManager.instance.score_ = 0;
		
		scoreLabel_ = new FLabel("ImmunityFont", "0 Bacteria");
		scoreLabel_.anchorX = 0.0f;
		scoreLabel_.anchorY = 1.0f;
		scoreLabel_.x = -Futile.screen.halfWidth + 50.0f;
		scoreLabel_.y = Futile.screen.halfHeight - 50.0f;
		scoreLabel_.color = Color.white;
		AddChild(scoreLabel_);
		
	}
	
	public void HandleGotBacteria(BacteriaBubble bacteria)
	{
	}
	
	public void CreateBacteria()
	{
	}
	
	protected void HandleUpdate()
	{
	}
	
	public void HandleMultiTouch(FTouch[] touches)
	{	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
