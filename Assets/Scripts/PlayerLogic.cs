using UnityEngine;
using UnityEngine.Animations;

public class PlayerLogic : MonoBehaviour
{
    public Vector3 targetPos => _targetPos;
    [SerializeField] Transform detectionRayOrigin;
    Animator animator;
    CharacterController characterController;
    float horizontalInput, verticalInput;
    float horizontalInputRaw, verticalInputRaw;
    float moveCD => RhythmManager.Instance.Tnote * 0.8f;
    float moveTimer;
    Vector3 _targetPos;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        characterController.enabled = false;

        _targetPos = transform.position;
        moveTimer = 0f;
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        horizontalInputRaw = Input.GetAxisRaw("Horizontal");
        verticalInputRaw = Input.GetAxisRaw("Vertical");

        animator.SetFloat("HorizontalInput", horizontalInput);
        animator.SetFloat("VerticalInput", verticalInput);

        animator.SetBool("OnLeft", Input.GetKeyDown(KeyCode.LeftArrow));
        animator.SetBool("OnRight", Input.GetKeyDown(KeyCode.RightArrow));
        // animator.SetBool("OnDown", Input.GetKeyDown(KeyCode.DownArrow));
        animator.SetBool("OnUp", Input.GetKeyDown(KeyCode.UpArrow));

        transform.position = Vector3.Lerp(transform.position, _targetPos, 10 * Time.deltaTime);
        if (moveTimer > 0f) moveTimer -= Time.deltaTime;


        Vector3 direction = Vector3.zero;
        if (Input.GetButton("Movement Left")) direction = Vector3.left;
        if (Input.GetButton("Movement Right")) direction = Vector3.right;
        if (Input.GetButton("Movement Up")) direction = Vector3.forward;
        if (Input.GetButton("Movement Down")) direction = Vector3.back;
        if (direction != Vector3.zero && Input.GetButtonDown("Dash"))
        {
            Ray ray = new(detectionRayOrigin.position, direction);
            if (!Physics.Raycast(ray, out RaycastHit hit, 1f))
                if (moveTimer <= 0f)
                {
                    _targetPos = transform.position + direction;
                    moveTimer = moveCD;
                }
        }
    }
}
