using UnityEngine;
using DG.Tweening;

public enum RhythmEach
{
    Note,
    HalfNote,
    QuarterNote
}

public enum RhythmAnimType
{
    Scale,
    Blink
}

public class RhythmicObject : MonoBehaviour
{
    public RhythmEach rhythmEach;
    public RhythmAnimType rhythmAnimType;
    [SerializeField] Color blinkTargetColor;
    Sequence sequence;

    private void Start()
    {
        float bpm = RhythmManager.Instance.bpm;
        float T = 60 / bpm * Mathf.Pow(2, -(int)rhythmEach);
        sequence = DOTween.Sequence();
        switch (rhythmAnimType)
        {
            case RhythmAnimType.Scale:
                sequence.AppendInterval(T / 5);
                sequence.Join(transform.DOScaleX(1.1f, T / 5));
                sequence.Join(transform.DOScaleY(0.9f, T / 5));
                sequence.AppendInterval(4 * T / 5);
                sequence.Join(transform.DOScaleX(1.0f, 4 * T / 5));
                sequence.Join(transform.DOScaleY(1.0f, 4 * T / 5));
                break;
            case RhythmAnimType.Blink:
                TryGetComponent(out MeshRenderer meshRenderer);
                meshRenderer.material.SetColor("_EmissionColor", Color.black);
                sequence.AppendInterval(T / 5);
                sequence.Join(meshRenderer.material.DOColor(blinkTargetColor, "_EmissionColor", T / 5));
                sequence.AppendInterval(4 * T / 5);
                sequence.Join(meshRenderer.material.DOColor(Color.black, "_EmissionColor", 4 * T / 5));
                break;
        }
        sequence.SetLoops(-1);
    }

    private void OnDestroy()
    {
        sequence.Kill();
    }
}
