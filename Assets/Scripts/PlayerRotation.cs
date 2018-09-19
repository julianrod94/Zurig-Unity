using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(Vector3.zero, Vector3.up);
		if (transform.rotation.eulerAngles.y > 100) {
			transform.LookAt(Vector3.zero, Vector3.down);
		}
	}
}
 