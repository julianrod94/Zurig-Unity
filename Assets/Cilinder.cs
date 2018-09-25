using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cilinder : MonoBehaviour {

	public GameObject Player;
	
	public GameObject MiddleCilinder;

	public GameObject[] halfCilinders;

	public GameObject[] cilinderParts;

	public float Difficulty;

	private MeshRenderer[] renderers;

	private void Awake() {
		renderers = GetComponentsInChildren<MeshRenderer>();
	}

	// Use this for initialization
	void Start () {
		Player = GameObject.FindWithTag("Player");
		foreach (var go in cilinderParts) {
			if (Random.value < 0.5f) {
				go.SetActive(false);
			} 
		}
		
		foreach (var go in halfCilinders) {
			if (Random.value < 0.5f) {
				go.SetActive(false);
			} 
		}
		
		
		MiddleCilinder.transform.Rotate(0,0,Random.value*360);
		transform.Rotate(0,0,Random.value*360);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(0,0,-GameConstants.Cilinder.Speed * Time.deltaTime);

		if (transform.position.z < Player.transform.position.z) {
			foreach(var render in renderers) {
				if (render.material.color.a.Equals(1)) {
					StandardShaderUtils.ChangeRenderMode(render.material, StandardShaderUtils.BlendMode.Transparent);
				}
				
				var materialColor = render.material.color;
				materialColor.a = render.material.color.a - Time.deltaTime;
				render.material.color = materialColor;

				if (materialColor.a <= 0) {
					Destroy(gameObject);
				}
			}
		} 
	}
}
