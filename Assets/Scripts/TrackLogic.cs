using UnityEngine;

public class TrackLogic : MonoBehaviour
{
    [SerializeField] NoteLogic notePrefab;
    [SerializeField] Transform spawnPoint;
    const float NOTE_SPEED = 1f;

    public void SpawnNote(float delay)
    {
        Invoke(nameof(SpawnNote), delay);
    }

    public void SpawnNote()
    {
        var note = Instantiate(notePrefab, spawnPoint.position, Quaternion.identity);
        note.Initialize(this, NOTE_SPEED * (-transform.up));
    }
}