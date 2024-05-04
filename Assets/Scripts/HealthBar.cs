using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Damageable master;
    Vector3 localScale;
    Vector3 localPos;
    float maxWidth;

    private void Awake()
    {
        master.OnHurt.AddListener(UpdateHealth);

        maxWidth = transform.localScale.x;
        localScale = transform.localScale;
    }

    private void UpdateHealth()
    {
        localScale.x = (float)master.health / master.maxHealth * maxWidth;
        transform.localScale = localScale;
    }
}
