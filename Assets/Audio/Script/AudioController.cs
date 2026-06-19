using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    [Header("Áudio Sources")]
    public AudioSource musicaSource;   // Para tocar as músicas
    public AudioSource efeitoSource;   // Para tocar efeitos

    [Header("Clipes")]
    public AudioClip musicaMenu;
    public AudioClip musicaGame;
    public AudioClip[] efeitos;        // Array com os 20 efeitos

    [Header("Mixer")]
    public AudioMixer mixer;

    [Header("Sliders da UI")]
    public Slider sliderMusica;
    public Slider sliderEfeitos;

    // Nomes dos parâmetros expostos no mixer
    private const string PARAM_MUSICA = "VolMusica";
    private const string PARAM_EFEITOS = "VolEfeitos";

    void Start()
    {
        // Carrega os valores salvos
        float volMusica = PlayerPrefs.GetFloat("VolMusica", 0f);
        float volEfeitos = PlayerPrefs.GetFloat("VolEfeitos", 0f);
        mixer.SetFloat(PARAM_MUSICA, volMusica);
        mixer.SetFloat(PARAM_EFEITOS, volEfeitos);

        // Sincroniza os sliders
        if (sliderMusica) sliderMusica.value = Mathf.Clamp01(volMusica / 80f + 1f);
        if (sliderEfeitos) sliderEfeitos.value = Mathf.Clamp01(volEfeitos / 80f + 1f);

        // Adiciona listeners
        if (sliderMusica) sliderMusica.onValueChanged.AddListener(AlterarVolumeMusica);
        if (sliderEfeitos) sliderEfeitos.onValueChanged.AddListener(AlterarVolumeEfeitos);
    }

    // --- Controle de Músicas ---
    public void TocarMusicaMenu()
    {
        musicaSource.clip = musicaMenu;
        musicaSource.Play();
    }

    public void TocarMusicaGame()
    {
        musicaSource.clip = musicaGame;
        musicaSource.Play();
    }

    public void PararMusica()
    {
        musicaSource.Stop();
    }

    // --- Controle de Efeitos (MODIFICADO para PlayOneShot) ---
    public void TocarEfeito(int indice)
    {
        if (efeitoSource != null && indice >= 0 && indice < efeitos.Length && efeitos[indice] != null)
        {
            // PlayOneShot toca o som independente do GameObject, não é cortado ao carregar cena
            efeitoSource.PlayOneShot(efeitos[indice]);
        }
        else
        {
            Debug.LogWarning("Índice de efeito inválido ou clipe nulo: " + indice);
        }
    }

    // --- Controle de Volume via Sliders ---
    public void AlterarVolumeMusica(float valorSlider)
    {
        float db = Mathf.Lerp(-80f, 0f, valorSlider);
        mixer.SetFloat(PARAM_MUSICA, db);
        PlayerPrefs.SetFloat("VolMusica", db);
    }

    public void AlterarVolumeEfeitos(float valorSlider)
    {
        float db = Mathf.Lerp(-80f, 0f, valorSlider);
        mixer.SetFloat(PARAM_EFEITOS, db);
        PlayerPrefs.SetFloat("VolEfeitos", db);
    }

    public void DefinirVolumeMusica(float db)
    {
        mixer.SetFloat(PARAM_MUSICA, db);
        if (sliderMusica) sliderMusica.value = Mathf.InverseLerp(-80f, 0f, db);
    }

    public void DefinirVolumeEfeitos(float db)
    {
        mixer.SetFloat(PARAM_EFEITOS, db);
        if (sliderEfeitos) sliderEfeitos.value = Mathf.InverseLerp(-80f, 0f, db);
    }
}