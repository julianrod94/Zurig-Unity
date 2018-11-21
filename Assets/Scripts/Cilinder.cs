using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cilinder : MonoBehaviour {

	private GameObject Player;

	public GameObject shield;
	public GameObject boost;
	public GameObject key;
	
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
		Player = GameManager.Instance.player;
		foreach (var go in cilinderParts) {
			if (Random.value > GameConstants.Cilinder.CilinderDensity) {
				if (GameManager.Instance.totalKeys == 0 || Random.value < GameConstants.Cilinder.KeyOdds) {
					Instantiate(key, go.spawner.transform.position, Quaternion.identity);
					GameManager.Instance.totalKeys++;
				} else if (Random.value < GameConstants.Cilinder.CilinderShieldOdds) {
					Instantiate(shield, go.spawner.transform.position, Quaternion.identity);
				}
				go.gameObject.SetActive(false);
			} 
		}
		
		cilinderParts[Mathf.FloorToInt(Random.Range(0,cilinderParts.Length))].gameObject.SetActive(false);
		
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
		//TODO See if there is an option to do this
//		if (transform.position.z - Player.transform.position.z < 0.05 ) {
//			AudioManager.Instance.playFlyOverCilinderSound();
//		}
	}
	
	public void Explode()
	{
		AudioManager.Instance.playBrokenCilinderSound();
 		GetComponent<Animator>().SetTrigger("Explode");
		_isFading = true;
	}

}
