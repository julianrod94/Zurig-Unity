using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour {

	private double _speedY;
	private bool _isJumping;

	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Space)) {
			Jump();
		}
		var z = (float) (transform.localPosition.z + Time.deltaTime * _speedY);
		if (z > 0) {
			transform.localPosition = new Vector3( transform.localPosition.x, transform.localPosition.y, z);
			gravity();
		}
		else {
			transform.localPosition = new Vector3( transform.localPosition.x, transform.localPosition.y, 0);
		}
		
		if(transform.localPosition.z <= 0) {
			_isJumping = false;
		}

	}
	
	private void gravity() {
		_speedY = _speedY - 50 * Time.deltaTime;
	}
	
	private void Jump() {
		if (_isJumping) return;
		_isJumping = true;
		_speedY = 25f;
	}
}
