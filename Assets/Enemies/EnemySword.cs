using UnityEngine;

public class EnemySword : MonoBehaviour
{
    public float damage = 15f;
    public float cooldown = 1f;
    private float lastHitTime;

    private void OnTriggerEnter(Collider other)
    {
        if (Time.time - lastHitTime < cooldown) return;

        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                lastHitTime = Time.time;
                Debug.Log("Inimigo acertou o jogador com a faca!");
            }
        }
    }
}