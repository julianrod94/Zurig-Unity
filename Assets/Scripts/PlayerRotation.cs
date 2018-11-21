using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour {
	public Transform target;
	// Update is called once per frame
	void Update () {
		Vector3 up = Vector3.down;
		
		transform.LookAt(target, Vector3.down);
		if (transform.parent.parent.rotation.y > 100) {
			if (transform.localRotation.eulerAngles.y > 100) {
				transform.LookAt(target, Vector3.up);
			}
		} else {
			if (transform.localRotation.eulerAngles.y < 100) {
				transform.LookAt(target,  Vector3.up);
			}
		}
	}
}
 