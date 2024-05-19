using System.Collections;
using UnityEngine;

public class DieLogic : MonoBehaviour
{
    private IEnumerator Start()
    {
        Time.timeScale = 0.5f;
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 1f;
        yield return VignetteLogic.Instance.Outro();
        SceneController.Instance.ReloadScene();
    }
}
