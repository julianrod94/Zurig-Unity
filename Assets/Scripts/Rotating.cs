using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotating : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0,3*60 * Time.deltaTime,0);
	}
}
