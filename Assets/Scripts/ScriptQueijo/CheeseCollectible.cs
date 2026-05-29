using UnityEngine;

public class CheeseCollectible : MonoBehaviour
{
    [Header("Som (opcional)")]
    public AudioClip pickupSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Avisa o GameManager
            if (GameManager.Instance != null)
            {
                GameManager.Instance.ColetarQueijo();
            }

            // Toca som
            if (pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            }

            // Remove o queijo da cena
            Destroy(gameObject);
        }
    }
}