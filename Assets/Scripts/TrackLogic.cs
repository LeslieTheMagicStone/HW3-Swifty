using System.Collections.Generic;
using UnityEngine;

public class TrackLogic : MonoBehaviour
{
    [SerializeField] NoteBulletLogic notePrefab;
    Queue<NoteBulletLogic> notesOnTrack;
    PlayerLogic player;
    Vector3 offset;
    const float SPAWN_DISTANCE = 4f;
    const float NOTE_VELOCITY = 4f;

    private void Awake()
    {
        notesOnTrack = new();
    }

    private void Start()
    {
        player = FindObjectOfType<PlayerLogic>();
        offset = transform.position - player.transform.position;
    }

    private void Update()
    {
        CheckMiss();
        if (player == null) return;
        transform.position = player.transform.position + offset;
    }

    public void SpawnNote(float delay)
    {
        Invoke(nameof(SpawnNote), delay);
    }

    public void SpawnNote()
    {
        NoteBulletLogic note = Instantiate(notePrefab, transform);
        note.transform.position = transform.position + transform.forward * SPAWN_DISTANCE;
        note.transform.rotation = Quaternion.Euler(0, 180, 0) * transform.rotation;
        note.Init(NOTE_VELOCITY);
        notesOnTrack.Enqueue(note);
    }

    public void DestroyNote()
    {
        if (notesOnTrack.Count == 0) return;
        NoteBulletLogic note = notesOnTrack.Dequeue();
        Destroy(note.gameObject);
    }

    private void ReverseNote(HitType hitType)
    {
        if (notesOnTrack.Count == 0) return;
        NoteBulletLogic note = notesOnTrack.Dequeue();
        note.Reverse(hitType);
    }

    public void DetectNote()
    {
        if (notesOnTrack.Count == 0) return;
        NoteBulletLogic note = notesOnTrack.Peek();
        Vector3 distance = note.transform.position - transform.position;
        float dt = Vector3.Dot(distance, note.transform.forward) / NOTE_VELOCITY;

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
        if (notesOnTrack.Count == 0) return;
        NoteBulletLogic note = notesOnTrack.Peek();
        Vector3 distance = note.transform.position - transform.position;
        float dt = Vector3.Dot(distance, note.transform.forward) / NOTE_VELOCITY;

        if (dt > RhythmManager.BAD_TIME)
        {
            RhythmManager.Instance.Miss();
            DestroyNote();
        }
    }
}
