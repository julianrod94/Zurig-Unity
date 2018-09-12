using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	private static Vector3 infinitum = new Vector3(0,0,10);
	public float Speed;
	private AxisProvider _axis;
	private Position _position;

	private const String axisName = "Horizontal";
	// Use this for initialization
	void Awake () {
		_axis = new NormalAxis();
		_position = new Position(transform, 0,0);
	}
	
	// Update is called once per frame
	void Update () {
		var x = _axis.GetAxis(axisName) * Time.deltaTime * GameConstants.Player.Speed;
		_position.Translate(x,0);
		transform.LookAt(infinitum);
	}
}
