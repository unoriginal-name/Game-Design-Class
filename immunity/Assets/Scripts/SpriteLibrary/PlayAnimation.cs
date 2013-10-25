using UnityEngine;
using System.Collections;

public class PlayAnimation : MonoBehaviour {
	
	public int columns = 4;
	public int rows = 3;
	public float framesPerSecond = 1f;
	
	// the current frame to display
	private int index = 0;

	// Use this for initialization
	void Start () {
		StartCoroutine(updateTiling());
		
		// set the tile size of the texture (in UV units), based on rows and columns
		Vector2 size = new Vector2(1f/columns, 1f/rows);
		renderer.sharedMaterial.SetTextureScale("_MainTex", size);
	}
	
	private IEnumerator updateTiling()
	{
		while(true)
		{
			// move to the next index
			if(index >= rows * columns)
				index = 0;
			
			Debug.Log ("index: " + index);
			
			// split into x and y indexes
			Vector2 offset = new Vector2((float)index/columns - (index/columns), // x index
				.6666f-(index/columns)/(float)rows);
			renderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
			
			index++;
			
			yield return new WaitForSeconds(1f/framesPerSecond);
		}
	}
}
