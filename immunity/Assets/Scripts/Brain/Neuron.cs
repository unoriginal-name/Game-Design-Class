using UnityEngine;
using System.Collections;

public class Neuron : FAnimatedSprite {
	
	public enum Opacity { PERCENT_40, PERCENT_60, PERCENT_80 };
		
	public Neuron(bool fast, Opacity opacity) : base("NeuronSlow40")
	{
		ListenForUpdate(HandleUpdate);
		
		scale = RXRandom.Range(0.1f, 0.25f);
		rotation = RXRandom.Range(-180.0f, 180.0f);
		
		int[] frames;
		string anim_name;
		if(fast)
		{
			frames = new int[10];
			for(int i=0; i<10; i++)
				frames[i] = i+1;
			
			switch(opacity)
			{
			case Opacity.PERCENT_40:
				anim_name = "NeuronFast40";
				break;
			case Opacity.PERCENT_60:
				anim_name = "NeuronFast60";
				break;
			case Opacity.PERCENT_80:
				anim_name = "NeuronFast80";
				break;
			default:
				anim_name = "NeuronFast40";
				break;
			}
		}
		else
		{
			frames = new int[15];
			for(int i=0; i<15; i++)
				frames[i] = i+1;
			
			switch(opacity)
			{
			case Opacity.PERCENT_40:
				anim_name = "NeuronSlow40";
				break;
			case Opacity.PERCENT_60:
				anim_name = "NeuronSlow60";
				break;
			case Opacity.PERCENT_80:
				anim_name = "NeuronSlow80";
				break;
			default:
				anim_name = "NeuronFast40";
				break;
			}
		}
		
		FAnimation animation = new FAnimation("spark", anim_name, frames, 100, false);
		base.addAnimation(animation);
	}
	
	// Update is called once per frame
	public void HandleUpdate () {
		base.Update();
	}
}
