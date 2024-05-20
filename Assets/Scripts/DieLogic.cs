using System.Collections;
using UnityEngine;
using DG.Tweening;

public class DieLogic : MonoBehaviour
{
    AudioLowPassFilter filter;
    private IEnumerator Start()
    {
        GameManager.Instance.ChangeGameState(GameState.GameOver);

        Time.timeScale = 0.5f;
        DOTween.timeScale = 0.5f;

        filter = FindObjectOfType<AudioLowPassFilter>();
        DOVirtual.Float(1f, 0f, 1f, (t) => filter.cutoffFrequency = 5000f * t * t);

        yield return new WaitForSecondsRealtime(2f);

        Time.timeScale = 1f;
        DOTween.timeScale = 1f;
        yield return VignetteLogic.Instance.Outro();
        SceneController.Instance.ReloadScene();
    }
}
