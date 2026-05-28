using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movimento")]
    public float speed = 6f;
    public float sprintMultiplier = 1.666f;
    public float jumpForce = 5f;
    public float mouseSensitivity = 0.08f;

    [Header("Ground Check")]
    public LayerMask groundMask = -1;
    public float groundDistance = 0.2f;

    private Rigidbody rb;

    private float yaw;

    private Vector3 moveInput;

    private bool isGrounded;
    private bool jumpRequested;

    private float currentSpeed;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // CONGELA TODAS rotações
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    void Start()
    {
        currentSpeed = speed;

        yaw = transform.eulerAngles.y;

        ResetPhysics();
    }

    void OnEnable()
    {
        ResetPhysics();
    }

    void ResetPhysics()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.position = transform.position;

        rb.rotation = Quaternion.Euler(0f, yaw, 0f);
    }

    void Update()
    {
        if (Time.timeScale == 0f)
            return;

        HandleMouseLook();

        HandleMovementInput();

        HandleJumpInput();

        HandleSprint();
    }

    void FixedUpdate()
    {
        if (Time.timeScale == 0f)
            return;

        RotatePlayer();

        CheckGrounded();

        ApplyMovement();

        ApplyJump();
    }

    void RotatePlayer()
    {
        Quaternion rotation = Quaternion.Euler(0f, yaw, 0f);

        rb.MoveRotation(rotation);
    }

    void HandleMouseLook()
    {
        if (Mouse.current == null)
            return;

        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        yaw += mouseDelta.x * mouseSensitivity;
    }

    void HandleMovementInput()
    {
        moveInput = Vector3.zero;

        Keyboard kb = Keyboard.current;

        if (kb == null)
            return;

        if (kb.wKey.isPressed)
            moveInput.z += 1f;

        if (kb.sKey.isPressed)
            moveInput.z -= 1f;

        if (kb.aKey.isPressed)
            moveInput.x -= 1f;

        if (kb.dKey.isPressed)
            moveInput.x += 1f;

        moveInput.Normalize();
    }

    void HandleJumpInput()
    {
        if (Keyboard.current != null &&
            Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            jumpRequested = true;
        }
    }

    void HandleSprint()
    {
        if (Keyboard.current != null &&
            Keyboard.current.leftShiftKey.isPressed)
        {
            currentSpeed = speed * sprintMultiplier;
        }
        else
        {
            currentSpeed = speed;
        }
    }

    void CheckGrounded()
    {
        Collider col = GetComponent<Collider>();

        if (col == null)
            return;

        float height = col.bounds.size.y;

        Vector3 feetPos =
            transform.position - new Vector3(0, height * 0.5f, 0);

        feetPos += Vector3.up * 0.05f;

        float rayDistance = groundDistance + 0.3f;

        isGrounded = Physics.Raycast(
            feetPos,
            Vector3.down,
            rayDistance,
            groundMask
        );
    }

    void ApplyMovement()
    {
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 direction =
            (forward * moveInput.z) +
            (right * moveInput.x);

        direction.Normalize();

        Vector3 velocity = direction * currentSpeed;

        velocity.y = rb.linearVelocity.y;

        rb.linearVelocity = velocity;
    }

    void ApplyJump()
    {
        if (jumpRequested && isGrounded)
        {
            rb.linearVelocity =
                new Vector3(
                    rb.linearVelocity.x,
                    jumpForce,
                    rb.linearVelocity.z
                );

            jumpRequested = false;
        }
    }
}