using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    private float life = 10f;

    public void TakeDamage(float damage)
    {
        life -= damage;
        Debug.Log("Inimigo levou " + damage + " de dano. Vida restante: " + life);

        //  SOM DE DANO (índice 11 - GemidoDor)
        FindObjectOfType<AudioController>()?.TocarEfeito(11);

        if (life <= 0)
        {
            //  SOM DE MORTE DO INIMIGO (índice 15 - Mortelnimigo)
            FindObjectOfType<AudioController>()?.TocarEfeito(0); // Caixa

            Destroy(this.gameObject);
        }
    }
}