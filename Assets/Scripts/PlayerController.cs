using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	private static Vector3 _infinitum = new Vector3(0,0,10);
	public bool IsShooting;
	public float ShootCooldownTime;

	private AxisProvider _axis;
	private Position _position;
	private Cooldown _shootCD;

	private const String axisName = "Horizontal";

	void Awake () {
		_axis = new NormalAxis();
		_position = new Position(transform, 0,0);
		_shootCD = new Cooldown(ShootCooldownTime);
	}

	void Update () {
		var x = _axis.GetAxis(axisName) * Time.deltaTime * GameConstants.Player.Speed;
		Debug.Log(_axis.GetAxis(axisName));
		_position.Translate(x,0);
	}

	private void Shoot() {
		if(!IsShooting) { return; }
		BulletManager.Instance.GenerateBullet(_position, Quaternion.identity);
		_shootCD.Use();
	}
}
