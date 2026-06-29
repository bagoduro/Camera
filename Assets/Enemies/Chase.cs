using UnityEngine;
using UnityEngine.AI;

public class Chase : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent agent;

    [Header("Detecção")]
    public float distanciaDeteccao = 30f; // Distância para começar a perseguir

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();

        if (player == null)
            Debug.LogError("Nenhum objeto com a tag 'Player' foi encontrado na cena!");

        if (agent == null)
            Debug.LogError("O componente NavMeshAgent está faltando neste GameObject!");
    }

    void Update()
    {
        if (player == null || agent == null) return;

        float distancia = Vector3.Distance(transform.position, player.transform.position);

        if (distancia <= distanciaDeteccao)
        {
            // Player dentro do alcance — perseguir
            agent.SetDestination(player.transform.position);
        }
        else
        {
            // Player fora do alcance — parar
            agent.ResetPath();
        }
    }

    // Desenha o raio de detecção no Editor para facilitar ajuste
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanciaDeteccao);
    }
}
