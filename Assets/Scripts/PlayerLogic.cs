using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    Animator animator;
    CharacterController characterController;
    float horizontalInput, verticalInput;
    float horizontalInputRaw, verticalInputRaw;
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

        horizontalInputRaw = Input.GetAxisRaw("Horizontal");
        verticalInputRaw = Input.GetAxisRaw("Vertical");

        animator.SetFloat("HorizontalInput", horizontalInput);
        animator.SetFloat("VerticalInput", verticalInput);

        animator.SetBool("OnLeft", Input.GetKey(KeyCode.LeftArrow));
        animator.SetBool("OnRight", Input.GetKey(KeyCode.RightArrow));
        animator.SetBool("OnDown", Input.GetKey(KeyCode.DownArrow));
        animator.SetBool("OnUp", Input.GetKey(KeyCode.UpArrow));
    }

    private void FixedUpdate()
    {
        var groundMovement = new Vector3(horizontalInput, 0, verticalInput).normalized * SPEED * Time.deltaTime;
        groundMovement.x *= Mathf.Abs(horizontalInput);
        groundMovement.z *= Mathf.Abs(verticalInput);
        movement.x = groundMovement.x;
        movement.z = groundMovement.z;
        characterController.Move(movement);
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new(0, 0, 200, 200));
        GUILayout.TextArea(movement.ToString());
    }
}
