using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    // Esta função é chamada automaticamente pela Unity quando ocorre um impacto físico
    void OnCollisionEnter(Collision collision)
    {
        // Imprime no Console o nome do objeto em que batemos
        Debug.Log("Colisão detectada com: " + collision.gameObject.name);
    }
}