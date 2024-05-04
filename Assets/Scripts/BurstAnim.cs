using UnityEngine;

public class BurstAnim : MonoBehaviour
{
    const float SPEED = 8f;

    private void Start() {
        Burst();
    }

    private void Burst()
    {
        foreach (var rb in transform.GetComponentsInChildren<Rigidbody>())
        {
            Vector3 randDirection = new(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            rb.velocity = SPEED * randDirection;
        }
    }
}
