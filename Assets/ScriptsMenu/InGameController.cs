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

    [Header("UI HUD")]
    public GameObject healthBarUI;   // Barra de vida (HealthSlider)
    public GameObject staminaBarUI;  // Barra de stamina (boostSlider do powerup)

    private bool isPaused = false;
    private bool staminaWasActive;   // Guarda o estado da stamina antes do pause

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

        // Esconde a barra de vida (sempre some no pause)
        if (healthBarUI != null)
            healthBarUI.SetActive(false);

        // Salva o estado atual da barra de stamina e depois esconde
        if (staminaBarUI != null)
        {
            staminaWasActive = staminaBarUI.activeSelf;
            staminaBarUI.SetActive(false);
        }

        UnlockCursor();
    }

    void ResumeGame()
    {
        isPaused = false;

        Time.timeScale = 1f;

        pauseMenu.SetActive(false);

        // Mostra a barra de vida novamente (sempre volta)
        if (healthBarUI != null)
            healthBarUI.SetActive(true);

        // Restaura a barra de stamina para o estado que tinha antes do pause
        if (staminaBarUI != null)
            staminaBarUI.SetActive(staminaWasActive);

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