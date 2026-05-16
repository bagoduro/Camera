using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float speed = 6f;
    public float mouseSensitivity = 0.08f;

    private Rigidbody rb;

    private float yaw;

    private Vector3 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rb.interpolation = RigidbodyInterpolation.Interpolate;

        yaw = transform.eulerAngles.y;
    }

    void Update()
    {
        RotatePlayer();
        ReadInput();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void RotatePlayer()
    {
        if (Mouse.current == null) return;

        float mouseX =
            Mouse.current.delta.x.ReadValue()
            * mouseSensitivity;

        yaw += mouseX;

        transform.rotation =
            Quaternion.Euler(0f, yaw, 0f);
    }

    void ReadInput()
    {
        moveInput = Vector3.zero;

        if (Keyboard.current.wKey.isPressed)
            moveInput += Vector3.forward;

        if (Keyboard.current.sKey.isPressed)
            moveInput += Vector3.back;

        if (Keyboard.current.aKey.isPressed)
            moveInput += Vector3.left;

        if (Keyboard.current.dKey.isPressed)
            moveInput += Vector3.right;

        moveInput.Normalize();
    }

    void MovePlayer()
    {
        Vector3 direction =
            transform.TransformDirection(moveInput);

        Vector3 velocity =
            direction * speed;

        velocity.y = rb.linearVelocity.y;

        rb.linearVelocity = velocity;
    }
}