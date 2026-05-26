using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InGameController : MonoBehaviour
{
    [Header("Menu Pause")]
    public GameObject pauseMenu;

    [Header("Botão Continuar")]
    public Button botaoContinuar;

    private bool isPaused = false;

    void Start()
    {
        pauseMenu.SetActive(false);

        Time.timeScale = 1f;

        // Mouse travado ao iniciar
        LockCursor();

        // Configura botão continuar
        if (botaoContinuar != null)
        {
            botaoContinuar.onClick.RemoveAllListeners();
            botaoContinuar.onClick.AddListener(Pause);
        }
    }

    void Update()
    {
        if (Keyboard.current != null &&
            Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Pause();
        }
    }

    public void Pause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    void PauseGame()
    {
        isPaused = true;

        Time.timeScale = 0f;

        pauseMenu.SetActive(true);

        UnlockCursor();
    }

    void ResumeGame()
    {
        isPaused = false;

        Time.timeScale = 1f;

        pauseMenu.SetActive(false);

        LockCursor();

        // Corrige problema do mouse no Input System
        InputSystem.ResetHaptics();
    }

    public void MainMenu()
    {
        // Despausa o jogo
        Time.timeScale = 1f;

        UnlockCursor();

        SceneManager.LoadScene(0);
    }

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void OnDisable()
    {
        UnlockCursor();
    }
}