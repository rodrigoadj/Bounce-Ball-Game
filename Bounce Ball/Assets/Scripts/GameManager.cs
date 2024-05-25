using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static int contadorQuicada;
    public static bool venceu;
    public bool comecarJogo;
    public bool movCamera, podeAtirar;
    public int faseAtual, quantidadeFase, contadorTentativa;
    [SerializeField] private GameObject painelBotoesCam, painelVitoria, painelPause;
    [SerializeField] private Transform posCamera, posCanhao, posChegada;
    [SerializeField] private Transform[] transformCamera, transformCanhao, transformChegada;
    [SerializeField] private Vector2[] contadorRecord;
    [SerializeField] private GameObject estrela;
    private Button btn_MovCam;
    private Cannon cannon;





    void Awake()
    {
        btn_MovCam = GameObject.Find("BTN_MoveCamera").GetComponent<Button>();
        cannon = GameObject.Find("Canhão").GetComponent<Cannon>();
        posCamera = GameObject.Find("Camera").transform;
    }

    void Start()
    {
        faseAtual = FaseID.faseID;
        painelVitoria.SetActive(false);
        comecarJogo = true;
        AtualizaInformacao();
        podeAtirar = true;
    }

    void Update()
    {
        if (comecarJogo)
        {
            MostrarInformacao();
            SeguirBola();
            LiberarDisparo();
            //AbrirPainelVitoria();
        }
    }

    void MostrarEstrelas()
    {
        if (venceu)
        {
            if (contadorTentativa == 1)
                estrela.SetActive(true);
        }
    }

    void MostrarInformacao() // Implementar A UI
    {
        contadorRecord[faseAtual] = new Vector2(contadorQuicada, contadorTentativa);
    }

    void AtualizaInformacao()
    {
        posCamera.position = transformCamera[faseAtual].position;
        posCanhao.position = transformCanhao[faseAtual].position;
        posChegada.position = transformChegada[faseAtual].position;
        painelVitoria.SetActive(false);
        contadorQuicada = 0;
        contadorTentativa = 0;
    }

    void LiberarDisparo()
    {
        if (!venceu)
        {
            if (cannon.bala == null && !movCamera)
            {
                btn_MovCam.interactable = true;
                posCamera.position = transformCamera[faseAtual].position;
                StartCoroutine(TempoDeResposta());
            }
        }
    }

    void SeguirBola()
    {
        if (!venceu)
        {
            if (cannon.bala != null)
            {
                btn_MovCam.interactable = false;
                podeAtirar = false;
                posCamera.position = Vector2.MoveTowards(posCamera.position, cannon.bala.transform.position, 1f);
            }
        }
    }

    void DelayJogo()
    {
        comecarJogo = true;
    }

    public void AbrirPainelVitoria()
    {
        if (venceu)
        {
            MostrarEstrelas();
            btn_MovCam.interactable = false;
            painelVitoria.SetActive(true);
        }
    }

    public void BTN_ReiniciarFase()
    {
        venceu = false;
        AtualizaInformacao();
        painelVitoria.SetActive(false);
        estrela.SetActive(false);
    }

    public void BTN_PassarDeFase() //Botão passar de fase
    {
        if (venceu)
        {
            cannon.bala = null;
            faseAtual++;
            if (faseAtual > FaseID.idFaseConcluida)
                FaseID.idFaseConcluida = faseAtual;
            PlayerPrefs.SetInt("Tentativas", contadorTentativa);
            AtualizaInformacao();
            estrela.SetActive(false);



            venceu = false;
        }
    }

    public void BTN_MoveCamera()
    {
        movCamera = !movCamera;

        // Atualiza o estado do botão e a posição da câmera
        if (movCamera)
        {
            painelBotoesCam.SetActive(true);
            podeAtirar = false;
        }
        else
        {
            posCamera.position = transformCamera[faseAtual].position;
            painelBotoesCam.SetActive(false);
            StartCoroutine(TempoDeResposta());
        }
    }

    public void BTN_PausarJogo()
    {
        comecarJogo = false;
        painelPause.SetActive(true);
        Time.timeScale = 0;
    }

    public void BTN_Continue()
    {
        Time.timeScale = 1;
        Invoke(nameof(DelayJogo), 0.2f);
        painelPause.SetActive(false);
    }

    public void BTN_VoltarMenu()
    {
        Time.timeScale = 1;
        painelPause.SetActive(false);
        SceneManager.LoadScene("Menu");
    }

    public void BTN_ReiniciarJogo()
    {
        Time.timeScale = 1;
        Invoke(nameof(DelayJogo), 0.2f);
        Destroy(cannon.bala);
        AtualizaInformacao();
        painelPause.SetActive(false);
        if (painelBotoesCam.activeInHierarchy)
        {
            movCamera = false;
            painelBotoesCam.SetActive(false);
        }
    }

    IEnumerator TempoDeResposta()
    {
        // Aguarda 1 segundo e, em seguida, define podeAtirar como verdadeiro
        yield return new WaitForSeconds(0.1f);
        podeAtirar = !movCamera;
    }
}

//JOGO FOI INICIADO EM 27/04/2024
