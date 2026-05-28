using UnityEngine;

public class Sword : MonoBehaviour
{
    public float damage = 10f;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Attack()
    {
        if (animator != null)
        {
            animator.Play("AnimacaoFaca 0"); 
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Collidiu com " + collision.gameObject.name);
        EnemyStatus enemy = collision.gameObject.GetComponent<EnemyStatus>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
    }
}