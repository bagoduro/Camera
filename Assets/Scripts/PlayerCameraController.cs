using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCameraController : MonoBehaviour
{
    public float mouseSensitivity = 0.08f;

    private float pitch = 0f;

    void Update()
    {
        if (Mouse.current == null) return;

        float mouseY =
            Mouse.current.delta.y.ReadValue()
            * mouseSensitivity;

        pitch -= mouseY;

        pitch = Mathf.Clamp(pitch, -80f, 80f);

        transform.localRotation =
            Quaternion.Euler(pitch, 0f, 0f);
    }
}