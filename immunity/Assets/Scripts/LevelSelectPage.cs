using UnityEngine;
using System.Collections;
using System;

/* Notes:
 * 		Setting button state images:
 * 			use setBrain(), setLungs(), setStomach()
 * 			these methods require a ButtonType parameter
 */

public class LevelSelectPage : ImmunityPage, FMultiTouchableInterface
{
	public enum ButtonType{
		normal,
		pressed,
		locked
	}

	private FSprite background_;
	private FSprite brain;
	private FSprite lungs;
	private FSprite stomach;

	private static readonly Vector2 BRAIN_POSITION = new Vector2(152, 134);
	private static readonly Vector2 LUNGS_POSITION = new Vector2(152, -80);
	private static readonly Vector2 STOMACH_POSITION = new Vector2(152, -290);

	private readonly Rect BRAIN_RECT = new Rect(BRAIN_POSITION.x - 175, BRAIN_POSITION.y - 75, 350, 150);
	private readonly Rect LUNGS_RECT = new Rect(LUNGS_POSITION.x - 150, LUNGS_POSITION.y - 75, 295, 156);
	private readonly Rect STOMACH_RECT = new Rect(STOMACH_POSITION.x - 210, STOMACH_POSITION.y - 75, 425, 128);
	
	public LevelSelectPage()
	{
		EnableMultiTouch();
		ListenForUpdate(HandleUpdate);
	}
	
	override public void Start()
	{
		/*
		FSoundManager.StopMusic();
		FSoundManager.UnloadAllSoundsAndMusic();
		FSoundManager.PlayMusic("background_music");
		FSoundManager.PreloadSound("button_click");
		*/

		background_ = new FSprite("level select screen final");
		AddChild(background_);
		setBrain(ButtonType.normal);
		setLungs(ButtonType.normal);
		setStomach(ButtonType.normal);

		background_.scale = 1f;

		Go.to(background_, 0.5f, new TweenConfig().setDelay(0.1f).floatProp("scale", 1.0f).setEaseType(EaseType.BackOut));
}
	protected void HandleUpdate()
	{
	}

	public void HandleMultiTouch(FTouch[] touches)
	{
		foreach(FTouch touch in touches)
		{
			if(touch.phase == TouchPhase.Began){
				// Pressed
				if(checkPressed(BRAIN_RECT, touch.position))
				{
					Debug.Log("Brain button pressed");
					setBrain(ButtonType.pressed);
				}else if(checkPressed(LUNGS_RECT,touch.position)){
					Debug.Log("Lungs button pressed");
					setLungs(ButtonType.pressed);
				}else if(checkPressed(STOMACH_RECT,touch.position)){
					Debug.Log("Stomach button pressed");
					setStomach(ButtonType.pressed);
				}
			}else if(touch.phase == TouchPhase.Ended)
			{	//Released
				if(checkPressed(BRAIN_RECT, touch.position))
				{
					Debug.Log("Brain button released");
					/** REMOVE Setting it to normal when you add functionality or it looks like it wasn't pressed**/
					setBrain(ButtonType.normal);
				}else if(checkPressed(LUNGS_RECT,touch.position)){
					Debug.Log("Lungs button released");
					/** REMOVE Setting it to normal when you add functionality or it looks like it wasn't pressed**/
					setLungs(ButtonType.normal);
				}else if(checkPressed(STOMACH_RECT,touch.position)){
					Debug.Log("Stomach button released");

					Application.LoadLevel("BubblePopDemo");
				}
			}
		}
	}

	private void setBrain(ButtonType type){
		if(brain != null){
			RemoveChild(brain);
		}

		switch(type){
		case ButtonType.normal:
			brain = new FSprite("Brain");
			break;
		case ButtonType.pressed:
			brain = new FSprite("BrainPressed");
			break;
		case ButtonType.locked:
			brain = new FSprite("BrainLocked");
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
				lungs = new FSprite("Lungs");
				break;
			case ButtonType.pressed:
				lungs = new FSprite("LungsPressed");
				break;
			case ButtonType.locked:
				lungs = new FSprite("LungsLocked");
				break;
			}
			
			AddChild(lungs);
			lungs.SetPosition(LUNGS_POSITION);
	}

	private void setStomach(ButtonType type){
			if(stomach != null){
				RemoveChild(stomach);
			}
			
			switch(type){
			case ButtonType.normal:
			case ButtonType.locked:
				stomach = new FSprite("Stomach");
				break;
			case ButtonType.pressed:
				stomach = new FSprite("StomachPressed");
				break;
			}
			
			AddChild(stomach);
			stomach.SetPosition(STOMACH_POSITION);
	}

	public Boolean checkPressed(Rect buttonBox, Vector2 touchPosition){
		return(buttonBox.xMin <= touchPosition.x 
		       && buttonBox.xMax >= touchPosition.x 
		       && buttonBox.yMin <= touchPosition.y 
		       && buttonBox.yMax >= touchPosition.y);
	}

}