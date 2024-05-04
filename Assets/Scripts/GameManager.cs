using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public GameManager Instance => instance;
    private GameManager instance;

    public UnityEvent<string> OnKeyDown;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        
    }
}
