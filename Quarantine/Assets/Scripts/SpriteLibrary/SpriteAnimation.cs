using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SpriteAnimation {
	
	// TODO: Think about getting rid of the redundancy between num_frames and sequence
	
	public Texture2D sprite_sheet; // texture to use for the sprites
	public int rows = 0; // number of rows of sprites in specified sheet
	public int cols = 0; // number of columns of sprites in specified sheet
	public int num_frames = 0; // number of frames (this is only used if there are missing sprites in a row (e.g. 14 sprites in a 4x4 sprite sheet)
	public List<int> sequence; // defaults to in order, but if you need to play frames out of order then specify it here
	public float default_fps = 24; // defaults to 24 frames, but changes this if needed. Animator will play at different speeds, but will default to this number
	
	// TODO: Figure out how to get the constructor to use the values set in the Unity Editor
	public void Init()
	{
		// if no num_frames (or invalid num_frames) specified then get it from
		// the number of rows and columns
		if(num_frames == 0 || num_frames > rows*cols)
			num_frames = rows*cols;
		
		// only if not set in editor
		//if(sequence == null)
		//	sequence = new List<int>(); // initialize the list so it can be used below
		
		// if no sequence specified then just play
		// the indicies in order
		if(sequence.Count == 0)
		{
			for(int i=0; i<num_frames; i++)
			{
				sequence.Add (i);	
			}
		} else {
			// if there is already a sequence then
			// set the number of frames equal to the sequence length
			num_frames = sequence.Count;	
		}
	}
}
