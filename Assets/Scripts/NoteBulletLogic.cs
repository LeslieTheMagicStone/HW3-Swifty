using UnityEngine;

public class NoteBulletLogic : MonoBehaviour
{
    float speed;
    [SerializeField] Material perfectMaterial;
    [SerializeField] Material goodMaterial;
    [SerializeField] Material badMaterial;

    private void FixedUpdate()
    {
        transform.Translate(speed * Time.deltaTime * transform.forward, Space.World);
    }

    public void Init(float speed)
    {
        this.speed = speed;
    }

    public void Reverse(HitType hitType)
    {
        transform.Rotate(0, 180f, 0);
        Material material = perfectMaterial;
        switch (hitType)
        {
            case HitType.Perfect: material = perfectMaterial; break;
            case HitType.Good: material = goodMaterial; break;
            case HitType.Bad: material = badMaterial; break;
        }
        GetComponent<MeshRenderer>().material = material;
        ParticleSystem particle = GetComponentInChildren<ParticleSystem>();
        particle.GetComponent<ParticleSystemRenderer>().material = material;
        particle.GetComponent<ParticleSystemRenderer>().trailMaterial = material;
        particle.Play();
        particle.transform.parent = null;
        transform.parent = null;
    }
}
