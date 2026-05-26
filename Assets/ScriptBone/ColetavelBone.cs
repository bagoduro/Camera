using UnityEngine;

public class ColetavelBone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Verifica se quem entrou no gatilho é o personagem
        if (other.CompareTag("Player"))
        {
            // Tenta encontrar o script do personagem que tem o método para equipar o boné
            Personagem personagem = other.GetComponent<Personagem>();
            if (personagem != null)
            {
                personagem.EquiparBone();
            }

            // Destroi o objeto do boné coletável
            Destroy(gameObject);
        }
    }
}