using UnityEngine;
using UnityEngine.Animations;

public class PlayerLogic : MonoBehaviour
{
    public Vector3 targetPos => _targetPos;
    [SerializeField] Transform spineTarget;
    Animator animator;
    CharacterController characterController;
    float horizontalInput, verticalInput;
    float horizontalInputRaw, verticalInputRaw;
    Vector3 _targetPos;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        characterController.enabled = false;

        _targetPos = transform.position;
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

        Vector3 direction = Vector3.zero;
        if (Input.GetButtonDown("Movement Left")) direction = Vector3.left;
        if (Input.GetButtonDown("Movement Right")) direction = Vector3.right;
        if (Input.GetButtonDown("Movement Up")) direction = Vector3.forward;
        if (Input.GetButtonDown("Movement Down")) direction = Vector3.back;
    }
}
