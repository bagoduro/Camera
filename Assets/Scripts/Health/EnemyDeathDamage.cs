using UnityEngine;

public class EnemyDeathDamage : MonoBehaviour
{
    public float damage = 15f;
    public float radius = 3f;
    public ParticleSystem explosionEffect;

    void OnDestroy()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (radius > 0 && distance > radius) return;

        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
            Debug.Log($"Caixa explodiu e causou {damage} de dano!");
        }

        // 🔥 SOM DE EXPLOSÃO (índice 8) via AudioController
        FindObjectOfType<AudioController>()?.TocarEfeito(8);

        if (explosionEffect != null)
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}