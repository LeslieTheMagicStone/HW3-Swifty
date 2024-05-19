using System.Collections;
using UnityEngine;

public class XBotLogic : MonoBehaviour
{
    [SerializeField] LaserLogic laserPrefab;
    [SerializeField] Transform laserSpawnPoint;

    Animator animator;

    bool isBusy;
    float laserDuration => RhythmManager.Instance.Tnote;
    const float LASER_CAST_TIME = 0.9f;
    const float LASER_LOCK_TIME = 1.1f;


    private void Start()
    {
        animator = GetComponent<Animator>();
        isBusy = false;
    }

    public void Laser(float delay)
    {
        Invoke(nameof(Laser), delay - LASER_CAST_TIME);
    }

    private void Laser()
    {
        if (isBusy) { print("Busy"); return; }
        StartCoroutine(LaserCoroutine());
    }

    private IEnumerator LaserCoroutine()
    {
        animator.SetTrigger("Laser");
        yield return new WaitForSeconds(LASER_CAST_TIME);
        LaserLogic laser = Instantiate(laserPrefab, laserSpawnPoint.position, laserSpawnPoint.rotation);
        laser.Init(laserDuration);
        // laser.transform.SetParent(transform);
        yield return new WaitForSeconds(LASER_LOCK_TIME - LASER_CAST_TIME);
        WaitForLaserComplete();
    }

    private void WaitForLaserComplete()
    {
        StartCoroutine(StopAnimatorCoroutine(laserDuration));
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
