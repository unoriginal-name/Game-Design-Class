using UnityEngine;
using System.Collections;
using System;

public class TitlePage : ImmunityPage, FMultiTouchableInterface
{
	private FSprite background_;
	private Rect play_button_rect_;
	private Rect settings_button_rect_;
	
	public TitlePage()
	{
		EnableMultiTouch();
		ListenForUpdate(HandleUpdate);
	}
	
	override public void Start()
	{
		FSoundManager.StopMusic();
		FSoundManager.UnloadAllSoundsAndMusic();
		FSoundManager.PlayMusic("background_music");
		FSoundManager.PreloadSound("button_click");
		
		background_ = new FSprite("start_screen");
		AddChild(background_);
		
		background_.scale = 0.0f;
		
		Go.to(background_, 0.5f, new TweenConfig().setDelay(0.1f).floatProp("scale", 1.0f).setEaseType(EaseType.BackOut));
		
		play_button_rect_ = new Rect(Futile.screen.halfWidth*.15f, Futile.screen.halfHeight*-0.05f, Futile.screen.halfWidth*.35f, Futile.screen.halfHeight*.3f);
		settings_button_rect_ = new Rect(Futile.screen.halfWidth*.15f, Futile.screen.halfHeight*-0.45f, Futile.screen.halfWidth*.6f, Futile.screen.halfHeight*.3f);
		
	}
	
	private void HandlePlayButtonRelease(FButton button)
	{
		Debug.Log("Play Button Clicked");
		Application.LoadLevel("BubblePopDemo");
	}
	
	private void HandleHowToPlayButtonRelease(FButton button)
	{
		Debug.Log("How To Play Button Clicked");
	}
	
	private void HandleSettingsButtonRelease(FButton button)
	{
		Debug.Log("Settings Button Clicked");
	}
	
	protected void HandleUpdate()
	{
	}
	
	public void HandleMultiTouch(FTouch[] touches)
	{
		
		foreach(FTouch touch in touches)
		{
			if(touch.phase == TouchPhase.Ended)
			{
				if(play_button_rect_.xMin <= touch.position.x && play_button_rect_.xMax >= touch.position.x &&
				play_button_rect_.yMin <= touch.position.y && play_button_rect_.yMax >= touch.position.y)
				{
					Debug.Log("Play button clicked");
					Application.LoadLevel("BubblePopDemo");
				}
				else if(settings_button_rect_.xMin <= touch.position.x && settings_button_rect_.xMax >= touch.position.x &&
				settings_button_rect_.yMin <= touch.position.y && settings_button_rect_.yMax >= touch.position.y)
				{
					Debug.Log("Settings button clicked");	
				}
			}
			
		}
	}
}