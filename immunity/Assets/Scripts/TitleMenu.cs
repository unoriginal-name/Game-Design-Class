using UnityEngine;
using System;
using System.Collections;

public class TitleMenu : Menu
{
	private FSprite character;
	
	private FButton play_button;
	private FButton network_button;
	private FButton settings_button;
	private FButton about_button;
	
	public TitleMenu () : base()
	{}
	
	override public void Start() {
		character = new FSprite("hhhh");
		
		play_button.AddLabel("ImmunityFont", "Play", Color.white);
		network_button.AddLabel("ImmunityFont", "Network", Color.white);
		settings_button.AddLabel("ImmunityFont", "Settings", Color.white);
		about_button.AddLabel("ImmunityFont", "About", Color.white);
		
		play_button.SignalRelease += HandlePlayButtonRelease;
		network_button.SignalRelease += HandleNetworkButtonRelease;
		settings_button.SignalRelease += HandleSettingsButtonRelease;
		about_button.SignalRelease += HandleAboutButtonRelease;
		
		AddChild(play_button);
		AddChild(network_button);
		AddChild(settings_button);
		AddChild(about_button);
	}
	
	private void HandlePlayButtonRelease(FButton button) {
		
	}
	
	private void HandleNetworkButtonRelease(FButton button) {
		
	}
	
	private void HandleSettingsButtonRelease(FButton button) {
		
	}
	
	private void HandleAboutButtonRelease(FButton button) {
		
	}
}

/*		play_button.AddLabel("ImmunityFont", "Play", Color.white);
		network_button.AddLabel("ImmunityFont", "Network", Color.white);
		settings_button.AddLabel("ImmunityFont", "Settings", Color.white);
		about_button.AddLabel("ImmunityFont", "About", Color.white);
		
		play_button.SignalRelease += HandlePlayButtonRelease;
		network_button.SignalRelease += HandleNetworkButtonRelease;
		settings_button.SignalRelease += HandleSettingsButtonRelease;
		about_button.SignalRelease += HandleAboutButtonRelease;
		
		AddChild(play_button);
		AddChild(network_button);
		AddChild(settings_button);
		AddChild(about_button);
	}
	
	private void HandlePlayButtonRelease(FButton button) {
		
	}
	
	private void HandleNetworkButtonRelease(FButton button) {
		
	}
	
	private void HandleSettingsButtonRelease(FButton button) {
		
	}
	
	private void HandleAboutButtonRelease(FButton button) {
		
	}
}

 */