using UnityEngine;
using UnityEngine.Events;

public enum GameState
{
    Playing,
    Paused,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance => instance;
    private static GameManager instance;

    public bool debugMode => _debugMode;
    [SerializeField] bool _debugMode;
    public GameState gameState => _gameState;
    private GameState _gameState;
    public UnityEvent OnGameStateChanged;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        _gameState = GameState.Playing;
    }

    public void ChangeGameState(GameState targetState)
    {
        if (_gameState == targetState) return;
        _gameState = targetState;
        OnGameStateChanged.Invoke();
    }
}
