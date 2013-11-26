using UnityEngine;
using System.Collections;
using System;

public class LevelSelectPage : ImmunityPage, FMultiTouchableInterface
{
	public enum ButtonType{
		normal,
		locked
	}

	private FSprite background_;
	private FButton brain;
	private FButton lungs;
	private FButton stomach;

	private static readonly Vector2 BRAIN_POSITION = new Vector2(152, 134);
	private static readonly Vector2 LUNGS_POSITION = new Vector2(152, -80);
	private static readonly Vector2 STOMACH_POSITION = new Vector2(152, -290);

	public LevelSelectPage()
	{
		EnableMultiTouch();
		ListenForUpdate(HandleUpdate);
	}
	
	override public void Start()
	{
		background_ = new FSprite("level select screen final");
		AddChild(background_);
		setBrain(ButtonType.locked);
		setLungs(ButtonType.locked);

		stomach = new FButton("Stomach", "StomachPressed");
		stomach.SignalRelease += HandleStomachButton;
		AddChild(stomach);
		stomach.SetPosition(STOMACH_POSITION);

		background_.scale = 1f;

		Go.to(background_, 0.5f, new TweenConfig().setDelay(0.1f).floatProp("scale", 1.0f).setEaseType(EaseType.BackOut));
	}

	private void HandleBrainButton(FButton button){
	}

	private void HandleLungsButton(FButton button){
	}

	private void HandleStomachButton(FButton button){
		Application.LoadLevel("BubblePopDemo");
	}

	private void setBrain(ButtonType type){
		if(brain != null){
			RemoveChild(brain);
		}

		switch(type){
		case ButtonType.normal:
			brain = new FButton("Brain", "BrainPressed");
			brain.SignalRelease += HandleBrainButton;
			break;
		case ButtonType.locked:
			brain = new FButton("BrainLocked");
			break;
		}

		AddChild(brain);
		brain.SetPosition(BRAIN_POSITION);
	}

	private void setLungs(ButtonType type){
			if(lungs != null){
				RemoveChild(lungs);
			}
			
			switch(type){
			case ButtonType.normal:
				lungs = new FButton("Lungs", "LungsPressed");
				lungs.SignalRelease += HandleLungsButton;
				break;
			case ButtonType.locked:
				lungs = new FButton("LungsLocked");
				break;
			}
			
			AddChild(lungs);
			lungs.SetPosition(LUNGS_POSITION);
	}

	public void HandleMultiTouch(FTouch[] touches){
		foreach(FTouch touch in touches){
		}
	}

	protected void HandleUpdate(){
	}
}