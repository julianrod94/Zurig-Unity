using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {

	public Canvas Idle;
	public Canvas Game;
	public Canvas EndGame;
	public Canvas Pause;
	
	public Text keysText;
	public Text finalScore;
	public Text openedPortalText;
	public Text LevelText;
	
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

				if (GameManager.Instance.portalIsOpened)
				{
					showOpenedPortalText();
				}
				else
				{
					updateKeyCanvas();					
				}
				
				break;
			
			case GameState.Score:
				if (!EndGame.gameObject.activeSelf) {
					DisableAll();
					EndGame.gameObject.SetActive(true);
					showFinalScore();
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
		GameManager.Instance.State = GameState.Playing;
		AudioManager.Instance.playMainTheme();
	}

	private void DisableAll() {
		Idle.gameObject.SetActive(false);
		Game.gameObject.SetActive(false);
		EndGame.gameObject.SetActive(false);
		Pause.gameObject.SetActive(false);
	}
	
	private void updateKeyCanvas()
	{
		openedPortalText.enabled = false;
		keysText.text = "Keys: " + GameManager.Instance.collectedKeys +"/"+ GameManager.Instance.totalKeys;
		LevelText.text = "Level " + GameManager.Instance.Level;
	}

	private void showFinalScore()
	{
		finalScore.text = GameManager.Instance.collectedKeys + "/" + GameManager.Instance.totalKeys + " keys";
	}

	private void showOpenedPortalText()
	{
		openedPortalText.enabled = true;
		keysText.enabled = false;
	}
}
