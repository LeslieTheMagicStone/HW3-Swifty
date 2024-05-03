using TMPro;
using UnityEngine;
using DG.Tweening;

public class LogoAnim : MonoBehaviour
{
    [SerializeField] TMP_Text logoName;
    [SerializeField] TMP_Text logo;

    private void Start()
    {
        logo.color = new(logo.color.r, logo.color.g, logo.color.b, 0.0f);
        logoName.color = new(logoName.color.r, logoName.color.g, logoName.color.b, 0.0f);
        float targetPosX = logo.transform.position.x;
        logo.transform.localPosition = Vector3.zero;

        Sequence sequence = DOTween.Sequence();
        sequence.Append(logo.DOFade(1, 0.5f));
        sequence.Append(logo.transform.DOMoveX(targetPosX, 0.5f));
        sequence.Append(logoName.DOFade(1, 0.5f));
        sequence.AppendInterval(0.3f);
        sequence.Append(logo.DOFade(0, 0.3f));
        sequence.Join(logoName.DOFade(0, 0.3f));
        sequence.AppendCallback(() => SceneController.Instance.LoadNextScene());
    }
}
