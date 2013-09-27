using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TouchTest : MonoBehaviour {
	private Interface interfaceScript;
	private LineRenderer lineRenderer;
	private List<Vector3> linePoints = new List<Vector3>();
	
	//Vector3 oldMouse;
	
	private const int X_TOLERANCE = 500;
	private const int Y_TOLERANCE = 300;
	
	private const int UPDATE_DISTANCE = 100; // pixels
	
	private enum direction {
		none = 0,
		up,
		down,
		left,
		right
	};
	
	private LinkedList<direction> path;
	private LinkedList<Vector3> pathPosition;
	
	private const string METHOD = "changeLabel";
	
	private bool checkForRelease = false;
	private bool twoFingerInput = false;
	
	private float timer = 0;
	private const float MAX_TIME = 2f;
	private const float TAP_TIME = .100f;
	//private Vector2 start;
	//private Vector2 end;
	
	void Start () {
		interfaceScript = (Interface)GameObject.Find("Interface").GetComponent<Interface>();
		lineRenderer = (LineRenderer)gameObject.AddComponent<LineRenderer>();
		
		lineRenderer.SetColors(Color.black,Color.black);
		lineRenderer.SetWidth(.01f, .01f); 
	}
	
	void Update () {
		if (interfaceScript == null) {
			interfaceScript = (Interface)GameObject.Find("Interface").GetComponent<Interface>();	
			return;
		}
		
		lineRenderer.renderer.material = (Material)Resources.Load("Steaksauce");
	
		
		if(!checkForRelease){
			linePoints = new List<Vector3>();
			updateLine();
			
			
			if(Input.GetMouseButtonDown(0)){
				checkForRelease = true;
				//start = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
			}
			
			if(Input.GetMouseButtonDown(1)){
				checkForRelease = true;
				twoFingerInput = true;
			}
			
			if(checkForRelease){
				timer = Time.time;	
				//oldMouse = Input.mousePosition;
				
				path = new LinkedList<direction>();
				pathPosition = new LinkedList<Vector3>();
				path.AddLast(direction.none);
				pathPosition.AddLast (Input.mousePosition);
			}
		}else{
			updateLine();
			
			if(!twoFingerInput){
				if(Input.GetMouseButtonUp(0)){
					checkForRelease = false;
					
					if(Time.time - timer < TAP_TIME){
						interfaceScript.SendMessage(METHOD,new LabelHolder("Tap",Interface.labelIndices.touch1));
					}else{
						findGesture(Input.mousePosition.x,Input.mousePosition.y);
					}
						//interfaceScript.SendMessage(METHOD,new LabelHolder("Long Press",Interface.labelIndices.touch1));	
				
				}else if(updateDistanceReached()){
					findGesture(Input.mousePosition.x,Input.mousePosition.y);
				}
			}else{
				if(Input.GetMouseButtonUp(1)){
					checkForRelease = false;
					twoFingerInput = false;
					interfaceScript.SendMessage(METHOD,new LabelHolder("More than 1 finger.",Interface.labelIndices.touch1));	
				}
			}
		}
	}
	
	private bool updateDistanceReached(){
		return  Mathf.Sqrt(Mathf.Pow(pathPosition.Last.Value.x - Input.mousePosition.x,2) + Mathf.Pow(pathPosition.Last.Value.y - Input.mousePosition.y,2)) > UPDATE_DISTANCE;
	}
	
	public void findGesture(float x, float y){
		bool xChange = false;
		bool yChange = false;
	
		if(Mathf.Abs(x - pathPosition.Last.Value.x) > X_TOLERANCE){
			xChange = true;
		}
		
		if(Mathf.Abs(y - pathPosition.Last.Value.y) > Y_TOLERANCE){
			yChange = true;
		}
		
		if(xChange){
			if(x > pathPosition.Last.Value.x){
				interfaceScript.SendMessage(METHOD,new LabelHolder("Swipe Right",Interface.labelIndices.touch1));
				path.AddLast (direction.right);
			}else{
				interfaceScript.SendMessage(METHOD,new LabelHolder("Swipe Left",Interface.labelIndices.touch1));
				path.AddLast (direction.left);
			}
		}
		
		if(yChange){
			if(y > pathPosition.Last.Value.y){
				interfaceScript.SendMessage(METHOD,new LabelHolder("Swipe Up",Interface.labelIndices.touch1));
				path.AddLast (direction.up);
			}else{
				interfaceScript.SendMessage(METHOD,new LabelHolder("Swipe Down",Interface.labelIndices.touch1));	
				path.AddLast (direction.down);
			}
		}
		
		/*
		if(!xChange && !yChange){
			interfaceScript.SendMessage(METHOD,new LabelHolder("Not a Gesture",Interface.labelIndices.touch1));
		}*/
		
		if(xChange || yChange){
			pathPosition.AddLast (new Vector3(x,y,0));
		}
	}
	
	private void updateLine(){
		Vector3 mousePosition = Input.mousePosition;
		mousePosition.z = Camera.main.nearClipPlane;
		linePoints.Add(Camera.main.ScreenToWorldPoint(mousePosition));
		
		int i = 0;
		lineRenderer.SetVertexCount(linePoints.Count);
		foreach(Vector3 point in linePoints){
			lineRenderer.SetPosition(i,point);
			i++;
		}
	}
}