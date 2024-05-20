using UnityEngine;
using DG.Tweening;

public class TargetLogic : MonoBehaviour
{
    float emergeTime;
    float duration;
    Sequence sequence;
    const float ANIM_TIME = 0.4f;
    const int HIT_SCORE = 1140;
    const int DESTROY_SCORE = 5140;

    public void Init(float emergeTime, float duration)
    {
        this.emergeTime = emergeTime;
        this.duration = duration;
    }

    private void Start()
    {
        var damageable = GetComponent<Damageable>();
        damageable.OnHurt.AddListener(() => RhythmManager.Instance.score += HIT_SCORE);
        damageable.OnDeath.AddListener(() => RhythmManager.Instance.score += DESTROY_SCORE);

        var healthBar = GetComponentInChildren<HealthBar>();
        // healthBar.gameObject.SetActive(false);
        sequence = DOTween.Sequence();
        sequence.AppendInterval(emergeTime - ANIM_TIME);
        sequence.AppendCallback(() => healthBar.gameObject.SetActive(true));
        sequence.Append(transform.DOMoveY(0.5f, ANIM_TIME));
        sequence.AppendInterval(duration);
        sequence.AppendCallback(() => healthBar.gameObject.SetActive(false));
        sequence.Append(transform.DOMoveY(-1.0f, ANIM_TIME));
        sequence.AppendCallback(() => Destroy(gameObject));
    }

    private void OnDestroy()
    {
        sequence?.Kill();
    }
}
