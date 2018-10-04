using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {

	public Canvas Idle;
	public Canvas Game;
	public Canvas EndGame;
	public Canvas Pause;

//	private float _initialTime = GameManager.Instance.Time;

	void Awake() {
		DisableAll();	
	}

	// Update is called once per frame
	void Update () {
		switch (GameManager.Instance.State) {
			case GameState.Idle:
				if (!Idle.gameObject.activeSelf) {
					DisableAll();
					Idle.gameObject.SetActive(true);
				}
				break;
				
			case GameState.Playing:
				if (!Game.gameObject.activeSelf) {
					DisableAll();
					Game.gameObject.SetActive(true);
				}
				break;
			
			case GameState.Score:
				if (!EndGame.gameObject.activeSelf) {
					DisableAll();
					EndGame.gameObject.SetActive(true);
				}
				break;
			
			case GameState.Pause:
				if (!Pause.gameObject.activeSelf) {
					DisableAll();
					Pause.gameObject.SetActive(true);
				}
				break;
		}
	}

	public void PlayGame() {
//		GameManager.Instance.Time = _initialTime;
//		ScoreManager.Instance.P1Score = 0;
		GameManager.Instance.State = GameState.Playing;
		AudioManager.Instance.playMainTheme();
	}

	private void DisableAll() {
		Idle.gameObject.SetActive(false);
		Game.gameObject.SetActive(false);
		EndGame.gameObject.SetActive(false);
		Pause.gameObject.SetActive(false);
	}
}
