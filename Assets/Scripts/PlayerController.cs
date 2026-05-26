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

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        yaw = transform.eulerAngles.y;
        currentSpeed = speed;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Escondido
        if (false) Debug.Log($"Ground Mask: {groundMask.value}");
    }

    void Update()
    {
        if (Time.timeScale > 0f)
        {
            if (Cursor.lockState != CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        if (Time.timeScale == 0f) return;

        HandleMouseLook();
        HandleMovementInput();
        HandleJumpInput();
        HandleSprint();

        // Aplica rotação e normaliza yaw para evitar valores gigantescos
        yaw = NormalizeAngle(yaw);
        transform.rotation = Quaternion.Euler(0f, yaw, 0f);
    }

    void FixedUpdate()
    {
        if (Time.timeScale == 0f) return;
        CheckGrounded();
        ApplyMovement();
        ApplyJump();
    }

    void HandleMouseLook()
    {
        if (Mouse.current == null) return;
        float mouseX = Mouse.current.delta.x.ReadValue() * mouseSensitivity;
        yaw += mouseX;
    }

    void HandleMovementInput()
    {
        moveInput = Vector3.zero;
        Keyboard kb = Keyboard.current;
        if (kb == null) return;

        if (kb.wKey.isPressed) moveInput.z += 1f;
        if (kb.sKey.isPressed) moveInput.z -= 1f;
        if (kb.aKey.isPressed) moveInput.x -= 1f;
        if (kb.dKey.isPressed) moveInput.x += 1f;

        moveInput.Normalize();
    }

    void HandleJumpInput()
    {
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
            jumpRequested = true;
    }

    void HandleSprint()
    {
        if (Keyboard.current != null && Keyboard.current.leftShiftKey.isPressed)
            currentSpeed = speed * sprintMultiplier;
        else
            currentSpeed = speed;
    }

    void CheckGrounded()
    {
        Collider col = GetComponent<Collider>();
        if (col == null) return;

        float height = col.bounds.size.y;
        Vector3 feetPos = transform.position - new Vector3(0, height * 0.5f, 0);
        feetPos += Vector3.up * 0.05f;
        
        float rayDistance = groundDistance + 0.3f;
        
        bool hit = Physics.Raycast(feetPos, Vector3.down, out RaycastHit hitInfo, rayDistance, groundMask);
        isGrounded = hit;
        
        Debug.DrawRay(feetPos, Vector3.down * rayDistance, isGrounded ? Color.green : Color.red);
        
        if (!isGrounded)
        {
            Vector3 lowerFeet = feetPos + Vector3.down * 0.1f;
            isGrounded = Physics.Raycast(lowerFeet, Vector3.down, out hitInfo, rayDistance - 0.1f, groundMask);
            Debug.DrawRay(lowerFeet, Vector3.down * (rayDistance - 0.1f), isGrounded ? Color.cyan : Color.yellow);
        }
    }

    void ApplyMovement()
    {
        Vector3 direction = (transform.forward * moveInput.z) + (transform.right * moveInput.x);
        direction.Normalize();

        Vector3 targetVelocity = direction * currentSpeed;
        targetVelocity.y = rb.linearVelocity.y;
        rb.linearVelocity = targetVelocity;
    }

    void ApplyJump()
    {
        if (jumpRequested && isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
            jumpRequested = false;
            Debug.Log("Pulo executado!");
        }
    }

    // Método auxiliar para manter o ângulo entre 0 e 360 graus
    private float NormalizeAngle(float angle)
    {
        angle %= 360f;
        if (angle < 0) angle += 360f;
        return angle;
    }

    void OnDestroy()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}