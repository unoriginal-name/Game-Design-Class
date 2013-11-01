using UnityEngine;
using System.Collections;
using System;

public class TitlePage : ImmunityPage
{
	private FSprite background_;
	private FButton playButton_;
	private FButton howToPlayButton_;
	private FButton settingsButton_;
	
	public TitlePage()
	{
		ListenForUpdate(HandleUpdate);
	}
	
	override public void Start()
	{
		background_ = new FSprite("hhhh");
		AddChild(background_);
		
		background_.scale = 0.25f;
		background_.x = background_.width/2.0f - Futile.screen.halfWidth;
		background_.y = background_.height/2.0f - Futile.screen.halfHeight;
		
		playButton_ = new FButton("BigButton", "BigButton"); // "BigButton", "ButtonClickSound");
		playButton_.AddLabel("ImmunityFont", "Play", Color.white);
		AddChild(playButton_);
		
		
		playButton_.SignalRelease += HandlePlayButtonRelease;
		
		playButton_.scale = 0.0f;
		
		Go.to(playButton_, 0.5f, new TweenConfig().setDelay(0.1f).floatProp("scaleX", 1.0f).setEaseType(EaseType.BackOut));
		Go.to(playButton_, 0.5f, new TweenConfig().setDelay(0.1f).floatProp("scaleY", 0.5f).setEaseType(EaseType.BackOut));
		
		howToPlayButton_ = new FButton("BigButton", "BigButton"); // "BigButton", "ButtonClickSound");
		howToPlayButton_.AddLabel("ImmunityFont", "How To Play", Color.white);
		AddChild(howToPlayButton_);
		
		howToPlayButton_.SignalRelease += HandleHowToPlayButtonRelease;
		
		howToPlayButton_.scale = 0.0f;
		
		Go.to(howToPlayButton_, 0.5f, new TweenConfig().setDelay(0.1f).floatProp("scaleX", 1.0f).setEaseType(EaseType.BackOut));
		Go.to(howToPlayButton_, 0.5f, new TweenConfig().setDelay(0.1f).floatProp("scaleY", 0.5f).setEaseType(EaseType.BackOut));

		
		settingsButton_ = new FButton("BigButton", "BigButton"); // "BigButton", "ButtonClickSound");
		settingsButton_.AddLabel("ImmunityFont", "Settings", Color.white);
		AddChild(settingsButton_);
		
		
		settingsButton_.SignalRelease += HandleSettingsButtonRelease;
		
		settingsButton_.scale = 0.0f;
		
		Go.to(settingsButton_, 0.5f, new TweenConfig().setDelay(0.1f).floatProp("scaleX", 1.0f).setEaseType(EaseType.BackOut));
		Go.to(settingsButton_, 0.5f, new TweenConfig().setDelay(0.1f).floatProp("scaleY", 0.5f).setEaseType(EaseType.BackOut));
		
		
		playButton_.y = (howToPlayButton_.hitRect.yMax - howToPlayButton_.hitRect.yMin)/2.0f + 50.0f ;
		settingsButton_.y = -((howToPlayButton_.hitRect.yMax - howToPlayButton_.hitRect.yMin)/2.0f + 50.0f);
	}
	
	private void HandlePlayButtonRelease(FButton button)
	{
		Debug.Log("Play Button Clicked");
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
}