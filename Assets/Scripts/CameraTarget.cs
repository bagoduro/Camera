using UnityEngine;
using UnityEngine.InputSystem;

public class CameraTarget : MonoBehaviour
{
    public float mouseSensitivity = 0.08f;

    // Limites de rotação vertical
    public float minPitch = -35f;
    public float maxPitch = 60f;

    private float pitch = 0f;

    void Update()
    {
        if (Time.timeScale == 0f) return;
        if (Mouse.current == null) return;

        float mouseY = Mouse.current.delta.y.ReadValue();
        pitch -= mouseY * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }
}
