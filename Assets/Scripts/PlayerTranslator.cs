using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTranslator : MonoBehaviour {

	public TurningZone zone;

	private bool isRotating = false;

	private float targetRotation = 0;
	private Vector3 targetPosition = Vector3.zero;

	private PlayerController controller;

	private void Awake() {
		controller = GetComponentInChildren<PlayerController>();
	}

	private void Start() {
		GameManager.Instance.player = controller.gameObject;
	}

	// Use this for initialization
	void Update () {
		if (GameManager.Instance.State != GameState.Playing) {
			return;
		}
		if (isRotating) {
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, targetRotation, 0), 0.2f);
			transform.position = Vector3.Lerp(transform.position, targetPosition, 0.2f);
			if (Mathf.Abs(Mathf.DeltaAngle(transform.rotation.eulerAngles.y, targetRotation)) < 5) {
				transform.rotation = Quaternion.Euler(0, targetRotation, 0);
				transform.position = targetPosition;
				isRotating = false;
			}
		} else {
			var speed = GameConstants.Player.Speed;
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
		if (other.gameObject.CompareTag("Key")) {
			GameManager.Instance.KeyObtained(other.gameObject);
			return;
		}

		if (other.gameObject.CompareTag("Win")) {
			GameManager.Instance.NextLevel();
			return;
		}
	
		var turningZone = other.GetComponentInParent<TurningZone>();
		if (turningZone != null) {
			zone = turningZone;
		}
		var finishZone = other.GetComponentInParent<FinishZone>();
		if (finishZone != null) {
			GameManager.Instance.EndGame();
		}

		controller.OnTriggerEnter(other);
	}

	private void OnCollisionEnter(Collision other) {
		controller.OnCollisionEnter(other);
	}

	private void OnTriggerExit(Collider other) {
		var turningZone = other.GetComponentInParent<TurningZone>();
		if (turningZone != null) {
			zone = null;
		}
	}
}
