using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		var player = GameManager.Instance.player.transform;
		if (player != null) {
			transform.position = new Vector3(player.position.x, player.position.y, player.position.z - 2f);			
		}
	}
}
