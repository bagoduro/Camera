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

        // 1. Verifica se é um INIMIGO (com sons)
        EnemyStatus enemy = collision.gameObject.GetComponent<EnemyStatus>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            return; // Sai da função para não executar o próximo
        }

        // 2. Verifica se é um OBJETO GENÉRICO (sem sons de inimigo)
        StatusThings thing = collision.gameObject.GetComponent<StatusThings>();
        if (thing != null)
        {
            thing.TakeDamage(damage);
        }
    }
}