using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {
	private Transform following;
	// Update is called once per frame
	void Update () {
		if (following == null) {
			var player = GameManager.Instance.player;
			if (player != null) {
				following = player.transform.FindChildByRecursive("Ship_1");
				
			}
		}else {
			transform.position = new Vector3(following.position.x, following.position.y, following.position.z);
			transform.rotation = following.rotation;
			transform.Translate(0,-2f,0);
		}
	}
}
