using UnityEngine;
using System.Collections;

public class Dust : FSprite {
	
	private bool tween_complete_ = true;
	
	private const float DUST_SPEED = 0.01f;
	
	private Rect container_;

	// Use this for initialization
	public Dust (string sprite_name, Rect container) : base(sprite_name) {
		ListenForUpdate(HandleUpdate);
		
		this.scale = 5.0f;
		this.alpha = .25f;
		
		container_ = container;
	}
	
	// Update is called once per frame
	public void HandleUpdate () {
		if(tween_complete_)
		{
			tween_complete_ = false;
				
			float x_pos = Random.value*container_.width + container_.xMin;
			float y_pos = Random.value*container_.height + container_.yMin;
				
			float distance = Mathf.Sqrt((x_pos - this.x)*(x_pos - this.x) + (y_pos - this.y)*(y_pos - this.y));
				
			float duration = distance/(Dust.DUST_SPEED*(Futile.screen.height + Futile.screen.width)/2.0f);
			Go.to(this, duration, new TweenConfig().floatProp("x", x_pos).floatProp("y", y_pos).onComplete(orignalTween => tween_complete_ = true));
		}
	}
}
