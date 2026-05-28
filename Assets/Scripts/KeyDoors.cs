using UnityEngine;

public class KeyDoors : MonoBehaviour
{
    public GameObject targetDoor;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(targetDoor);
            Destroy(this.gameObject);
        }
    }
}