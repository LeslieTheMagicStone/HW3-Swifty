using UnityEngine;
using DG.Tweening;

public class OptionPanelLogic : MonoBehaviour
{
    [SerializeField]Transform cam;
    const float ANIM_TIME = 0.4f;
    public void ShowPanel()
    {
        cam.DOMoveX(0.44f, ANIM_TIME).SetEase(Ease.OutQuad);
        transform.DOMoveX(0f, ANIM_TIME).SetEase(Ease.OutQuad);
    }

    public void HidePanel()
    {
        cam.DOMoveX(0f, ANIM_TIME).SetEase(Ease.InQuad);
        transform.DOMoveX(-500f, ANIM_TIME).SetEase(Ease.InQuad);
    }
}
