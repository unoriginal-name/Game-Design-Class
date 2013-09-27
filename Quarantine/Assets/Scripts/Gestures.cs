using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gestures : MonoBehaviour {
	private const int X_TOLERANCE = 500;
	private const int Y_TOLERANCE = 300;
	
	private const int UPDATE_DISTANCE = 100; // pixels
	
	public enum direction {
		none = 0,
		up,
		down,
		left,
		right
	};
	
	public enum gesture{
		tap = 0,
		up,
		down,
		left,
		right,
		upDown,
		upLeft,
		upRight,
		downUp,
		downLeft, 
		downRight,
		leftUp,
		leftDown,
		leftRight,
		rightUp,
		rightDown, 
		rightLeft,
		circleLeft,
		circleRight,
		twoFinger,
		other,
	}
	
	private float lastTimeStamp;
	public float LastTimeStamp{
		get{
			return lastTimeStamp;
		}
		set{}
	}
	
	private gesture lastGesture;
	public gesture LastGesture{
		get{
			return lastGesture;
		}
		set{}
	}
	
	private LinkedList<direction> lastPath;
	public LinkedList<direction> LastPath{
		get{
			return lastPath;
		}
		set{}
	}
	
	private void setGesture(){
		lastTimeStamp = Time.time;
		lastPath = path;
		lastPath.RemoveFirst();
		
		print (path.Count);
		if(lastPath.Count == 2){
			print (lastPath.First.Value);
			print (" ");
			print (lastPath.First.Next.Value);
		}
		
		if(Time.time - timer < TAP_TIME){
			lastGesture = gesture.tap;
		}else if(lastPath.Count == 1){
			switch(lastPath.First.Value){
			case direction.up:
				lastGesture = gesture.up;
				break;
			case direction.down:
				lastGesture = gesture.down;
				break;
			case direction.left:
				lastGesture = gesture.left;
				break;
			case direction.right:
				lastGesture = gesture.right;
				break;
			}
		}else if(lastPath.Count == 2){
			switch(lastPath.First.Value){
			case direction.up:
				switch(lastPath.First.Next.Value){
				case direction.down:
					lastGesture = gesture.upDown;
					break;
				case direction.left:
					lastGesture = gesture.upLeft;
					break;
				case direction.right:
					lastGesture = gesture.upRight;
					break;
				}
				break;
			case direction.down:
				switch(lastPath.First.Next.Value){
				case direction.up:
					lastGesture = gesture.downUp;
					break;
				case direction.left:
					lastGesture = gesture.downLeft;
					break;
				case direction.right:
					lastGesture = gesture.downRight;
					break;
				}
				break;
			case direction.left:
				switch(lastPath.First.Next.Value){
				case direction.up:
					lastGesture = gesture.leftUp;
					break;
				case direction.down:
					lastGesture = gesture.leftDown;
					break;
				case direction.right:
					lastGesture = gesture.leftRight;
					break;
				}
				break;
			case direction.right:
				switch(lastPath.First.Next.Value){
				case direction.up:
					lastGesture = gesture.rightUp;
					break;
				case direction.down:
					lastGesture = gesture.rightDown;
					break;
				case direction.left:
					lastGesture = gesture.rightLeft;
					break;
				}
				break;
			}
		}else{
			lastGesture = gesture.other;
			
			if(lastPath.Count == 4 || lastPath.Count == 5){
				direction[] pathArray = new direction[5];
				LinkedListNode<direction> next = lastPath.First;
				for(int i = 0; i < 5; i++){
					if(next != null){
						pathArray[i] = next.Value;
					}else{
						pathArray[i] = direction.none;	
					}	
					
					next = next.Next;
				}
				
				if(pathArray[0] == direction.left
					&& pathArray[1] == direction.down
					&& pathArray[2] == direction.right
					&& pathArray[3] == direction.up
					&& (pathArray[4] == direction.left || pathArray[4] == direction.none)){
					lastGesture = gesture.circleLeft;
				}else if(pathArray[0] == direction.right
					&& pathArray[1] == direction.down
					&& pathArray[2] == direction.left
					&& pathArray[3] == direction.up
					&& (pathArray[4] == direction.right || pathArray[4] == direction.none)){
					lastGesture = gesture.circleRight;
				}
			}
		}
	}
	
	private LineRenderer lineRenderer;
	private List<Vector3> linePoints = new List<Vector3>();
	
	private LinkedList<direction> path;
	private LinkedList<Vector3> pathPosition;
	
	private bool checkForRelease = false;
	private bool twoFingerInput = false;
	
	private float timer = 0;
	private const float MAX_TIME = 2f;
	private const float TAP_TIME = .100f;
	
	void Start () {
		lineRenderer = (LineRenderer)gameObject.AddComponent<LineRenderer>();
		lineRenderer.SetColors(Color.black,Color.black);
		lineRenderer.SetWidth(.01f, .01f); 
	}
	
	void Update () {
		if(!checkForRelease){
			linePoints = new List<Vector3>();
			updateLine();
			
			if(Input.GetMouseButtonDown(0)){
				checkForRelease = true;
			}
			
			if(Input.GetMouseButtonDown(1)){
				checkForRelease = true;
				twoFingerInput = true;
			}
			
			if(checkForRelease){
				timer = Time.time;	
				
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
					
					addGesture(Input.mousePosition.x,Input.mousePosition.y);
					setGesture();
						
				}else if(checkUpdateDistanceReached()){
					addGesture(Input.mousePosition.x,Input.mousePosition.y);
				}
			}else{
				if(Input.GetMouseButtonUp(1)){
					checkForRelease = false;
					twoFingerInput = false;
					
					lastTimeStamp = Time.time;
					lastPath = null;
					lastGesture = gesture.tap;
				}
			}
		}
	}
	
	public void addGesture(float mouseX, float mouseY){
		bool xChange = false;
		bool yChange = false;
	
		if(Mathf.Abs(mouseX - pathPosition.Last.Value.x) > X_TOLERANCE){
			xChange = true;
		}
		
		if(Mathf.Abs(mouseY - pathPosition.Last.Value.y) > Y_TOLERANCE){
			yChange = true;
		}
		
		if(xChange){
			if(mouseX > pathPosition.Last.Value.x && path.Last.Value != direction.right){
				path.AddLast (direction.right);
			}else if(path.Last.Value != direction.left){
				path.AddLast (direction.left);
			}else{
				xChange = false;	
			}
		}
		
		if(yChange){
			if(mouseY > pathPosition.Last.Value.y && path.Last.Value != direction.up){
				path.AddLast (direction.up);
			}else if(path.Last.Value != direction.down){
				path.AddLast (direction.down);
			}else{
				yChange = false;	
			}
		}
		
		if(xChange || yChange){
			pathPosition.AddLast (new Vector3(mouseX,mouseY,0));
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
	
	private bool checkUpdateDistanceReached(){
		return  Mathf.Sqrt(Mathf.Pow(pathPosition.Last.Value.x - Input.mousePosition.x,2) + Mathf.Pow(pathPosition.Last.Value.y - Input.mousePosition.y,2)) > UPDATE_DISTANCE;
	}
}