using UnityEngine;
using DG.Tweening;

public class LaserLogic : MonoBehaviour
{
    Sequence sequence;
    float duration;
    const float ANIM_TIME = 0.2f;

    public void Init(float duration)
    {
        this.duration = duration;
    }

    private void Start()
    {
        sequence = DOTween.Sequence();
        Vector3 targetScale = transform.localScale;
        transform.localScale = new(targetScale.x * 0.01f, targetScale.y, targetScale.z * 0.01f);
        sequence.AppendInterval(ANIM_TIME);
        sequence.Join(transform.DOScaleX(targetScale.x, ANIM_TIME).SetEase(Ease.InQuad));
        sequence.Join(transform.DOScaleZ(targetScale.z, ANIM_TIME).SetEase(Ease.InQuad));
        sequence.Append(transform.DOShakePosition(duration, 0.2f, 60).SetEase(Ease.Unset));
        sequence.Join(transform.DOScaleX(targetScale.x * 3f, ANIM_TIME).SetLoops((int)(duration / ANIM_TIME), LoopType.Yoyo));
        sequence.Join(transform.DOScaleZ(targetScale.z * 3f, ANIM_TIME).SetLoops((int)(duration / ANIM_TIME), LoopType.Yoyo));
        sequence.AppendInterval(ANIM_TIME);
        sequence.Join(transform.DOScaleX(targetScale.x * 0.01f, ANIM_TIME).SetEase(Ease.InQuad));
        sequence.Join(transform.DOScaleZ(targetScale.z * 0.01f, ANIM_TIME).SetEase(Ease.InQuad));
        sequence.AppendCallback(() => Destroy(gameObject));
    }

    private void OnDestroy()
    {
        sequence?.Kill();
    }
}
