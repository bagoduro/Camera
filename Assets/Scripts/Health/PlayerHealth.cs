using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Vida")]
    public float maxHealth = 100f;

    private float currentHealth;

    // Evento para atualizar a UI
    public System.Action<float, float> OnHealthChanged;

    void Start()
    {
        currentHealth = maxHealth;

        // Atualiza a barra com a vida inicial
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public void TakeDamage(float damage)
    {
        // Impede dano após morrer
        if (currentHealth <= 0f)
            return;

        currentHealth -= damage;

        // Impede valor negativo
        if (currentHealth < 0f)
            currentHealth = 0f;

        Debug.Log($"Dano recebido: {damage}. Vida atual: {currentHealth}");

        // Atualiza a UI
        OnHealthChanged?.Invoke(currentHealth, maxHealth);

        // Verifica morte
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Jogador morreu! Recarregando cena...");

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}