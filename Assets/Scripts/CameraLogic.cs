using UnityEngine;

public class CameraLogic : MonoBehaviour
{
    Transform player;

    Vector3 offset;
    const float SMOOTH_FACTOR = 2f;

    private void Start()
    {
        player = FindObjectOfType<PlayerLogic>().transform;
        offset = transform.position - player.position;
    }

    private void Update()
    {
        if (player == null) return;
        Vector3 targetPos = player.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPos, SMOOTH_FACTOR * Time.deltaTime);
    }
}
