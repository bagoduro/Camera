using UnityEngine;
using UnityEngine.UI;          // Para Slider
using TMPro;                   // Para TextMeshPro (opcional)

public class PlayerPowerupController : MonoBehaviour
{
    private PlayerController playerController;
    private float originalSpeed;
    private bool isBoosted = false;
    private float boostTimer;
    private float totalBoostDuration;   // Duração total para o cálculo da barra

    [Header("UI Elements (opcional)")]
    public Slider boostSlider;          // Referência à Slider da UI
    public TextMeshProUGUI remainingTimeText; // Texto para mostrar segundos restantes

    void Start()
    {
        playerController = GetComponent<PlayerController>();

        if (playerController != null)
        {
            originalSpeed = playerController.speed;
        }
        else
        {
            Debug.LogError("PlayerController não encontrado no jogador!");
        }

        // Desativa os elementos de UI no início
        if (boostSlider != null)
            boostSlider.gameObject.SetActive(false);
        if (remainingTimeText != null)
            remainingTimeText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isBoosted)
        {
            boostTimer -= Time.deltaTime;
            
            // Atualiza a barra e o texto
            UpdateBoostUI();

            if (boostTimer <= 0f)
            {
                RemoveBananaBoost();
            }
        }
    }

    private void UpdateBoostUI()
    {
        if (boostSlider != null)
        {
            // Normaliza o tempo restante (0 a 1) e define no slider
            float normalizedTime = Mathf.Clamp01(boostTimer / totalBoostDuration);
            boostSlider.value = normalizedTime;
        }

        if (remainingTimeText != null)
        {
            // Mostra segundos restantes arredondados para cima
            int secondsLeft = Mathf.CeilToInt(boostTimer);
            remainingTimeText.text = secondsLeft.ToString() + "s";
        }
    }

    public void ActivateBananaBoost(float bonusSpeed, float duration)
    {
        if (playerController == null) return;

        if (isBoosted)
        {
            // Renova o tempo, mas mantém a duração total original
            boostTimer = duration;
            totalBoostDuration = duration;
            Debug.Log("Banana renovada! Tempo resetado para " + duration);
        }
        else
        {
            originalSpeed = playerController.speed;
            playerController.speed += bonusSpeed;
            isBoosted = true;
            boostTimer = duration;
            totalBoostDuration = duration;
            Debug.Log("Banana powerup ativado! Velocidade aumentada em " + bonusSpeed);
            
            // Ativa os elementos de UI
            if (boostSlider != null)
                boostSlider.gameObject.SetActive(true);
            if (remainingTimeText != null)
                remainingTimeText.gameObject.SetActive(true);
        }
    }

    private void RemoveBananaBoost()
    {
        if (playerController != null)
        {
            playerController.speed = originalSpeed;
        }
        isBoosted = false;
        
        // Desativa os elementos de UI
        if (boostSlider != null)
            boostSlider.gameObject.SetActive(false);
        if (remainingTimeText != null)
            remainingTimeText.gameObject.SetActive(false);
        
        Debug.Log("Efeito da banana terminou. Velocidade voltou ao normal.");
    }
}