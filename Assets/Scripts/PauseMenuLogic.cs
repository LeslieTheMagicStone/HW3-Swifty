using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuLogic : MonoBehaviour
{
    [SerializeField] Image panel;
    [SerializeField] AudioSource music;
    const float ANIM_TIME = 0.4f;

    private void Awake()
    {
        panel.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.Instance.gameState == GameState.Playing)
                Pause();
            else if (GameManager.Instance.gameState == GameState.Paused)
                Resume();
        }
    }

    public void Pause()
    {
        GameManager.Instance.ChangeGameState(GameState.Paused);
        music.Pause();

        panel.gameObject.SetActive(true);

        Time.timeScale = 0f;
        DOTween.timeScale = 1f;
    }

    public void Resume()
    {
        GameManager.Instance.ChangeGameState(GameState.Playing);
        music.Play();

        panel.gameObject.SetActive(false);

        Time.timeScale = 1f;
        DOTween.timeScale = 1f;
    }

    public void Restart()
    {
        Resume();
        SceneController.Instance.ReloadScene();
    }

    public void ToMainMenu()
    {
        Resume();
        SceneController.Instance.ToMainMenu();
    }
}
