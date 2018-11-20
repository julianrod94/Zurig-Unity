using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurningZone : MonoBehaviour {
	public enum TurningDirection {
		LEFT,
		RIGHT
	}

	public enum Wall
	{
		NORTH,
		SOUTH,
		EAST,
		WEST
	}
	
	public bool LeftOpen;
	public bool RightOpen;
	public bool FrontOpen;
	public bool BackOpen;

	public GameObject NWall;
	public GameObject SWall;
	public GameObject EWall;
	public GameObject WWall;

	private void Awake() {
		if(LeftOpen) WWall.SetActive(false);
		if(RightOpen) EWall.SetActive(false);
		if(FrontOpen) NWall.SetActive(false);
		if(BackOpen) SWall.SetActive(false);
		}


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

	public void EnableWall(Wall wall)
	{
		if (wall == Wall.WEST)
		{
			LeftOpen = true;
			WWall.SetActive(false);
		}

		if (wall == Wall.EAST)
		{
			RightOpen = true;
			EWall.SetActive(false);
		}

		if (wall == Wall.NORTH)
		{
			FrontOpen = true;
			NWall.SetActive(false);
		}

		if (wall == Wall.SOUTH)
		{
			BackOpen = true;
			SWall.SetActive(false);
		}
	}
	
	
}
