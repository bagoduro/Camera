using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        // Garante que o jogo não fique pausado
        Time.timeScale = 1f;

        // Mouse travado para gameplay
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Carrega gameplay
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}