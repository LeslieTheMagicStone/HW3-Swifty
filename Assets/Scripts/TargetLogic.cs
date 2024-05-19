using UnityEngine;
using DG.Tweening;

public class TargetLogic : MonoBehaviour
{
    [SerializeField] float emergeTime;
    [SerializeField] float duration;
    Sequence sequence;
    const float ANIM_TIME = 0.2f;

    private void Start()
    {
        var healthBar = GetComponentInChildren<HealthBar>();
        healthBar.gameObject.SetActive(false);
        sequence = DOTween.Sequence();
        sequence.AppendInterval(emergeTime - ANIM_TIME);
        sequence.AppendCallback(() => healthBar.gameObject.SetActive(true));
        sequence.Append(transform.DOMoveY(0.5f, ANIM_TIME).SetEase(Ease.OutBounce));
        sequence.AppendInterval(duration);
        sequence.AppendCallback(() => healthBar.gameObject.SetActive(false));
        sequence.Append(transform.DOMoveY(-1.0f, ANIM_TIME).SetEase(Ease.InBounce));
        sequence.AppendCallback(() => Destroy(gameObject));
    }

    private void OnDestroy()
    {
        sequence?.Kill();
    }
}
