using UnityEngine;

public enum GameState { Idle, Playing,Pause, Score }
public class GameManager: MonoBehaviour{
    
    public GameState State = GameState.Idle;
    public GameObject player;
    public ParticleSystem shipAce;
    public ParticleSystem explosion;
    private float _prevTS = 0;
    private GameState _prevState = GameState.Pause;

    public static GameManager Instance;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public void StartGame() {
        State = GameState.Playing;
        player.SetActive(true);
        shipAce.Play();
    }
    
    public void EndGame() {
        State = GameState.Score;
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
