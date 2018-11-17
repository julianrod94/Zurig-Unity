using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTranslator : MonoBehaviour {

	public TurningZone zone;

	private bool isRotating = false;
	public float baseSpeed = 50;

	private float targetRotation = 0;
	private Vector3 targetPosition = Vector3.zero;
	// Use this for initialization
	void Update () {
		if (isRotating) {
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, targetRotation, 0), 0.2f);
			transform.position = Vector3.Lerp(transform.position, targetPosition, 0.2f);
			if (Mathf.Abs(Mathf.DeltaAngle(transform.rotation.eulerAngles.y, targetRotation)) < 5) {
				transform.rotation = Quaternion.Euler(0, targetRotation, 0);
				transform.position = targetPosition;
				isRotating = false;
			}
		} else {
			var speed = baseSpeed;
			if (Input.GetKey(KeyCode.W)) {
				speed *= 2;
			}
			transform.Translate(Vector3.forward * speed * Time.deltaTime);

			if (zone != null) {

				if (Input.GetKeyDown(KeyCode.D) && zone.canRotate(transform, TurningZone.TurningDirection.RIGHT)) {
					Rotate(90);
				} else if (Input.GetKeyDown(KeyCode.A) &&
				           zone.canRotate(transform, TurningZone.TurningDirection.LEFT)) {
					Rotate(-90);
				}
			}

			if (Input.GetKeyDown(KeyCode.S)) {
				Rotate(180);
			}
		}
	}

	private void Rotate(float angle) {
		isRotating = true;
		targetRotation = transform.rotation.eulerAngles.y + angle;
		if (zone != null) {
			targetPosition = zone.transform.position;
		} else {
			targetPosition = transform.position;
		}
	}

	private void OnTriggerEnter(Collider other) {
		var turningZone = other.GetComponentInParent<TurningZone>();
		if (turningZone != null) {
			zone = turningZone;
		}
	}
	
	private void OnTriggerExit(Collider other) {
		var turningZone = other.GetComponentInParent<TurningZone>();
		if (turningZone != null) {
			zone = null;
		}
	}
}
