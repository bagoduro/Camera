using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public float cooldown = 2f;
    private float timer;
    private bool canAttack = true;
    public Sword sword;

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

        if (Keyboard.current.spaceKey.wasPressedThisFrame && canAttack)
        {
            Attack();
        }
    }

    void Attack()
    {
        canAttack = false;
        sword.Attack();
        Debug.Log("Attack performed!");
    }
}