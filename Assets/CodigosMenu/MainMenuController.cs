using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Necessário para o LoadScene

public class MainMenuController : MonoBehaviour
{
    // Método chamado pelo botão "Jogar" / "Start" para carregar a Gameplay (Cena 1)
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    // Método chamado pelo botão "Sair" / "Quit"
    public void Quit()
    {
        Application.Quit();
        
        // Linha auxiliar para testar o fechamento dentro do editor do Unity
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}