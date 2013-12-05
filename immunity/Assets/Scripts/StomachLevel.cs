using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class StomachLevel : FContainer {
	
	private FSprite lake_sprite_;
	private FSprite cave_sprite_;
	
	private Rect bubble_container_size_;
	private FContainer bubbles_container_;
	private List<StomachBubble> stomach_bubbles_;
	
	private FSprite foreground_;
	
	private const int MAX_STOMACH_BUBBLES = 15;
	
	public StomachLevel()
	{
		lake_sprite_ = new FSprite("stomach_back");
		cave_sprite_ = new FSprite("stomach_mid");
		bubbles_container_ = new FContainer();
		foreground_ = new FSprite("stomach_fore");
		
		AddChild(lake_sprite_);
		AddChild(cave_sprite_);
		AddChild(bubbles_container_);
		AddChild(foreground_);
		
		bubble_container_size_ = new Rect(-Futile.screen.halfWidth*.75f, -Futile.screen.halfHeight*.75f, Futile.screen.width*.75f, Futile.screen.halfHeight*.75f);
		stomach_bubbles_ = new List<StomachBubble>();
	}
	
	public void Update () {
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
}
