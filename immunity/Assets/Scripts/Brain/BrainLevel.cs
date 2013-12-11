﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BrainLevel : Level {
	
	FSprite background_;

	FParallaxContainer background;
	FParallaxContainer foreground;

	FParallaxContainer back_neurons_container_;
	List<Neuron> back_neurons_;
	Rect back_neurons_area_;
	
	FSprite foreground_;
	
	FParallaxContainer front_neurons_container_;
	List<Neuron> front_neurons_;
	Rect front_neurons_area_;
	
	private float last_firing_time_;

	Vector2 backgroundContainerSize = new Vector2(Futile.screen.width * 1.5f, Futile.screen.height * 1.5f);
	Vector2 foregroundContainerSize = new Vector2(Futile.screen.width * 2f, Futile.screen.height * 2f);

	public BrainLevel()
	{		
		background_ = new FSprite("Background");
		//AddChild(background_);
		background = new FParallaxContainer();
		background.AddChild (background_);
		AddChild (background);

		background.setSize (backgroundContainerSize);
		
		back_neurons_container_ = new FParallaxContainer();
		back_neurons_ = new List<Neuron>();
		back_neurons_area_ = new Rect(-Futile.screen.halfWidth, 0, Futile.screen.width, Futile.screen.halfHeight);
		AddChild(back_neurons_container_);

		back_neurons_container_.setSize (backgroundContainerSize);
		
		for(int i=0; i<50; i++)
		{
			bool is_fast = (Random.value > .5f);
			float opacity_chance = Random.value;
			Neuron.Opacity opacity;
			if(opacity_chance < .33f)
			{
				opacity = Neuron.Opacity.PERCENT_40;
			}
			else if(opacity_chance < .66f)
			{
				opacity = Neuron.Opacity.PERCENT_60;
			}
			else
			{
				opacity = Neuron.Opacity.PERCENT_80;
			}
			
			Neuron neuron = new Neuron(is_fast, opacity);
			
			float x_pos = Random.value*back_neurons_area_.width + back_neurons_area_.xMin;
			float y_pos = Random.value*back_neurons_area_.height + back_neurons_area_.yMin;
			
			neuron.SetPosition(new Vector2(x_pos, y_pos));
			
			back_neurons_container_.AddChild(neuron);
			back_neurons_.Add(neuron);
			
			last_firing_time_ = Time.time;
		}
		
		foreground_ = new FSprite("Forground");
		//AddChild(foreground_);
		foreground = new FParallaxContainer();
		foreground.AddChild (foreground_);
		AddChild (foreground);

		foreground.setSize (foregroundContainerSize);
		
		front_neurons_container_ = new FParallaxContainer();
		front_neurons_ = new List<Neuron>();
		front_neurons_area_ = new Rect(-Futile.screen.halfWidth, -Futile.screen.halfHeight, Futile.screen.width, Futile.screen.halfHeight*.5f);
		AddChild(front_neurons_container_);

		front_neurons_container_.setSize(foregroundContainerSize);
		
		for(int i=0; i<0; i++)
		{
			bool is_fast = (Random.value > .5f);
			float opacity_chance = Random.value;
			Neuron.Opacity opacity;
			if(opacity_chance < .33f)
			{
				opacity = Neuron.Opacity.PERCENT_40;
			}
			else if(opacity_chance < .66f)
			{
				opacity = Neuron.Opacity.PERCENT_60;
			}
			else
			{
				opacity = Neuron.Opacity.PERCENT_80;
			}
			
			Neuron neuron = new Neuron(is_fast, opacity);
			
			float x_pos = Random.value*front_neurons_area_.width + front_neurons_area_.xMin;
			float y_pos = Random.value*front_neurons_area_.height + front_neurons_area_.yMin;
			
			neuron.SetPosition(new Vector2(x_pos, y_pos));
			
			front_neurons_container_.AddChild(neuron);
			front_neurons_.Add(neuron);
		}
	}
	
	// Update is called once per frame
	public override void Update () {
		
		if(Time.time - last_firing_time_ > 1.0f)
		{
			last_firing_time_ = Time.time;
			for(int n=0; n<back_neurons_.Count; n++)
			{
				if(Random.value < .2f)
				{
					if(back_neurons_[n].FinishedCount >= 1)
						back_neurons_[n].play("spark", true);
				}
			}
			
			for(int n=0; n<front_neurons_.Count; n++)
			{
				if(Random.value < .2f)
				{
					if(back_neurons_[n].FinishedCount >= 1)
						front_neurons_[n].play("spark", true);
				}
			}
		}
	}
}
