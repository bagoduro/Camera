using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    public GameObject portaParaAbrir; // Arraste a porta aqui no Inspetor

    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            // 🔥 SOM DE PORTA ABRINDO (índice 18)
            FindObjectOfType<AudioController>()?.TocarEfeito(18);

            // Se houver uma porta referenciada, destrua-a (ou abra)
            if (portaParaAbrir != null)
            {
                Destroy(portaParaAbrir);
            }

            // Destroi o grupo de inimigos
            Destroy(this.gameObject);
        }
    }
}