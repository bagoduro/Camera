using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathScreenManager : MonoBehaviour
{
    public GameObject deathScreenPanel;
    public Button restartButton;
    public Button mainMenuButton;

    private PlayerHealth playerHealth;
    private bool isDead = false;

    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        if (playerHealth == null)
        {
            Debug.LogError("DeathScreenManager: Nenhum PlayerHealth encontrado!");
            return;
        }

        playerHealth.useDeathScreen = true;
        playerHealth.OnHealthChanged += OnHealthChanged;

        if (deathScreenPanel != null)
            deathScreenPanel.SetActive(false);

        if (restartButton != null)
            restartButton.onClick.AddListener(RestartGame);

        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(GoToMainMenu);
    }

    private void OnHealthChanged(float currentHealth, float maxHealth)
    {
        if (currentHealth <= 0f && !isDead)
        {
            isDead = true;
            ShowDeathScreen();
        }
    }

    private void ShowDeathScreen()
    {
        Time.timeScale = 0f;

        if (deathScreenPanel != null)
            deathScreenPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    private void OnDestroy()
    {
        if (playerHealth != null)
            playerHealth.OnHealthChanged -= OnHealthChanged;
    }
}