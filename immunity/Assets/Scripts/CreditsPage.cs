using UnityEngine;
using System.Collections;

public class CreditsPage : ImmunityPage, FMultiTouchableInterface
{
	private FSprite background_;

	public CreditsPage()
	{
		EnableMultiTouch();
		ListenForUpdate(HandleUpdate);
	}
	
	override public void Start()
	{		
		// GUI objects
		background_ = new FSprite("CreditsMenu");
		AddChild(background_);
		background_.scale = 0.0f;

		Go.to(background_, 0.5f, new TweenConfig().setDelay(0.1f).floatProp("scale", 1.0f).setEaseType(EaseType.BackOut));
	}

	public void HandleMultiTouch(FTouch[] touches){
		foreach(FTouch touch in touches){	
			if(touch.phase == TouchPhase.Ended){
			}			
		}
	}
	
	protected void HandleUpdate(){
	}
}