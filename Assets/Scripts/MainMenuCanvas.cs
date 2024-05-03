using UnityEngine;
using DG.Tweening;

public class MainMenuCanvas : MonoBehaviour
{
    public void LoadNextScene()
    {
        DOVirtual.DelayedCall(0.3f, () => SceneController.Instance.LoadNextScene());
    }

    public void QuitGame()
    {
        DOVirtual.DelayedCall(0.3f, () => Application.Quit());
    }
}
