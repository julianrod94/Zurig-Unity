using UnityEngine;

public enum GameState { Idle, Playing, Score }
public class GameManager: MonoBehaviour{
    
    public GameState State = GameState.Idle;
    public GameObject player;
    public ParticleSystem shipAce;
    public ParticleSystem explosion;

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
}
