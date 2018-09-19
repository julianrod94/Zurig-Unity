using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularPattern {

	private static bool InCilinder = true;
	
	// Update is called once per frame

	public static Vector3 CilPos(float x, float y) {
		if(!InCilinder) { return new Vector3(x,0,y);}
		return new Vector3(
			GameConstants.Wold.Radius * Mathf.Cos(x),
			GameConstants.Wold.Radius *Mathf.Sin(x),
			y);
	}
}
