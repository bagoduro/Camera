using UnityEngine;

public class BananaPowerup : MonoBehaviour
{
    [Header("Configuração da Banana")]
    public float bonusSpeed = 5f;
    public float duration = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 🔥 SOM DE COMENDO BANANA (índice 4)
            FindObjectOfType<AudioController>()?.TocarEfeito(4);

            // 🔥 SOM DE ATIVAÇÃO DO POWER-UP (índice 13 - ItemPegando)
            FindObjectOfType<AudioController>()?.TocarEfeito(13);

            PlayerPowerupController playerPowerup = other.GetComponent<PlayerPowerupController>();
            if (playerPowerup != null)
            {
                playerPowerup.ActivateBananaBoost(bonusSpeed, duration);
            }

            Destroy(gameObject);
        }
    }
}