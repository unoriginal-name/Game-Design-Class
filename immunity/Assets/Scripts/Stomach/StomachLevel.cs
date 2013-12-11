using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class StomachLevel : Level {
	
	private FSprite lake_sprite_;
	private FSprite cave_sprite_;

	private FParallaxContainer background;
	private FParallaxContainer stomachCave;
	private FParallaxContainer foreground;
	
	private Rect bubble_container_size_;
	private FParallaxContainer bubbles_container_;
	private List<StomachBubble> stomach_bubbles_;
	
	private FSprite foreground_;
	
	private const int MAX_STOMACH_BUBBLES = 15;
	
	public StomachLevel()
	{
		lake_sprite_ = new FSprite("stomach_back");
		cave_sprite_ = new FSprite("stomach_mid");
		bubbles_container_ = new FParallaxContainer();
		foreground_ = new FSprite("stomach_fore");

		background = new FParallaxContainer();
		stomachCave = new FParallaxContainer();
		foreground = new FParallaxContainer();

		background.AddChild (lake_sprite_);
		stomachCave.AddChild (cave_sprite_);
		foreground.AddChild (foreground_);

		background.setSize (new Vector2 (Futile.screen.width * 1.5f, Futile.screen.height * 1.5f));
		bubbles_container_.setSize(new Vector2 (Futile.screen.width *1.5f, Futile.screen.height * 1.5f));
		foreground.setSize (new Vector2 (Futile.screen.width * 2f, Futile.screen.height * 2f));

		AddChild(background);
		AddChild(stomachCave);
		AddChild(bubbles_container_);
		AddChild(foreground);

		bubble_container_size_ = new Rect(-Futile.screen.halfWidth*.75f, -Futile.screen.halfHeight*.75f, Futile.screen.width*.75f, Futile.screen.halfHeight*.75f);
		stomach_bubbles_ = new List<StomachBubble>();
	}
	
	public override void Update () {
		if(stomach_bubbles_.Count < StomachLevel.MAX_STOMACH_BUBBLES)
		{
			Debug.Log("less stomach bubbles than max amount");
			float new_bubble_chance = Random.value;
			if(new_bubble_chance > .5f)
			{
				Debug.Log("Creating a new bubble");
				StomachBubble new_bubble = new StomachBubble();
				stomach_bubbles_.Add(new_bubble);
				
				float x_pos = Random.value;
				float y_pos = Random.value;
				
				new_bubble.SetPosition(x_pos*bubble_container_size_.width + bubble_container_size_.xMin,
					y_pos*bubble_container_size_.height + bubble_container_size_.yMin);
				bubbles_container_.AddChild(new_bubble);
			}
		}
		
		for(int b = stomach_bubbles_.Count-1; b >= 0; b--)
		{
			StomachBubble bubble = stomach_bubbles_[b];
			if(bubble.Finished)
			{
				bubbles_container_.RemoveChild(bubble);
				stomach_bubbles_.Remove(bubble);
			}
		}
	}

	public override FParallaxContainer getBackground()
	{
		return background;
	}

	public override FParallaxContainer getMid()
	{
		return stomachCave;
	}

	public override FParallaxContainer getForeground()
	{
		return foreground;
	}

	public override FParallaxContainer getAnimations()
	{
		return bubbles_container_;
	}
}
