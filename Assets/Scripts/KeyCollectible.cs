using UnityEngine;

public class KeyCollectible : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //  SOM DE COLETA DE CHAVE (índice 3)
            FindObjectOfType<AudioController>()?.TocarEfeito(3);

            // Aqui você pode incrementar contador de chaves
            // Exemplo: GameManager.Instance.AddKey();

            Destroy(gameObject);
        }
    }
}