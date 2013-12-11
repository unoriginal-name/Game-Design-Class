using UnityEngine;
using System.Collections;

public class Level : FContainer {

	public Level()
	{
		
	}
	
	public virtual void Update()
	{
		
	}

	public virtual FParallaxContainer getBackground()
	{
		return new FParallaxContainer();
	}

	public virtual FParallaxContainer getMid()
	{
		return new FParallaxContainer();
	}

	public virtual FParallaxContainer getForeground()
	{
		return new FParallaxContainer();
	}

	public virtual FParallaxContainer getAnimations()
	{
		return new FParallaxContainer();
	}	
}
