using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Vitória")]
    public int queijosNecessarios = 3;
    private int queijosColetados = 0;

    [Header("UI")]
    public TextMeshProUGUI contadorText;     // texto "Queijos: 0/3"
    public GameObject victoryPanel;          // painel de vitória
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
            Vitoria();
        }
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

        //  SOM DE VITÓRIA (índice 2)
        FindObjectOfType<AudioController>()?.TocarEfeito(2);

        Time.timeScale = 0f;  // pausa o jogo

        if (victoryPanel != null)
            victoryPanel.SetActive(true);

        // Esconde o menu de pause se estiver ativo (evita conflito)
        InGameController igc = FindObjectOfType<InGameController>();
        if (igc != null && igc.pauseMenu != null)
            igc.pauseMenu.SetActive(false);

        // Libera o cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Configura botões
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
        SceneManager.LoadScene(0); // cena do menu principal
    }
}