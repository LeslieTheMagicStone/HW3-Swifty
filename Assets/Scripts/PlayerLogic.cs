using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    Animator animator;
    CharacterController characterController;
    float horizontalInput, verticalInput;
    Vector3 movement;
    Vector3 rotation;

    const float SPEED = 2f;
    const float ANGULAR_SPEED = 120f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        animator.SetBool("OnLeft", Input.GetKey(KeyCode.LeftArrow));
        animator.SetBool("OnRight", Input.GetKey(KeyCode.RightArrow));
        animator.SetBool("OnDown", Input.GetKey(KeyCode.DownArrow));
        animator.SetBool("OnUp", Input.GetKey(KeyCode.UpArrow));
    }

    private void FixedUpdate()
    {
        rotation.y = horizontalInput * ANGULAR_SPEED * Time.fixedDeltaTime;
        transform.Rotate(rotation);
        movement = transform.forward * verticalInput * SPEED * Time.fixedDeltaTime;
        characterController.Move(movement);
    }
}
