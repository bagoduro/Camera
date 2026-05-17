using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    private float life = 10f;

    public void TakeDamage(float damage)
    {
        life -= damage;
        Debug.Log("Enemy took " + damage + " damage. Remaining life: " + life);

        if (life <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}