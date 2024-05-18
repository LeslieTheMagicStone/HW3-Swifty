using UnityEngine;

public class NoteBulletLogic : MonoBehaviour
{
    Side side;
    float speed;
    [SerializeField] Material perfectMaterial;
    [SerializeField] Material goodMaterial;
    [SerializeField] Material badMaterial;
    [SerializeField] ParticleSystem reverseParticle;
    [SerializeField] ParticleSystem explodeParticle;
    MeshRenderer meshRenderer;
    ParticleSystemRenderer reverseRenderer, explodeRenderer;
    const int DAMAGE = 1;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        reverseRenderer = reverseParticle.GetComponent<ParticleSystemRenderer>();
        explodeRenderer = explodeParticle.GetComponent<ParticleSystemRenderer>();
        side = Side.Enemy;
        Destroy(gameObject, 20f);
    }

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
        meshRenderer.material = material;
        reverseRenderer.material = material;
        reverseRenderer.trailMaterial = material;
        reverseParticle.Play();
        reverseParticle.transform.parent = null;
        transform.parent = null;
        side = Side.Player;
    }

    private void Explode()
    {
        explodeRenderer.material = meshRenderer.material;
        explodeParticle.Play();
        explodeParticle.transform.parent = null;
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Damageable damageable))
        {
            if (damageable.side == side) return;
            damageable.TakeDamage(DAMAGE);
            Explode();
        }
    }
}
