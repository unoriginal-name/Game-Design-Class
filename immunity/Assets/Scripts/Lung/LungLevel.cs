using UnityEngine;
using System.Collections;

public class LungLevel : FContainer {
	
	private FAnimatedSprite background_;
	
	private Dust dust1_;
	
	private FSprite tubes1_;
	private FSprite tubes2_;
	
	private Dust dust2_;
	
	private FSprite tubes3_;
	private FSprite tubes4_;
	
	private Dust dust3_;
	
	private Rect dust_container_size_;
	
	private bool dust1_tween_complete_ = true;
	private bool dust2_tween_complete_ = true;	
	private const float DUST_SPEED = 0.01f;
	
	public LungLevel()
	{
		background_ = new FAnimatedSprite("Lungs_Background");
		int[] breathing_frames = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
		FAnimation breathing_animation = new FAnimation("breathing", "Lungs_Background", breathing_frames, 250, true);
		background_.addAnimation(breathing_animation);
		background_.play("breathing");
		
		AddChild(background_);
		
		dust_container_size_ = new Rect(-Futile.screen.halfWidth*2.5f, -Futile.screen.halfHeight*2.5f, Futile.screen.width*2.5f, Futile.screen.height*2.5f);
		
		dust1_ = new Dust("Dust_4", dust_container_size_);
		AddChild(dust1_);
		
		tubes1_ = new FSprite("Lungs_Rear");
		AddChild(tubes1_);
		tubes2_ = new FSprite("Lungs_MidBack1");
		AddChild(tubes2_);
		
		dust2_ = new Dust("Dust_3", dust_container_size_);
		AddChild(dust2_);
		
		tubes3_ = new FSprite("Lungs_MiddleFore");
		AddChild(tubes3_);
		tubes4_ = new FSprite("Lungs_Fore");
		AddChild(tubes4_);
		
		dust3_ = new Dust("Dust_2", dust_container_size_);
		AddChild(dust3_);
		
	}
	
	public void Update () {
		
		dust1_.HandleUpdate();
		dust2_.HandleUpdate();
		dust3_.HandleUpdate();
	}
}
