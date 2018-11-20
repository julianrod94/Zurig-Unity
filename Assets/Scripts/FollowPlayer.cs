using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    private Transform player;
	// Use this for initialization
	void Start () {
		player = GameManager.Instance.player.transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (player != null) {
			transform.position = new Vector3(player.position.x, player.position.y, player.position.z - 2f);			
		}
	}
}
