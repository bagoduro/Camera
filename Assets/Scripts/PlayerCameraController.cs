using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCameraController : MonoBehaviour
{
    public float mouseSensitivity = 0.08f;
    public bool invertY = false;

    private float pitch = 0f;

    void Update()
    {
        // Se o jogo estiver pausado, não move a câmera
        if (Time.timeScale == 0f) return;
        if (Mouse.current == null) return;

        float mouseY = Mouse.current.delta.y.ReadValue() * mouseSensitivity;
        float delta = invertY ? mouseY : -mouseY;
        pitch += delta;
        pitch = Mathf.Clamp(pitch, -80f, 80f);

        // Rotação local apenas no eixo X (pitch)
        transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }
}