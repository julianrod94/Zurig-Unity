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
		
		if (GameManager.Instance.State != GameState.Playing) {
			_position.Translate(Time.deltaTime,0);
			return;
		}
		var x = _axis.GetAxis(axisName) * Time.deltaTime * GameConstants.Player.Speed;
		_position.Translate(x,0);
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
		Debug.Log(other.gameObject);
		if (other.gameObject.CompareTag("Cilinder")) {
			var cilinder = other.gameObject.GetComponentInParent<Cilinder>();
			if(cilinder.hasBeenHit) { return; }

			cilinder.hasBeenHit = true;
			if (hasShield) {
				TurnShiled(false);
				cilinder.Explode();
			} else {
				SceneManager.LoadScene(0);
			}
		} else if (other.gameObject.CompareTag("Shield")) {
			AudioManager.Instance.playShieldSound();
			TurnShiled(true);
			Destroy(other.gameObject);
		}
	}
}
