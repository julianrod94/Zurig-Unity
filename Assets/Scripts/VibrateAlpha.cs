using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrateAlpha : MonoBehaviour {

	private MeshRenderer renderer;
	
	// Use this for initialization
	void Awake () {
		renderer = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		var materialColor = renderer.material.color;
		materialColor.a = (float) (Mathf.Cos(5*Time.time)*0.05 + 0.2);
		renderer.material.color = materialColor;
	}
}
