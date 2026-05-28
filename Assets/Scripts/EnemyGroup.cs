using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            Destroy(this.gameObject);
        }
    }
}