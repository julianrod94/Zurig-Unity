using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameState { Idle, Playing,Pause, Score }
public class GameManager: MonoBehaviour{
    
    public GameState State = GameState.Idle;
    public GameObject player;
    public ParticleSystem shipAce;
    private ParticleSystem _shipAce;
    public ParticleSystem explosion;
    private float _prevTS = 0;
    private GameState _prevState = GameState.Pause;
    public FinishZone endCilinder;
    
    public int collectedKeys = 0;
    public int totalKeys = 0;

    public int Level = 1;

    public static GameManager Instance;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this);
            ResetValues();
        } else {
            Instance.ResetValues();
            Destroy(gameObject);
        }
    }

    public void ResetValues() {
        collectedKeys = 0;
        totalKeys = 0;
        endCilinder = null;
        player = null;
        _shipAce = Instantiate(shipAce);
    }
    
    public void StartGame() {
        State = GameState.Playing;
        player.SetActive(true);
        shipAce.Play();
    }

    public void Restart() {
        State = GameState.Idle;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void EndGame() {
        State = GameState.Score;
        Instantiate(explosion, player.transform.position, Quaternion.identity);
        explosion.gameObject.SetActive(true);
        explosion.Stop();
        explosion.Play();
        player.SetActive(false);
        _shipAce.Stop();
    }

    public void TogglePause() {
        var temp = State;
        var tempTS = Time.timeScale;
        State = _prevState;
        Time.timeScale = _prevTS;
        _prevState = temp;
        _prevTS = tempTS;
    }

    public void KeyObtained(GameObject key) {
        Destroy(key);
        collectedKeys++;
        if (collectedKeys == totalKeys) {
            endCilinder.openPortal();
        }
    }
}
