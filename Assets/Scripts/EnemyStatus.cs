using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    private float life = 10f;

    public void TakeDamage(float damage)
    {
        life -= damage;
        Debug.Log("Inimigo levou " + damage + " de dano. Vida restante: " + life);

        if (life <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}