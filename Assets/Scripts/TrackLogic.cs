using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TrackLogic : MonoBehaviour
{
    [SerializeField] NoteBulletLogic notePrefab;
    [SerializeField] MeshRenderer axisRenderer;
    Queue<NoteBulletLogic> notesOnTrack;
    PlayerLogic player;
    Vector3 offset;
    Vector3 targetPos;
    Sequence blinkSequence;
    private float noteDelay => RhythmManager.Instance.Tnote * 4;
    private float noteSpeed => SPAWN_DISTANCE / noteDelay;
    const float SPAWN_DISTANCE = 4f;

    private void Awake()
    {
        notesOnTrack = new();
    }

    private void Start()
    {
        player = FindObjectOfType<PlayerLogic>();
        offset = transform.position - player.transform.position;
        targetPos = transform.position;
        Blink();
    }

    private void Update()
    {
        while (notesOnTrack.Count > 0 && notesOnTrack.Peek() == null) notesOnTrack.Dequeue();
        CheckMiss();
        if (player == null) return;
        targetPos.x = player.transform.position.x + offset.x;
        targetPos.z = player.transform.position.z + offset.z;
        transform.position = targetPos;
    }

    private void OnDestroy()
    {
        blinkSequence?.Kill();
    }

    public void SpawnNote(float delay)
    {
        Invoke(nameof(SpawnNote), delay - noteDelay);
    }

    public void SpawnNote()
    {
        NoteBulletLogic note = Instantiate(notePrefab, transform);
        note.transform.position = transform.position + transform.forward * SPAWN_DISTANCE;
        note.transform.rotation = Quaternion.Euler(0, 180, 0) * transform.rotation;
        note.Init(noteSpeed);
        notesOnTrack.Enqueue(note);
    }

    public void DestroyNote()
    {
        while (notesOnTrack.Count > 0 && notesOnTrack.Peek() == null) notesOnTrack.Dequeue();
        if (notesOnTrack.Count == 0) return;
        NoteBulletLogic note = notesOnTrack.Dequeue();
        Destroy(note.gameObject);
    }

    private void ReverseNote(HitType hitType)
    {
        while (notesOnTrack.Count > 0 && notesOnTrack.Peek() == null) notesOnTrack.Dequeue();
        if (notesOnTrack.Count == 0) return;
        NoteBulletLogic note = notesOnTrack.Dequeue();
        note.Reverse(hitType);
    }

    public void DetectNote()
    {
        Blink();
        while (notesOnTrack.Count > 0 && notesOnTrack.Peek() == null) notesOnTrack.Dequeue();
        if (notesOnTrack.Count == 0) return;
        NoteBulletLogic note = notesOnTrack.Peek();
        Vector3 distance = note.transform.position - transform.position;
        float dt = Vector3.Dot(distance, note.transform.forward) / noteSpeed;

        if (Mathf.Abs(dt) < RhythmManager.PERFECT_TIME)
        {
            RhythmManager.Instance.Hit(HitType.Perfect);
            ReverseNote(HitType.Perfect);
        }
        else if (Mathf.Abs(dt) < RhythmManager.GOOD_TIME)
        {
            RhythmManager.Instance.Hit(HitType.Good);
            ReverseNote(HitType.Good);
        }
        else if (Mathf.Abs(dt) < RhythmManager.BAD_TIME)
        {
            RhythmManager.Instance.Hit(HitType.Bad);
            ReverseNote(HitType.Bad);
        }
    }

    void CheckMiss()
    {
        while (notesOnTrack.Count > 0 && notesOnTrack.Peek() == null) notesOnTrack.Dequeue();
        if (notesOnTrack.Count == 0) return;
        NoteBulletLogic note = notesOnTrack.Peek();
        Vector3 distance = note.transform.position - transform.position;
        float dt = Vector3.Dot(distance, note.transform.forward) / noteSpeed;

        if (dt > RhythmManager.BAD_TIME)
        {
            RhythmManager.Instance.Miss();
            DestroyNote();
        }
    }

    void Blink()
    {
        blinkSequence?.Kill();
        blinkSequence = DOTween.Sequence();
        blinkSequence.Append(axisRenderer.material.DOColor(Color.white, "_EmissionColor", 0.1f));
        blinkSequence.Append(axisRenderer.material.DOColor(Color.black, "_EmissionColor", 0.5f));
    }
}
