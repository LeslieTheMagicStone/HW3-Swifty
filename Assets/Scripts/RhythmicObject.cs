using UnityEngine;
using DG.Tweening;

public enum RhythmEach
{
    Note,
    HalfNote,
    QuarterNote
}

public class RhythmicObject : MonoBehaviour
{
    public RhythmEach rhythmEach;
    Sequence sequence;

    private void Start()
    {
        float bpm = RhythmManager.Instance.bpm;
        float T = 60 / bpm * Mathf.Pow(2, -(int)rhythmEach);
        sequence = DOTween.Sequence();
        sequence.Append(transform.DOScaleX(1.1f, T / 5));
        sequence.Join(transform.DOScaleY(0.9f, T / 5));
        sequence.Append(transform.DOScaleX(1.0f, 4 * T / 5));
        sequence.Join(transform.DOScaleY(1.0f, 4 * T / 5));
        sequence.SetLoops(-1);
    }

    private void OnDestroy()
    {
        sequence.Kill();
    }
}
