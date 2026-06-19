using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerPowerupController : MonoBehaviour
{
    private PlayerController playerController;
    private float originalSpeed;
    private bool isBoosted = false;
    private float boostTimer;
    private float totalBoostDuration;

    [Header("UI Elements (opcional)")]
    public Slider boostSlider;
    public TextMeshProUGUI remainingTimeText;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        if (playerController != null) originalSpeed = playerController.speed;
        else Debug.LogError("PlayerController não encontrado!");

        if (boostSlider != null) boostSlider.gameObject.SetActive(false);
        if (remainingTimeText != null) remainingTimeText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isBoosted)
        {
            boostTimer -= Time.deltaTime;
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
            float normalizedTime = Mathf.Clamp01(boostTimer / totalBoostDuration);
            boostSlider.value = normalizedTime;
        }

        if (remainingTimeText != null)
        {
            int secondsLeft = Mathf.CeilToInt(boostTimer);
            remainingTimeText.text = secondsLeft.ToString() + "s";
        }
    }

    public void ActivateBananaBoost(float bonusSpeed, float duration)
    {
        if (playerController == null) return;

        if (isBoosted)
        {
            boostTimer = duration;
            totalBoostDuration = duration;
            Debug.Log("Banana renovada!");
        }
        else
        {
            originalSpeed = playerController.speed;
            playerController.speed += bonusSpeed;
            isBoosted = true;
            boostTimer = duration;
            totalBoostDuration = duration;
            Debug.Log("Banana powerup ativado!");

            if (boostSlider != null) boostSlider.gameObject.SetActive(true);
            if (remainingTimeText != null) remainingTimeText.gameObject.SetActive(true);
        }
    }

    private void RemoveBananaBoost()
    {
        if (playerController != null) playerController.speed = originalSpeed;
        isBoosted = false;

        // 🔥 SOM DE FIM DO POWER-UP (índice 12 - Hit)
        FindObjectOfType<AudioController>()?.TocarEfeito(12);

        if (boostSlider != null) boostSlider.gameObject.SetActive(false);
        if (remainingTimeText != null) remainingTimeText.gameObject.SetActive(false);

        Debug.Log("Efeito da banana terminou.");
    }
}