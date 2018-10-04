using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cilinder : MonoBehaviour {

	private GameObject Player;

	public GameObject shield;
	public GameObject boost;
	
	public GameObject MiddleCilinder;

	public GameObject[] halfCilinders;

	public EigthCilinder[] cilinderParts;

	public float Difficulty;

	private MeshRenderer[] renderers;
	
	private bool _isFading = false;

	public bool hasBeenHit = false;

	private void Awake() {
		renderers = GetComponentsInChildren<MeshRenderer>();
	}

	// Use this for initialization
	void Start () {
		Player = GameObject.FindWithTag("Player");
		foreach (var go in cilinderParts) {
			if (Random.value > GameConstants.Cilinder.CilinderDensity) {
				if (Random.value < GameConstants.Cilinder.CilinderShieldOdds) {
					var instance = Instantiate(shield, go.spawner.transform);
					instance.transform.parent = transform;
					var lp = instance.transform.position;
					instance.transform.localPosition = new Vector3(lp.x, lp.y, 5f);
					instance.transform.localScale = Vector3.one*4;
				} else if (Random.value < GameConstants.Cilinder.BoostOdds) {
					var instance = Instantiate(boost, go.spawner.transform);
					instance.transform.parent = transform;
					var lp = instance.transform.position;
					instance.transform.localPosition = new Vector3(lp.x, lp.y, 5f);
					instance.transform.localScale = Vector3.one*4;
				}
				go.gameObject.SetActive(false);
			} 
		}
		
		foreach (var go in halfCilinders) {
			if (Random.value > GameConstants.Cilinder.CilinderDensity) {
				go.SetActive(false);
			} 
		}
		
		
		MiddleCilinder.transform.Rotate(0,0,Random.value*360);
		transform.Rotate(0,0,0);
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.Instance.State != GameState.Playing && GameManager.Instance.State != GameState.Pause) {
			Destroy(gameObject);
			return;
		}
		
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
	
	public void Explode()
	{
		AudioManager.Instance.playBrokenCilinderSound();
 		GetComponent<Animator>().SetTrigger("Explode");
		_isFading = true;
	}

	private void startFading() {
		_isFading = true;
	}
}
