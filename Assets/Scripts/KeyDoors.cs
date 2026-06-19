using UnityEngine;

public class KeyDoors : MonoBehaviour
{
    public GameObject targetDoor;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 🔥 SOM DE ABRIR PORTA (índice 18)
            FindObjectOfType<AudioController>()?.TocarEfeito(18);

            Destroy(targetDoor);
            Destroy(this.gameObject);
        }
    }
}