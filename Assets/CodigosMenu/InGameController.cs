using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI; // Garante o acesso aos botões da UI

public class InGameController : MonoBehaviour
{
    public GameObject pauseMenu;
    
    [Header("Arraste o botão CONTINUAR aqui")]
    public Button botaoContinuar; 
    
    bool isPaused = false;

    void Start()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;

        // Configura o clique do botão diretamente por código, ignorando o OnClick do Inspector
        if (botaoContinuar != null)
        {
            botaoContinuar.onClick.RemoveAllListeners();
            botaoContinuar.onClick.AddListener(Pause);
        }
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Pause();
        }
    }

    public void MainMenu()
    {
        Time.timeScale = 1; // Garante que o tempo despausa ao ir pro menu
        SceneManager.LoadScene(0);
    }

    public void Pause()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            isPaused = false;

            // Se seu jogo for em 1ª ou 3ª pessoa, descomente as linhas abaixo tirando as "//"
            // Cursor.lockState = CursorLockMode.Locked;
            // Cursor.visible = false;
        }
        else
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            isPaused = true;

            // Força o mouse a aparecer e ficar solto para clicar na tela
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}