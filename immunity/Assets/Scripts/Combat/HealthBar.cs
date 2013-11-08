using UnityEngine;
using System.Collections;

public class HealthBar : FContainer {
	
	private FSprite overlay_;
	private FSprite background_;
	
	private float percentage_;
	
	public HealthBar()
	{
		percentage_ = 1.0f;
		
		background_ = new FSprite("HealthBar_background");
		overlay_ = new FSprite("HealthBar_overlay");
		
		background_.anchorX = 0.0f;
		overlay_.anchorX = 0.0f;
		
		AddChild(background_);
		AddChild(overlay_);
		
	}
	
	public float Percentage
	{
		get { return percentage_; }
		set { 
			percentage_ = value; 
			overlay_.scaleX = percentage_; 
		}
	}
	
	public float width
	{
		get { return background_.width; }
	}

	public float height
	{
		get { return background_.height; }
	}
	
}
