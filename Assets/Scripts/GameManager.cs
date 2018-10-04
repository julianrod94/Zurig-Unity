using UnityEngine;
using UnityEngine.UI;

public enum GameState { Idle, Playing,Pause, Score }
public class GameManager: MonoBehaviour{
    
    public GameState State = GameState.Idle;
    public float Score;
    public Text ScoreText;
    public Text FinalScore;
    public GameObject player;
    public ParticleSystem shipAce;
    public ParticleSystem explosion;
    private float _prevTS = 0;
    private GameState _prevState = GameState.Pause;
    private int _showScoreDelta = 0;

    public static GameManager Instance;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    private void Update() {
        if (_showScoreDelta % 10 == 0) ScoreText.text = (int)Score + " km";
        _showScoreDelta++;
    }

    public void StartGame() {
        State = GameState.Playing;
        player.SetActive(true);
        shipAce.Play();
        Score = 0;
    }
    
    public void EndGame() {
        State = GameState.Score;
        FinalScore.text = (int)Score + " km";
        explosion.gameObject.SetActive(true);
        explosion.Stop();
        explosion.Play();
        player.SetActive(false);
        shipAce.Stop();
    }

    public void TogglePause() {
        var temp = State;
        var tempTS = Time.timeScale;
        State = _prevState;
        Time.timeScale = _prevTS;
        Debug.Log("TimeScale" + Time.timeScale);
        _prevState = temp;
        _prevTS = tempTS;
    }
}
