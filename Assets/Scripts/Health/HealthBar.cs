using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("Referências")]
    public Slider healthSlider;
    public PlayerHealth playerHealth;

    void Start()
    {
        // Procura automaticamente o PlayerHealth
        if (playerHealth == null)
        {
            playerHealth = FindObjectOfType<PlayerHealth>();
        }

        if (playerHealth != null)
        {
            // Configura o slider
            healthSlider.minValue = 0f;
            healthSlider.maxValue = playerHealth.maxHealth;
            healthSlider.value = playerHealth.maxHealth;

            // Escuta mudanças de vida
            playerHealth.OnHealthChanged += UpdateHealthUI;
        }
        else
        {
            Debug.LogError(
                "HealthBar: Nenhum PlayerHealth encontrado na cena!"
            );
        }
    }

    void UpdateHealthUI(float currentHealth, float maxHealth)
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }
    }

    void OnDestroy()
    {
        // Remove o evento para evitar erros
        if (playerHealth != null)
        {
            playerHealth.OnHealthChanged -= UpdateHealthUI;
        }
    }
}