using UnityEngine;

public class StatusThings : MonoBehaviour
{
    private float life = 10f;

    public void TakeDamage(float damage)
    {
        life -= damage;
        Debug.Log("Objeto genérico levou " + damage + " de dano. Vida restante: " + life);

        if (life <= 0)
        {
            //  SOM DE CAIXA (índice 0)
            FindObjectOfType<AudioController>()?.TocarEfeito(0);
            Destroy(gameObject);
        }
    }
}