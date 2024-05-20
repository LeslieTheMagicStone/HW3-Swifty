using UnityEngine;
using DG.Tweening;

public class MusicName : MonoBehaviour
{
    const float SHOW_DURATION = 4f;
    const float ANIM_TIME = 0.4f;
    Sequence sequence;
    private void Start()
    {
        float origX = transform.position.x;
        sequence = DOTween.Sequence();
        sequence.Append(transform.DOMoveX(22, ANIM_TIME));
        sequence.AppendInterval(SHOW_DURATION);
        sequence.Append(transform.DOMoveX(origX, ANIM_TIME));
    }

    private void OnDestroy()
    {
        sequence.Kill();
    }
}
