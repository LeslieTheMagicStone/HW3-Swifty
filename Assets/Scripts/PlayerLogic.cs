using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        animator.SetBool("OnLeft", Input.GetKey(KeyCode.A));
        animator.SetBool("OnRight", Input.GetKey(KeyCode.D));
        animator.SetBool("OnDown", Input.GetKey(KeyCode.S));
        animator.SetBool("OnUp", Input.GetKey(KeyCode.W));
    }
}
