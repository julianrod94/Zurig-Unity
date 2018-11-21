using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour {

	public void startPlaying() {
		AudioManager.Instance.playButtonSound();
		GameManager.Instance.StartGame();
	}
	
	public void Restart() {
		GameManager.Instance.Restart();
	}
}
