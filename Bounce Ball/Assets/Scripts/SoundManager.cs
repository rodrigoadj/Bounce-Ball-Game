using UnityEngine.SceneManagement;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager intanceSound;
    public AudioSource[] audioReprodutorSource;
    public AudioClip[] SFX_Botao;
    public AudioClip[] SFX_Outros;
    public AudioClip[] SFX_PlayListMenu;
    public AudioClip[] SFX_PlayListGame;

    private void Awake()
    {
        intanceSound = GetComponent<SoundManager>();
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
            Invoke(nameof(PlayListMenu), 1f);

        if (SceneManager.GetActiveScene().buildIndex == 1)
            Invoke(nameof(PlayListGame), 1f);
    }

    public void PlaySfxBotaoVoltar()
    {
        audioReprodutorSource[0].clip = SFX_Botao[0];
        audioReprodutorSource[0].Play();
    }

    public void PlaySfxBotoes()
    {
        audioReprodutorSource[0].clip = SFX_Botao[1];
        audioReprodutorSource[0].Play();
    }

    public void PlayListMenu()
    {

        if (!audioReprodutorSource[1].isPlaying)
        {
            audioReprodutorSource[1].clip = SFX_PlayListMenu[Random.Range(0, SFX_PlayListMenu.Length)];
            audioReprodutorSource[1].Play();
        }
        Invoke(nameof(PlayListMenu), 1f);
    }

    public void PlayListGame()
    {
        if (!audioReprodutorSource[1].isPlaying)
        {
            audioReprodutorSource[1].clip = SFX_PlayListGame[Random.Range(0, SFX_PlayListGame.Length)];
            audioReprodutorSource[1].Play();
        }
        Invoke(nameof(PlayListGame), 1f);
    }

    public void PlayResetDados()
    {
        audioReprodutorSource[0].clip = SFX_Outros[0];
        audioReprodutorSource[0].Play();
    }

    public void PlayVitoria()
    {
        audioReprodutorSource[1].clip = SFX_Outros[1];
        audioReprodutorSource[1].Play();
    }

    public void PlayCarregar(int _rola)
    {
        if (_rola == 1)
        {
            audioReprodutorSource[0].clip = SFX_Outros[3];
            audioReprodutorSource[0].Play();
        }
        else
        {
            audioReprodutorSource[0].Stop();
        }
    }

    public void PlayTiro()
    {
        audioReprodutorSource[0].clip = SFX_Outros[2];
        audioReprodutorSource[0].Play();
    }



}
