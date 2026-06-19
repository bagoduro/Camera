using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
    private AudioController audioController;

    void Start()
    {
        audioController = FindObjectOfType<AudioController>();
    }

    // 🔥 BOTÃO INICIAR JOGO
    public void StartGame()
    {
        if (audioController != null)
            audioController.TocarEfeito(16); // Confirmação (ajuste o índice se necessário)

        StartCoroutine(CarregarCenaDelay(1)); // 1 = índice da cena de jogo
    }

    IEnumerator CarregarCenaDelay(int sceneIndex)
    {
        yield return new WaitForSecondsRealtime(0.1f); // 0.1s para o som tocar

        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene(sceneIndex);
    }

    // 🔥 BOTÃO SAIR
    public void Quit()
    {
        if (audioController != null)
            audioController.TocarEfeito(15); // Clique (ajuste o índice)

        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}