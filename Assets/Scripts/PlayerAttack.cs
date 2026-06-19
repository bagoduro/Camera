using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [Header("Cooldown")]
    public float cooldown = 2f;

    private float timer;
    private bool canAttack = true;
    public Sword sword;

    // 🔥 VARIÁVEL PARA ALTERNAR OS SONS
    private bool usarSomAlternativo = false;

    void Start()
    {
        sword = GetComponentInChildren<Sword>();
        timer = 0f;
    }

    void Update()
    {
        // Cooldown
        if (!canAttack)
        {
            timer += Time.deltaTime;
            if (timer >= cooldown)
            {
                canAttack = true;
                timer = 0f;
            }
        }

        // 🔥 APENAS BOTÃO ESQUERDO PARA ATACAR
        if (Mouse.current.leftButton.wasPressedThisFrame && canAttack)
        {
            Attack();
        }
    }

    void Attack()
    {
        canAttack = false;

        // Executa a animação
        if (sword != null)
            sword.Attack();

        // 🔥 ALTERNA OS SONS A CADA ATAQUE
        if (usarSomAlternativo)
        {
            FindObjectOfType<AudioController>()?.TocarEfeito(10); // FacaCorte2
            Debug.Log("Som alternativo (FacaCorte2)");
        }
        else
        {
            FindObjectOfType<AudioController>()?.TocarEfeito(9);  // FacaCorte
            Debug.Log("Som principal (FacaCorte)");
        }

        // Inverte para o próximo ataque
        usarSomAlternativo = !usarSomAlternativo;

        Debug.Log("Ataque realizado!");
    }
}