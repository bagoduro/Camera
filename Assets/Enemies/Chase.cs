using UnityEngine;
using UnityEngine.AI; // Necessário para o NavMeshAgent

public class Chase : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent agent;

    void Start()
    {
        // Encontra o objeto com a tag "Player"
        player = GameObject.FindGameObjectWithTag("Player");
        
        // Obtém o componente NavMeshAgent anexado ao mesmo GameObject
        agent = GetComponent<NavMeshAgent>();

        // Opcional: verificar se encontrou o player e o agent
        if (player == null)
            Debug.LogError("Nenhum objeto com a tag 'Player' foi encontrado na cena!");
        
        if (agent == null)
            Debug.LogError("O componente NavMeshAgent está faltando neste GameObject!");
    }

    void Update()
    {
        // Se o player ou o agent forem nulos, não faz nada
        if (player == null || agent == null)
            return;

        // Define a posição do player como destino do NavMeshAgent
        agent.SetDestination(player.transform.position);
    }
}