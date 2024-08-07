using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject obj_FPS, postProcessLayer;
    public static int contadorQuicada;
    public static bool venceu;
    public bool comecarJogo;
    public bool movCamera, podeAtirar;
    public int faseAtual, quantidadeFase, contadorTentativa;
    [SerializeField] public GameObject painelBotoesCam, painelProximaFase, painelPause, painelVitoria;
    [SerializeField] private Transform posCamera, posCanhao, posChegada;
    [SerializeField] private Transform[] transformCamera, transformCanhao, transformChegada;
    [SerializeField] private Vector2[] contadorRecord;
    [SerializeField] private GameObject estrela;
    private Button btn_MovCam;
    private Cannon cannon;
    public TMP_Text txt_Tentativas;


    void Awake()
    {
        btn_MovCam = GameObject.Find("BTN_MoveCamera").GetComponent<Button>();
        cannon = GameObject.Find("Canhao").GetComponent<Cannon>();
        posCamera = GameObject.Find("Camera").transform;
    }

    void Start()
    {
        ConfiguracaoGet();
        faseAtual = FaseID.faseID;
        painelVitoria.SetActive(false);
        painelProximaFase.SetActive(false);
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
        }
    }

    void MostrarEstrelas()
    {
        if (venceu)
        {
            if (contadorTentativa == 1)
                estrela.SetActive(true);
            else
                estrela.SetActive(false);
        }
    }

    void ConfiguracaoGet()
    {
        obj_FPS.SetActive(JasonManager.fps_Ligar);
        postProcessLayer.SetActive(JasonManager.pos_ProcessamentoLigar);
        SoundManager.intanceSound.audioReprodutorSource[0].volume = JasonManager.volume;
        SoundManager.intanceSound.audioReprodutorSource[1].volume = JasonManager.volume;
    }

    void MostrarInformacao() // Implementar A UI
    {
        contadorRecord[faseAtual] = new Vector2(contadorQuicada, contadorTentativa);
        txt_Tentativas.text = contadorTentativa.ToString();
    }

    void AtualizaInformacao()
    {
        if (faseAtual != 10)
        {
            posCamera.position = transformCamera[faseAtual].position;
            posCanhao.position = transformCanhao[faseAtual].position;
            posChegada.position = transformChegada[faseAtual].position;
            painelProximaFase.SetActive(false);
            contadorQuicada = 0;
            contadorTentativa = 0;
            FaseID.bitEstrelas[faseAtual] = 0;
        }
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

            if (cannon.bala != null && !movCamera && cannon.recarregar == 2)
            {
                btn_MovCam.interactable = true;
                posCamera.position = transformCamera[faseAtual].position;
                Destroy(cannon.bala);
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
                //posCamera.position = Vector2.MoveTowards(posCamera.position, cannon.bala.transform.position, 1f);
                posCamera.position = Vector2.Lerp(posCamera.position, cannon.bala.transform.position, 5f * Time.deltaTime);
            }
        }
    }

    void DelayJogo()
    {
        comecarJogo = true;
    }

    void SalvaEstrelas()
    {
        if (contadorRecord[faseAtual].y == 1)
            FaseID.instance.SalvarEstrelas(faseAtual, 1);
        else
            FaseID.instance.SalvarEstrelas(faseAtual, 0);
    }

    void GanhouJogo()
    {
        if (faseAtual == 10)
        {
            painelVitoria.SetActive(true);
            comecarJogo = false;            //TRAVA TUDO, SERVE APENAR PARA FINALIZAR O GAME <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        }
    }

    [ContextMenu("Hack")]
    void PassouFase()
    {
        Debug.Log("Entrou");
        if (faseAtual >= PlayerPrefs.GetInt("liberar", -1))
        {
            Debug.Log("Salvou");
            PlayerPrefs.SetInt("liberar", faseAtual);
        }
    }

    public void AbrirPainelVitoria()
    {
        if (venceu)
        {
            Debug.Log("Venceu" + "Fase Atual" + faseAtual + "GameManager");
            MostrarEstrelas();
            PassouFase();
            btn_MovCam.interactable = false;
            painelProximaFase.SetActive(true);
        }
    }

    public void BTN_ReiniciarFase()
    {
        venceu = false;
        AtualizaInformacao();
        painelProximaFase.SetActive(false);
        estrela.SetActive(false);
    }

    public void BTN_PassarDeFase() //Botão passar de fase
    {
        if (venceu)
        {
            SalvaEstrelas();
            faseAtual++;
            GanhouJogo();
            cannon.bala = null;
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
        btn_MovCam.interactable = false;
        painelPause.SetActive(true);
        Time.timeScale = 0;
    }

    public void BTN_Continue()
    {
        Time.timeScale = 1;
        btn_MovCam.interactable = true;
        Invoke(nameof(DelayJogo), 0.2f);
        painelPause.SetActive(false);
    }

    public void BTN_VoltarMenu()
    {
        Time.timeScale = 1;
        venceu = false;
        painelPause.SetActive(false);
        SceneManager.LoadScene("Menu");
    }

    public void BTN_ReiniciarJogo()
    {
        Time.timeScale = 1;
        btn_MovCam.interactable = false;
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
