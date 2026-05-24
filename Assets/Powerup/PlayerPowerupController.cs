using UnityEngine;

public class PlayerPowerupController : MonoBehaviour
{
    private PlayerController playerController;  // Referência ao seu script de movimento
    private float originalSpeed;
    private bool isBoosted = false;
    private float boostTimer;

    void Start()
    {
        playerController = GetComponent<PlayerController>();

        if (playerController != null)
        {
            originalSpeed = playerController.speed; // Usa a variável 'speed' do seu script
        }
        else
        {
            Debug.LogError("PlayerController não encontrado no jogador!");
        }
    }

    void Update()
    {
        if (isBoosted)
        {
            boostTimer -= Time.deltaTime;
            if (boostTimer <= 0f)
            {
                RemoveBananaBoost();
            }
        }
    }

    public void ActivateBananaBoost(float bonusSpeed, float duration)
    {
        if (playerController == null) return;

        if (isBoosted)
        {
            boostTimer = duration; // Renova o tempo se já estiver ativo
        }
        else
        {
            originalSpeed = playerController.speed;
            playerController.speed += bonusSpeed;
            isBoosted = true;
            boostTimer = duration;
            Debug.Log("Banana powerup ativado! Velocidade aumentada em " + bonusSpeed);
        }
    }

    private void RemoveBananaBoost()
    {
        if (playerController != null)
        {
            playerController.speed = originalSpeed;
        }
        isBoosted = false;
        Debug.Log("Efeito da banana terminou. Velocidade voltou ao normal.");
    }
}