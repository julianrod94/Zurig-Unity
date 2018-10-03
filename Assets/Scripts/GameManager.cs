public enum GameState { Idle, Playing, Score }
public class GameManager {
    
    public GameState State = GameState.Idle;
    private static GameManager _instance = null;

    public static GameManager Instance {
        get { return _instance ?? (_instance = new GameManager()); }
        private set { _instance = value; }
    }
}
