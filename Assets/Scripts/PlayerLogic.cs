using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    [SerializeField] Transform detectionRayOrigin;
    Animator animator;
    CharacterController characterController;
    float horizontalInput, verticalInput;
    float horizontalInputRaw, verticalInputRaw;
    Vector3 rotation;
    Vector3 targetPos;
    float moveTimer;
    bool isMoving => moveTimer > 0f;

    const float MOVE_TIME = 0.2f;
    const float MOVEMENT_DISTANCE = 1f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();

        targetPos = transform.position;
        moveTimer = 0f;
    }

    private void Update()
    {
        UpdateTimers();

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

        Vector3 direction = Vector3.zero;
        if (Input.GetButtonDown("Movement Left")) direction = Vector3.left;
        if (Input.GetButtonDown("Movement Right")) direction = Vector3.right;
        if (Input.GetButtonDown("Movement Up")) direction = Vector3.forward;
        if (Input.GetButtonDown("Movement Down")) direction = Vector3.back;
        if (direction != Vector3.zero && !isMoving)
        {
            moveTimer = MOVE_TIME;
            Ray ray = new(detectionRayOrigin.position, direction);
            if (!Physics.Raycast(ray, out RaycastHit hit, MOVEMENT_DISTANCE))
                targetPos += direction * MOVEMENT_DISTANCE;
        }

        Vector3 nextPos = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 10f);
        characterController.Move(nextPos - transform.position);
    }


    private void UpdateTimers()
    {
        if (moveTimer > 0) moveTimer -= Time.deltaTime;
        else moveTimer = 0f;
    }
}
