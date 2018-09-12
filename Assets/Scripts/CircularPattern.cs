using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularPattern {

	public static float radius = 5;
	private static bool InCilinder = true;
	
	// Update is called once per frame

	public static Vector3 CilPos(float x, float y) {
		if(!InCilinder) { return new Vector3(x,0,y);}
		return new Vector3(radius * Mathf.Cos(x), radius *Mathf.Sin(x), y);
	}
}
