using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour {

	public GameObject indicator;
	private GameObject _indicator;
	
	// Use this for initialization
	void Start () {
		_indicator = Instantiate(indicator,
			new Vector3(transform.position.x,
				transform.position.y + 100,
				transform.position.z), Quaternion.identity);
	}

	private void OnDestroy() {
		Destroy(_indicator);
	}
}
