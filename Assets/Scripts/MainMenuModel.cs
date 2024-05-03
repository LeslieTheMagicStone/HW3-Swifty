using UnityEngine;
using DG.Tweening;

public class MainMenuModel : MonoBehaviour
{
    [SerializeField]
    Transform moveTransform;
    Animator animator;
    Vector3 lookAtPos;
    Vector3 lookAtPosRaw;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector3 targetPoint = transform.position + transform.forward;
        Vector3 mousePos = new(Input.mousePosition.x, Input.mousePosition.y, Vector3.Distance(targetPoint, Camera.main.transform.position));
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        lookAtPosRaw = worldPos;
        lookAtPos = Vector3.Lerp(lookAtPos, worldPos, Time.deltaTime * 10.0f);
    }

    private void OnAnimatorIK(int layerIndex)
    {
        animator.SetLookAtPosition(lookAtPos);
        animator.SetLookAtWeight(1.0f);
    }

    private void OnDestroy()
    {
        DOTween.KillAll();
    }

    public void OnButtonClicked()
    {
        animator.enabled = false;
        moveTransform.DOMove(lookAtPosRaw, 0.2f);
    }
}
