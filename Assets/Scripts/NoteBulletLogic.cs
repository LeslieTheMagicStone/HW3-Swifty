using UnityEngine;

public class NoteBulletLogic : MonoBehaviour
{
    float speed;

    private void FixedUpdate()
    {
        transform.Translate(speed * Time.deltaTime * transform.forward, Space.World);
    }

    public void Init(float speed)
    {
        this.speed = speed;
    }
}
