using UnityEngine;

public class XBotLogic : MonoBehaviour
{
    Animator animator;
    PlayerLogic player;

    bool canFire => fireballTimer <= 0;
    float fireballTimer;
    bool isBusy;
    const float FIREBALL_INTERVALL = 1f;
    const float ROTATION_SPEED = 10f;


    private void Start()
    {
        animator = GetComponent<Animator>();
        player = FindObjectOfType<PlayerLogic>();
        fireballTimer = 0.0f;
        isBusy = false;
    }

    private void Update()
    {
        if (isBusy) return;

        if (fireballTimer > 0) fireballTimer -= Time.deltaTime;

        if (canFire)
        {
            animator.SetTrigger("FireBall");
            fireballTimer = FIREBALL_INTERVALL;
        }

        if (player == null) return;
        var targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * ROTATION_SPEED);
    }

    public void SetBusy(bool value)
    {
        isBusy = value;
    }
}
