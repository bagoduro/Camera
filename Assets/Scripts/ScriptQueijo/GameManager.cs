using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Vitória")]
    public int queijosNecessarios = 3;
    private int queijosColetados = 0;

    [Header("Timer")]
    public float tempoLimite = 180f; // 3 minutos
    private float tempoRestante;
    private bool timerAtivo = true;

    [Header("Tic-Tac de Alerta")]
    public AudioClip ticTacClip;
    public float tempoAlerta = 10f; // segundos finais para tocar o tic-tac
    private AudioSource ticTacSource;
    private bool ticTacIniciado = false;

    [Header("UI")]
    public TextMeshProUGUI contadorText;
    public TextMeshProUGUI timerText;
    public GameObject victoryPanel;
    public GameObject defeatPanel;
    public TextMeshProUGUI defeatTitleText; // Arraste o TextMeshPro "Death" aqui
    public Button restartButton;
    public Button mainMenuButton;
    public Button restartButtonDerrota;
    public Button mainMenuButtonDerrota;

    [Header("Recorde")]
    public TextMeshProUGUI recordeText;
    public TextMeshProUGUI recordeAtualText;

    [Header("Cores do Timer")]
    public Color corNormal = Color.white;
    public Color corAviso = Color.yellow;   // abaixo de 60s
    public Color corPerigo = Color.red;     // abaixo de 30s

    private bool jogoTerminou = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        tempoRestante = tempoLimite;

        if (victoryPanel != null) victoryPanel.SetActive(false);
        if (defeatPanel != null) defeatPanel.SetActive(false);

        // Cria o AudioSource para o tic-tac
        ticTacSource = gameObject.AddComponent<AudioSource>();
        ticTacSource.clip = ticTacClip;
        ticTacSource.loop = true;
        ticTacSource.playOnAwake = false;
        ticTacSource.volume = 0.6f;

        AtualizarContadorUI();
        AtualizarTimerUI();
    }

    void Update()
    {
        if (jogoTerminou || !timerAtivo) return;

        tempoRestante -= Time.deltaTime;

        // Inicia o tic-tac nos últimos segundos
        if (!ticTacIniciado && tempoRestante <= tempoAlerta && tempoRestante > 0f)
        {
            ticTacIniciado = true;
            if (ticTacSource != null && ticTacClip != null)
                ticTacSource.Play();
        }

        if (tempoRestante <= 0f)
        {
            tempoRestante = 0f;
            AtualizarTimerUI();
            PararTicTac();
            Derrota();
            return;
        }

        AtualizarTimerUI();
    }

    void PararTicTac()
    {
        if (ticTacSource != null && ticTacSource.isPlaying)
            ticTacSource.Stop();
    }

    public void ColetarQueijo()
    {
        if (jogoTerminou) return;

        queijosColetados++;
        AtualizarContadorUI();

        if (queijosColetados >= queijosNecessarios)
        {
            FindObjectOfType<AudioController>()?.TocarEfeito(1);
            StartCoroutine(VitoriaDelay());
        }
    }

    IEnumerator VitoriaDelay()
    {
        yield return new WaitForSeconds(0.2f);
        Vitoria();
    }

    void AtualizarContadorUI()
    {
        if (contadorText != null)
            contadorText.text = $"Queijos: {queijosColetados}/{queijosNecessarios}";
    }

    void AtualizarTimerUI()
    {
        if (timerText == null) return;

        int minutos = Mathf.FloorToInt(tempoRestante / 60f);
        int segundos = Mathf.FloorToInt(tempoRestante % 60f);
        timerText.text = $"{minutos:00}:{segundos:00}";

        if (tempoRestante <= 30f)
            timerText.color = corPerigo;
        else if (tempoRestante <= 60f)
            timerText.color = corAviso;
        else
            timerText.color = corNormal;
    }

    void Vitoria()
    {
        jogoTerminou = true;
        timerAtivo = false;

        PararTicTac();

        float tempoUsado = tempoLimite - tempoRestante;

        float melhorTempo = PlayerPrefs.GetFloat("MelhorTempo", float.MaxValue);
        bool novoRecorde = tempoUsado < melhorTempo;

        if (novoRecorde)
        {
            PlayerPrefs.SetFloat("MelhorTempo", tempoUsado);
            PlayerPrefs.Save();
            melhorTempo = tempoUsado;
        }

        FindObjectOfType<AudioController>()?.TocarEfeito(2);

        Time.timeScale = 0f;

        if (victoryPanel != null)
            victoryPanel.SetActive(true);

        if (recordeAtualText != null)
        {
            int min = Mathf.FloorToInt(tempoUsado / 60f);
            int seg = Mathf.FloorToInt(tempoUsado % 60f);
            recordeAtualText.text = novoRecorde
                ? $"Novo Recorde: {min:00}:{seg:00}! "
                : $"Seu tempo: {min:00}:{seg:00}";
        }

        if (recordeText != null)
        {
            int min = Mathf.FloorToInt(melhorTempo / 60f);
            int seg = Mathf.FloorToInt(melhorTempo % 60f);
            recordeText.text = $"Recorde: {min:00}:{seg:00}";
        }

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

    void Derrota()
    {
        jogoTerminou = true;
        timerAtivo = false;

        // Troca o texto para "Tempo Esgotado!"
        if (defeatTitleText != null)
            defeatTitleText.text = "Tempo Esgotado!";

        // Som de falha por tempo (índice 20)
        FindObjectOfType<AudioController>()?.TocarEfeito(20);

        Time.timeScale = 0f;

        if (defeatPanel != null)
            defeatPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (restartButtonDerrota != null)
            restartButtonDerrota.onClick.AddListener(RestartGame);
        if (mainMenuButtonDerrota != null)
            mainMenuButtonDerrota.onClick.AddListener(GoToMainMenu);
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
