using UnityEngine;
using System.Collections;

public class GestureHandler : MonoBehaviour {
	//private const float DOUBLE_TAP_TIME = .5f;
	
	public GameObject gestureObject;
	
	private GestureInterpreter gestureInterpreter;
	
	//private float oldTapTime;
	private float newTapTime;
	
	void Start () {
		gestureInterpreter = gestureObject.GetComponent<GestureInterpreter>();
	}
	
	void Update () {
		checkMove();
	}
	
	private void checkMove(){
		if(newTapTime != gestureInterpreter.LastTimeStamp && gestureInterpreter.LastGesture == GestureInterpreter.gesture.tap){
			//oldTapTime = newTapTime;
			newTapTime = gestureInterpreter.LastTimeStamp;
			
			
			
			//if(newTapTime - oldTapTime <= DOUBLE_TAP_TIME){				
				Vector3 movePosition = gestureInterpreter.LastTapPosition;
				movePosition.z = 0;
				//print (movePosition.x + " " + movePosition.y);
				this.gameObject.transform.rigidbody.MovePosition(movePosition);
			//}
		}
	}
}