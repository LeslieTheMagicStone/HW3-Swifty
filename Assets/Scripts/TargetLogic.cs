using UnityEngine;
using DG.Tweening;

public class TargetLogic : MonoBehaviour
{
    float emergeTime;
    float duration;
    Sequence sequence;
    const float ANIM_TIME = 0.2f;

    public void Init(float emergeTime, float duration)
    {
        this.emergeTime = emergeTime;
        this.duration = duration;
    }

    private void Start()
    {
        print(emergeTime);
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
