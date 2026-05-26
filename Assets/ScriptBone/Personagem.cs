using UnityEngine;

public class Personagem : MonoBehaviour
{
    [SerializeField] private GameObject boneVisual; // Arraste o filho "BoneVisual" aqui no Inspector

    public void EquiparBone()
    {
        if (boneVisual != null)
        {
            boneVisual.SetActive(true);
            Debug.Log("Boné equipado!");
        }
    }
}