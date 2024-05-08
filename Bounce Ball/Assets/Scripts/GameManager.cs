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
    [SerializeField]private int faseAtual, quantidadeFase, contadorTentativa;
    [SerializeField]private GameObject painelBotoesCam, painelVitoria;
    [SerializeField]private Transform posCamera, posCanhao, posChegada;
    [SerializeField]private Transform[] transformCamera, transformCanhao, transformChegada;
    [SerializeField]private Vector2[] contadorRecord;
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
        painelVitoria.SetActive(false);
        comecarJogo= true;
        AtualizaInformacao();
        podeAtirar = true;
    }
    
    void Update()
    {
        if(comecarJogo)
        {
            MostraInformacao();
            SeguirBola();
            ReiniciaTentativa();
            AbrirPainelVitoria();
        }
    }
    
    void AbrirPainelVitoria()
    {
        if(venceu)
            painelVitoria.SetActive(true);
    }
    

    void MostraInformacao() // Implementar A UI
    {
        contadorRecord[faseAtual] = new Vector2(contadorQuicada,contadorTentativa);
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

    void ReiniciaTentativa()
    {
        if(!venceu)
        {
            if (cannon.bala == null && !movCamera)
            {
                btn_MovCam.interactable = true;
                posCamera.position = transformCamera[faseAtual].position;
                StartCoroutine(TempoDeResposta());
            }  
        }
    }

    public void ReiniciarFase()
    {
        venceu = false;
        cannon.bala = null;
        painelVitoria.SetActive(false);
    }

    void SeguirBola()
    {
        if(!venceu)
        {
            if (cannon.bala != null)
            {
                btn_MovCam.interactable = false;
                podeAtirar = false;
                posCamera.position = Vector2.MoveTowards(posCamera.position, cannon.bala.transform.position, 1f);
            }
        }   
    }

    public void BTN_PassarDeFase() //Botão passar de fase
    {
        if(venceu)
        {
            cannon.bala = null;
            faseAtual++;
            AtualizaInformacao();
            print(faseAtual);
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

    IEnumerator TempoDeResposta()
    {
        // Aguarda 1 segundo e, em seguida, define podeAtirar como verdadeiro
        yield return new WaitForSeconds(0.1f);
        podeAtirar = !movCamera;
    }
}

//JOGO FOI INICIADO EM 27/04/2024
