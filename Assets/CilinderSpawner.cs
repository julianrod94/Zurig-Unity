using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CilinderSpawner : MonoBehaviour {

	public GameObject cilinder;
	public float SpawnRate = 3;
	
	// Use this for initialization
	void Start () {
		StartCoroutine(Spawn());
	}

	IEnumerator Spawn() {
		while (true) {
			Instantiate(cilinder, transform.position, transform.rotation);
			yield return new WaitForSeconds(SpawnRate);
		}
	}
}
