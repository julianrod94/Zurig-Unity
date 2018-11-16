using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurningZone : MonoBehaviour {
	public enum TurningDirection {
		LEFT,
		RIGHT
	}
	
	public bool LeftOpen;
	public bool RightOpen;
	public bool FrontOpen;
	public bool BackOpen;


	public bool canRotate(Transform transform, TurningDirection direction) {
		var angle = transform.eulerAngles.y;
		Debug.Log("rotating " + Mathf.DeltaAngle(angle,0));
		
		if (Math.Abs(Mathf.DeltaAngle(angle,0)) < 2) {
			//FORWARD
			switch (direction) {
				case TurningDirection.LEFT: return LeftOpen;
				case TurningDirection.RIGHT: return RightOpen;
			}
		}
		
		if (Math.Abs(Mathf.DeltaAngle(angle, 270)) < 2) {
			//LEFT
			switch (direction) {
				case TurningDirection.LEFT: return BackOpen;
				case TurningDirection.RIGHT: return FrontOpen;
			}
		}
		
		if (Math.Abs(Mathf.DeltaAngle(angle, 90)) < 2) {
			//RIGHT
			switch (direction) {
				case TurningDirection.LEFT: return FrontOpen;
				case TurningDirection.RIGHT: return BackOpen;
			}
		}
		
		if (Math.Abs(Mathf.DeltaAngle(angle ,180)) < 2) {
			//BACK
			switch (direction) {
				case TurningDirection.LEFT: return RightOpen;
				case TurningDirection.RIGHT: return LeftOpen;
			}
		}

		return false;
	}
	
}
