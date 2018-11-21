using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour {

	public GameObject playerPrefab;
	
	private void Start() {
		Vector3 direction = Vector3.zero;
		switch (MazeGenerator.GeneratedMaze.StartZoneOpening) {
				case TurningZone.Wall.EAST:
					direction = new Vector3(0,90,0);
					break;
				case TurningZone.Wall.WEST:
					direction = new Vector3(0,-90,0);
					break;
				case TurningZone.Wall.SOUTH:
					direction = new Vector3(0,0,0);
					break;
				case TurningZone.Wall.NORTH:
					direction = new Vector3(0,180,0);
					break;
					
		}
		Instantiate(playerPrefab, transform.position, Quaternion.Euler(direction));
	}
}
