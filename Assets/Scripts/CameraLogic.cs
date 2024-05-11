using UnityEngine;

public class CameraLogic : MonoBehaviour
{
    Transform player;

    const float Y_OFFSET = 8f;
    const float Z_OFFSET = -2f;
    const float SMOOTH_FACTOR = 2f;

    private void Start()
    {
        player = FindObjectOfType<PlayerLogic>().transform;
    }

    private void Update()
    {
        Vector3 offset = new(0, Y_OFFSET, Z_OFFSET);
        Vector3 targetPos = player.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPos, SMOOTH_FACTOR * Time.deltaTime);
    }
}
