using UnityEngine;
using System.Collections;

public class KeyCollectible : MonoBehaviour
{
    [Header("Porta que será aberta")]
    public GameObject doorToOpen;

    [Header("Tempo para a porta desaparecer após destravar")]
    public float unlockDelay = 0.5f;

    private bool collected = false;

    private void OnTriggerEnter(Collider other)
    {
        if (collected) return;

        if (other.CompareTag("Player"))
        {
            collected = true;
            StartCoroutine(OpenDoorSequence());
        }
    }

    private IEnumerator OpenDoorSequence()
    {
        // Som da chave sendo coletada (índice 3)
        FindObjectOfType<AudioController>()?.TocarEfeito(3);

        yield return new WaitForSeconds(0.2f);

        // Som da fechadura destravando (índice 7)
        FindObjectOfType<AudioController>()?.TocarEfeito(7);

        yield return new WaitForSeconds(unlockDelay);

        // Som de abrir porta (índice 18)
        FindObjectOfType<AudioController>()?.TocarEfeito(18);

        yield return new WaitForSeconds(0.5f);

        // Remove a porta
        if (doorToOpen != null)
            Destroy(doorToOpen);

        // Remove a chave
        Destroy(gameObject);
    }
}
