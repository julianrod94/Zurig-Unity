using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularPattern : MonoBehaviour {

	public bool InCilinder;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		var radius = 5;
		var x = Mathf.Sin(Time.time);
		var y = 0;

		if (InCilinder) {
			setPosition(5 * Mathf.Cos(Time.time), 5 * Mathf.Sin(Time.time));
		}
		else {
			transform.position = new Vector3(x,transform.position.y,y);
		}
	}

	void setPosition(float x, float y) {
		var radius = 9;
		transform.position = new Vector3(radius * Mathf.Cos(x/(Mathf.PI/2)), radius *Mathf.Sin(x/(Mathf.PI/2)), y);
	}
}
