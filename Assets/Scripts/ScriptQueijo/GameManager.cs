using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Collections; // necessário para IEnumerator

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Vitória")]
    public int queijosNecessarios = 3;
    private int queijosColetados = 0;

    [Header("UI")]
    public TextMeshProUGUI contadorText;
    public GameObject victoryPanel;
    public Button restartButton;
    public Button mainMenuButton;

    private bool jogoTerminou = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (victoryPanel != null)
            victoryPanel.SetActive(false);

        AtualizarContadorUI();
    }

    public void ColetarQueijo()
    {
        if (jogoTerminou) return;

        queijosColetados++;
        AtualizarContadorUI();

        if (queijosColetados >= queijosNecessarios)
        {
            // 🔥 Toca o som de "Checado" antes da vitória
            FindObjectOfType<AudioController>()?.TocarEfeito(1); // Checado
            StartCoroutine(VitoriaDelay());
        }
    }

    IEnumerator VitoriaDelay()
    {
        yield return new WaitForSeconds(0.2f); // espera o som tocar
        Vitoria();
    }

    void AtualizarContadorUI()
    {
        if (contadorText != null)
        {
            contadorText.text = $"Queijos: {queijosColetados}/{queijosNecessarios}";
        }
    }

    void Vitoria()
    {
        jogoTerminou = true;

        FindObjectOfType<AudioController>()?.TocarEfeito(2); // Vitória

        Time.timeScale = 0f;

        if (victoryPanel != null)
            victoryPanel.SetActive(true);

        InGameController igc = FindObjectOfType<InGameController>();
        if (igc != null && igc.pauseMenu != null)
            igc.pauseMenu.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (restartButton != null)
            restartButton.onClick.AddListener(RestartGame);
        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(GoToMainMenu);
    }

    void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}