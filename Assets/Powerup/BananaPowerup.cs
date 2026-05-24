using UnityEngine;

public class BananaPowerup : MonoBehaviour
{
    [Header("Configuração da Banana")]
    public float bonusSpeed = 5f;      // aumento de velocidade
    public float duration = 10f;       // duração do efeito (segundos)
    public AudioClip pickupSound;      // som ao pegar (opcional)

    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto que tocou tem a tag "Player"
        if (other.CompareTag("Player"))
        {
            // Tenta obter o script que controla os powerups no jogador
            PlayerPowerupController playerPowerup = other.GetComponent<PlayerPowerupController>();
            if (playerPowerup != null)
            {
                playerPowerup.ActivateBananaBoost(bonusSpeed, duration);
            }

            // Toca o som (se atribuído)
            if (pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            }

            // Destroi a banana após ser coletada
            Destroy(gameObject);
        }
    }
}