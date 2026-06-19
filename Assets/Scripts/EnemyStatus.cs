using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    private float life = 10f;

    public void TakeDamage(float damage)
    {
        life -= damage;
        Debug.Log("Inimigo levou " + damage + " de dano. Vida restante: " + life);

        //  SOM DE DANO (vou usar um som para ambos temporariamente, por ser hit kill)
        FindObjectOfType<AudioController>()?.TocarEfeito(15);

        if (life <= 0)
        {
            //  SOM DE MORTE DO INIMIGO (índice 15 - Mortelnimigo)
            FindObjectOfType<AudioController>()?.TocarEfeito(15);

            //  DESTRÓI COM DELAY DE 0.1s PARA O SOM TOCAR
            Destroy(this.gameObject, 0.1f);
        }
    }
}