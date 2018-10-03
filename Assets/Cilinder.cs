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
	
	private bool _isFading = false;

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
			startFading();
		}
		
		if (transform.position.z - Player.transform.position.z < 0.05 ) {
			AudioManager.Instance.playFlyOverCilinderSound();
		}

		if (_isFading) {
			foreach(var render in renderers) {
				if (render.material.color.a.Equals(1)) {
					StandardShaderUtils.ChangeRenderMode(render.material, StandardShaderUtils.BlendMode.Transparent);
				}
				
				var materialColor = render.material.color;
				materialColor.a = render.material.color.a - 3*Time.deltaTime;
				render.material.color = materialColor;

				if (materialColor.a <= 0) {
					Destroy(gameObject);
				}
			}
		}
	}

	
	void OnCollisionEnter(Collision collision) {
		Explode();
		// Now do whatever you need with myCollider.
		// (If multiple colliders were involved in the collision, 
		// you can find them all by iterating through the contacts)
	}
	
	void Explode()
	{
		AudioManager.Instance.playBrokenCilinderSound();
		GetComponent<Animator>().SetTrigger("Explode");
		_isFading = true;
	}

	private void startFading() {
		_isFading = true;
//		foreach (var go in halfCilinders) {
//			foreach (var child in go.GetComponentsInChildren<Transform>()) {
//				if(child.gameObject == go) continue;
//				child.gameObject.SetActive(false);
//			}
//		}
//		
//		foreach (var go in cilinderParts) {
//			foreach (var child in go.GetComponentsInChildren<Transform>()) {
//				if(child.gameObject == go) continue;
//				child.gameObject.SetActive(false);
//			}
//		}
	}
}
