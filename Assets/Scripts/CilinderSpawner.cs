﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CilinderSpawner : MonoBehaviour {

	public GameObject cilinder;
	
	// Use this for initialization
	void Start () {
		StartCoroutine(Spawn());
	}

	IEnumerator Spawn() {
		while (true) {
			if (GameManager.Instance.State == GameState.Playing) {
				Instantiate(cilinder, transform.position, transform.rotation);
			}
			yield return new WaitForSeconds(GameConstants.Cilinder.SpawnRate);
		}
	}
}
