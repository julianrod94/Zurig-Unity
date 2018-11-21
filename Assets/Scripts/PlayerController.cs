using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
	public float ShootCooldownTime;
	public GameObject PlayerShield;

	private AxisProvider _axis;
	private Position _position;
	private bool hasShield;
	private bool _boosting;
	private bool _invulnerable;

	private const String axisName = "Horizontal";

	void Awake () {
		_axis = new NormalAxis();
		_position = new Position(transform, 0,0);
	}

	void Start() {
		PlayerShield.SetActive(false);
	}
	
	void Update () {
		switch (GameManager.Instance.State) {
			case GameState.Pause:
				Time.timeScale = 0;
				if (Input.GetKeyDown(KeyCode.P)) {
					GameManager.Instance.TogglePause();
				}
				return;
			case GameState.Idle:
				_position.Translate(Time.deltaTime,0);
				return;
			case GameState.Score:
				return;
			case GameState.Playing:
				if (Input.GetKeyDown(KeyCode.P)) {
					GameManager.Instance.TogglePause();
				}
				
				if (_boosting) {
					Time.timeScale = Mathf.Clamp(Time.timeScale + Time.deltaTime * 10f,1f, 6f);
				} else {
					Time.timeScale = Mathf.Clamp(Time.timeScale - Time.deltaTime * 3,1f, 10f);
				}
		
				SetInvulnerable(Time.timeScale > 1f);

				var x = _axis.GetAxis(axisName) * Time.deltaTime * GameConstants.Player.Speed;
				_position.Translate(x,0);
				return;
		}
	}

	public void TurnShiled(bool turnOn) {
		hasShield = turnOn;
		PlayerShield.SetActive(turnOn);
	}

	public void OnTriggerEnter(Collider other) {
		Debug.Log(other.gameObject.tag);
		if (other.gameObject.CompareTag("Wall")) {
			GameManager.Instance.EndGame();
		}
	}

	public void OnCollisionEnter(Collision other) {
		var cilinder = other.gameObject.GetComponentInParent<Cilinder>();
		if (other.gameObject.CompareTag("Cilinder")) {	
			if(cilinder.hasBeenHit) { return; }

			cilinder.hasBeenHit = true;

			if (_invulnerable) {
				cilinder.Explode();
			} else if (hasShield) {
				TurnShiled(false);
				cilinder.Explode();
			} else {
				GameManager.Instance.EndGame();
			}
		} else if (other.gameObject.CompareTag("Shield")) {
			AudioManager.Instance.playShieldSound();
			TurnShiled(true);
			Destroy(other.gameObject);
		} else if (other.gameObject.CompareTag("Boost")) {
			StartCoroutine(Boost());
			Destroy(other.gameObject);
			AudioManager.Instance.playBoostSound();
		}
	}

	void SetInvulnerable(bool isGod) {
		if (_invulnerable != isGod) {
			PlayerShield.SetActive(isGod);
			if(!isGod) { TurnShiled(true); }
		}
		_invulnerable = isGod;
	}

	IEnumerator Boost() {
		if (!_invulnerable) {
			_boosting = true;
			AudioManager.Instance.playBoostSound();
			yield return new WaitForSeconds(10);
			_boosting = false;
		}
	}
}
