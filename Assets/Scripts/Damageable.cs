using UnityEngine;
using DG.Tweening;

public class Damageable : MonoBehaviour
{
    public Side side => _side;
    [SerializeField] int health;
    [SerializeField] Side _side;
    private Vector3 origPos;
    private Tween shakeTween;
    private bool isInvincible => invincibleTimer > 0;
    private float invincibleTimer;
    const float INVINCIBLE_TIME = 0.1f;

    private void Start()
    {
        origPos = transform.position;
    }

    private void Update()
    {
        if (isInvincible) invincibleTimer -= Time.deltaTime;
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible) return;
        health -= damage;
        invincibleTimer = INVINCIBLE_TIME;
        if (health <= 0)
        {
            Destroy(gameObject);
            return;
        }
        if (shakeTween != null) DOTween.Kill(shakeTween);
        shakeTween = transform.DOShakePosition(0.3f, 0.2f, 30).OnComplete(() => transform.position = origPos);
    }

    private void OnDestroy()
    {
        DOTween.Kill(shakeTween);
    }
}
