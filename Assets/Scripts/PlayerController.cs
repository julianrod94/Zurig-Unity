using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
	private static Vector3 _infinitum = new Vector3(0,0,10);
	public bool IsShooting;
	public float ShootCooldownTime;
	public GameObject PlayerShield;

	private AxisProvider _axis;
	private Position _position;
	private Cooldown _shootCD;
	private bool hasShield;
	private bool _boosting;
	private bool _invulnerable;

	private const String axisName = "Horizontal";

	void Awake () {
		_axis = new NormalAxis();
		_position = new Position(transform, 0,0);
		_shootCD = new Cooldown(ShootCooldownTime);
	}

	void Start() {
		PlayerShield.SetActive(false);
	}
	
	void Update () {
		switch (GameManager.Instance.State) {
			case GameState.Idle:
				_position.Translate(Time.deltaTime,0);
				return;
			case GameState.Score:
				return;
			case GameState.Playing:
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
	
	private void Shoot() {
		if(!IsShooting) { return; }
		BulletManager.Instance.GenerateBullet(_position, Quaternion.identity);
		_shootCD.Use();
	}

	private void OnCollisionEnter(Collision other) {
		if (other.gameObject.CompareTag("Cilinder")) {
			var cilinder = other.gameObject.GetComponentInParent<Cilinder>();
			if(cilinder.hasBeenHit) { return; }

			cilinder.hasBeenHit = true;

			Debug.Log(_invulnerable);
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
		}
	}

	void SetInvulnerable(bool isGod) {
		_invulnerable = isGod;
		if (isGod) {
			PlayerShield.SetActive(true);
		}
	}

	IEnumerator Boost() {
		if (!_boosting) {
			_boosting = true;
			yield return new WaitForSeconds(10);
			_boosting = false;
		}
	}
}
