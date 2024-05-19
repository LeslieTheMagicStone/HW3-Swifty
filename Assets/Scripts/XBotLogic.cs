using System.Collections;
using UnityEngine;

public class XBotLogic : MonoBehaviour
{
    [SerializeField] LaserLogic laserPrefab;
    [SerializeField] Transform laserSpawnPoint;

    Animator animator;
    PlayerLogic player;

    bool canFire => laserTimer <= 0;
    float laserTimer;
    bool isBusy;
    const float LASER_INTERVAL = 1f;
    const float LASER_DURATION = 1f;
    const float ROTATION_SPEED = 10f;


    private void Start()
    {
        animator = GetComponent<Animator>();
        player = FindObjectOfType<PlayerLogic>();
        laserTimer = 0.0f;
        isBusy = false;
    }

    private void Update()
    {
        if (isBusy) return;

        if (laserTimer > 0) laserTimer -= Time.deltaTime;

        if (canFire)
        {
            animator.SetTrigger("Laser");
            laserTimer = LASER_INTERVAL;
        }

        if (player == null) return;
        var targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * ROTATION_SPEED);
    }

    public void Fire()
    {
        LaserLogic laser = Instantiate(laserPrefab, laserSpawnPoint.position, transform.rotation);
        laser.Init(LASER_DURATION);
    }

    public void WaitForLaserComplete()
    {
        StartCoroutine(StopAnimatorCoroutine(LASER_DURATION));
    }

    private IEnumerator StopAnimatorCoroutine(float time)
    {
        animator.speed = 0f;
        yield return new WaitForSeconds(time);
        animator.speed = 1f;
    }

    public void SetBusy(bool value)
    {
        isBusy = value;
    }
}
