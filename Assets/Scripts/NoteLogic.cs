using UnityEngine;
using DG.Tweening;

public class NoteLogic : MonoBehaviour
{
    [SerializeField] string targetInput;
    TrackLogic trackLogic;
    Vector3 velocity;
    SpriteRenderer spriteRenderer;
    bool isAlive = true;
    const float PERFECT_TIME = 0.1f;
    const float GOOD_TIME = 0.3f;
    const float BAD_TIME = 0.5f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (!isAlive) return;
        transform.position += velocity * Time.deltaTime;

        float distance = Vector3.Dot(velocity, transform.position - trackLogic.transform.position) / velocity.magnitude;
        float time = distance / velocity.magnitude;

        if (time > BAD_TIME)
        {
            isAlive = false;
            spriteRenderer.DOFade(0, 0.5f);
            Destroy(gameObject, 1f);
            RhythmManager.Instance.Miss();
            return;
        }

        if (Input.GetKeyDown(targetInput))
        {
            if (Mathf.Abs(time) < PERFECT_TIME)
            {
                isAlive = false;
                spriteRenderer.DOFade(0, 0.3f);
                Destroy(gameObject, 1f);
                transform.DOScale(Vector3.one * 1.5f, 0.1f).SetEase(Ease.OutQuad).OnComplete(() =>
                  transform.DOScale(Vector3.one, 0.1f).SetEase(Ease.InQuad));
                RhythmManager.Instance.Hit(HitType.Perfect);
            }
            else if (Mathf.Abs(time) < GOOD_TIME)
            {
                isAlive = false;
                spriteRenderer.DOFade(0, 0.3f);
                Destroy(gameObject, 1f);
                transform.DOScale(Vector3.one * 1.5f, 0.1f).SetEase(Ease.OutQuad).OnComplete(() =>
                  transform.DOScale(Vector3.one, 0.1f).SetEase(Ease.InQuad));
                RhythmManager.Instance.Hit(HitType.Good);
            }
            else if (Mathf.Abs(time) < BAD_TIME)
            {
                isAlive = false;
                spriteRenderer.DOFade(0, 0.3f);
                Destroy(gameObject, 1f);
                transform.DOScale(Vector3.one * 1.5f, 0.1f).SetEase(Ease.OutQuad).OnComplete(() =>
                 transform.DOScale(Vector3.one, 0.1f).SetEase(Ease.InQuad));
                RhythmManager.Instance.Hit(HitType.Bad);
            }
        }
    }
    public void Initialize(TrackLogic trackLogic, Vector3 velocity)
    {
        this.trackLogic = trackLogic;
        this.velocity = velocity;
    }
    private void OnDestroy()
    {
        DOTween.Kill(gameObject);
    }
}
