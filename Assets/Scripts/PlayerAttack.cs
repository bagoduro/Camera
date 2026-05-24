using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public float cooldown = 2f;
    private float timer;
    private bool canAttack = true;
    public Sword sword;  // Referência para o script Sword

    void Start()
    {
        sword = GetComponentInChildren<Sword>();
        timer = 0f;
    }

    void Update()
    {
        if (!canAttack)
        {
            timer += Time.deltaTime;
            if (timer >= cooldown)
            {
                canAttack = true;
                timer = 0f;
            }
        }

        // Ataca com clique esquerdo do mouse
        if (Mouse.current.leftButton.wasPressedThisFrame && canAttack)
        {
            Attack();
        }
    }

    void Attack()
    {
        canAttack = false;
        if (sword != null)
            sword.Attack();
        Debug.Log("Attack performed!");
    }
}