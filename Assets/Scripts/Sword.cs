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

        // 1. INIMIGO
        EnemyStatus enemy = collision.gameObject.GetComponent<EnemyStatus>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            return;
        }

        // 2. CAIXA DO BEM
        StatusThings thing = collision.gameObject.GetComponent<StatusThings>();
        if (thing != null)
        {
            thing.TakeDamage(damage);
            return;
        }

        // 3. CAIXA EXPLOSIVA
        ExplosiveCrate crate = collision.gameObject.GetComponent<ExplosiveCrate>();
        if (crate != null)
        {
            crate.TakeDamage(damage);
            return;
        }

        //  NADA MAIS ACONTECE! SEM SOM DE HIT, SEM DANO EM PAREDES/CHÃO.
    }
}