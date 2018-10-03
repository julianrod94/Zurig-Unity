using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour {

	private double _speedY;
	private bool _isJumping;

	private float _lastJumped;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Space)) {
			Jump();
		}
		
		var z = (float) (transform.localPosition.z + Time.deltaTime * _speedY);
		
		transform.localPosition = new Vector3( 
			transform.localPosition.x, 
			transform.localPosition.y, 
			Mathf.Clamp(z,0,GameConstants.Wold.Radius * 0.7f)
		);
		
		if (z > 0 && (_lastJumped + GameConstants.Player.JumpTime) < Time.time) {
			ApplyGravity();
		}
		
		if(transform.localPosition.z <= 0) {
			_isJumping = false;
		}
	}
	
	private void ApplyGravity() {
		_speedY = _speedY - GameConstants.Wold.Gravity * Time.deltaTime;
	}
	
	private void Jump() {
		if (_isJumping) return;
		AudioManager.Instance.playJumpSound();
		_isJumping = true;
		_lastJumped = Time.time;
		_speedY = GameConstants.Player.JumpForce;
	}
}
