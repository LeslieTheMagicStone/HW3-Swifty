using UnityEngine;
using DG.Tweening;

public class InputResponder : MonoBehaviour
{
    [SerializeField] string targetInput;
    private void Update()
    {
        if (!Input.GetKeyDown(targetInput)) return;

        transform.DOScale(Vector3.one * 1.5f, 0.1f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            transform.DOScale(Vector3.one, 0.1f).SetEase(Ease.InQuad);
        });
    }
    private void OnDestroy()
    {
        DOTween.KillAll();
    }
}
