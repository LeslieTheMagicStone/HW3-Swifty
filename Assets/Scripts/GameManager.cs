using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance => instance;
    private static GameManager instance;

    public bool debugMode => _debugMode;
    [SerializeField] bool _debugMode;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start() {
    }

    private void Update()
    {

    }
}
