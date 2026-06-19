using UnityEngine;

public class CheeseCollectible : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 🔥 SOM DE PEGANDO QUEIJO (índice 17)
            FindObjectOfType<AudioController>()?.TocarEfeito(17);

            // Avisa o GameManager
            if (GameManager.Instance != null)
            {
                GameManager.Instance.ColetarQueijo();
            }

            // Remove o queijo da cena
            Destroy(gameObject);
        }
    }
}