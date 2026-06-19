using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Vida")]
    public float maxHealth = 100f;

    private float currentHealth;

    public System.Action<float, float> OnHealthChanged;

    public bool useDeathScreen = false;
    public System.Action OnPlayerDied;

    void Start()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth <= 0f)
            return;

        currentHealth -= damage;

        if (currentHealth < 0f)
            currentHealth = 0f;

        // 🔥 SOM DE DANO (índice 11 - GemidoDor)
        FindObjectOfType<AudioController>()?.TocarEfeito(11);

        Debug.Log($"Dano recebido: {damage}. Vida atual: {currentHealth}");

        OnHealthChanged?.Invoke(currentHealth, maxHealth);

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        // 🔥 SOM DE MORTE DO JOGADOR (índice 14 - Morte)
        FindObjectOfType<AudioController>()?.TocarEfeito(14);

        Debug.Log("Jogador morreu! Recarregando cena...");

        if (!useDeathScreen)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            OnPlayerDied?.Invoke();
        }
    }
}