using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private GameObject[] imgOBJ, prefab_AreaEstrelas, estrelas;
    [SerializeField] private Button[] btn_Fases;
    [SerializeField] private TMP_Text txt_InfoBuild, txt_popUp;
    public GameObject painel_Fases;
    private int fasesConcluidas, chave;
    [SerializeField] Button removeDados;
    [SerializeField] private AnimacaoMenu animMenu;

    public GameObject[] filhos;




    void Start()
    {
        GeraBarreira();
        StartCoroutine(animMenu.InstanciaObjeto(15));
        removeDados.onClick.AddListener(() => StartCoroutine(RemoveDados()));
        painel_Fases.SetActive(false);
        InformacaoJogo();
        ArmazenaFases();
        ArmazenaInformacoes();
        LiberaFases();
        MostraEstrela();
    }

    void Update()
    {
        if (animMenu.boolTemporizada)
            painel_Fases.SetActive(true);
    }

    void ArmazenaFases()
    {
        btn_Fases = new Button[painel_Fases.transform.childCount - 1];
        estrelas = new GameObject[btn_Fases.Length];
        filhos = new GameObject[estrelas.Length];
        for (int _index = 0; _index < painel_Fases.transform.childCount - 1; _index++)
        {
            btn_Fases[_index] = painel_Fases.transform.GetChild(_index).GetComponent<Button>();
            estrelas[_index] = prefab_AreaEstrelas[_index].transform.GetChild(0).gameObject;
            filhos[_index] = estrelas[_index].transform.GetChild(0).gameObject;
            btn_Fases[_index].interactable = false;
        }
        btn_Fases[0].interactable = true;
    }

    void ArmazenaInformacoes()
    {
        fasesConcluidas = PlayerPrefs.GetInt("liberar");
    }

    void LiberaFases()
    {
        for (int i = 0; i <= fasesConcluidas; i++)
        {
            btn_Fases[i].interactable = true;
        }
    }

    void MostraEstrela()
    {
        for (int i = 0; i < FaseID.bitEstrelas.Length; i++)
        {
            print("Estrela Ganha Menu: " + FaseID.bitEstrelas[i]);
            if (FaseID.bitEstrelas[i] == 1)
                filhos[i].SetActive(true);
            else
                filhos[i].SetActive(false);
        }
    }

    void GeraBarreira()
    {
        imgOBJ[0].transform.localScale = new Vector2(transform.localScale.x, animMenu.alturaTotal);
        imgOBJ[0].transform.position = new Vector2(-animMenu.larguraDividida, 0);
        imgOBJ[1].transform.localScale = new Vector2(transform.localScale.x, animMenu.alturaTotal);
        imgOBJ[1].transform.position = new Vector2(animMenu.larguraDividida, 0);
        imgOBJ[2].transform.localScale = new Vector2(animMenu.larguraTotal, transform.localScale.y);
        imgOBJ[2].transform.position = new Vector2(0, -animMenu.alturaDividida);
        imgOBJ[3].transform.localScale = new Vector2(animMenu.larguraTotal, transform.localScale.y);
        imgOBJ[3].transform.position = new Vector2(0, animMenu.alturaDividida);
    }

    public void ComecaJogo()
    {
        StartCoroutine(animMenu.FadeIn("TransicaoAnimacao", "FadeInOut", 0.5f, 0));
    }

    public void Créditos() //créditos SFX
    {
        /*
        https://pixabay.com/pt/sound-effects/hitech-logo-165392/
        https://pixabay.com/pt/sound-effects/life-in-a-droplet-128269/
        https://pixabay.com/pt/sound-effects/dripping-water-nature-sounds-8050/
        https://pixabay.com/pt/sound-effects/selection-sounds-73225/
        https://pixabay.com/pt/sound-effects/electronic-hit-98242/
        https://pixabay.com/pt/sound-effects/interface-1-126517/
        https://pixabay.com/pt/sound-effects/paft-drunk-drums-efx-1574-206584/
        https://pixabay.com/pt/sound-effects/riser-and-hit-194451/
        https://pixabay.com/pt/sound-effects/electronic-glitter-68293/
        https://pixabay.com/pt/sound-effects/echo-chime-chime-89653/
        https://pixabay.com/pt/sound-effects/short-echo-fart-185251/
        https://pixabay.com/pt/sound-effects/paft-drunk-drums-efx-1574-206584/
        */

    }

    public void InformacaoJogo()
    {
        txt_InfoBuild.text = Application.companyName + ", " + "Versão " + Application.version;
        print(txt_InfoBuild.text);
    }

    public void Sair()
    {
        Application.Quit();
    }
    public void IndexFaseButton(int ID = 0)
    {
        FaseID.faseID = ID;
        SceneManager.LoadScene("Game");
    }

    public IEnumerator RemoveDados()
    {
        chave++;
        print(chave);
        switch (chave)
        {
            case 1:
                txt_popUp.text = "Você deseja mesmo apagar tudo?";
                break;

            case 2:
                txt_popUp.text = "Ultima chance para voltar atrás. Você tem certeza?";
                break;

            case 3:
                txt_popUp.text = "Registros apagados com sucesso";
                PlayerPrefs.DeleteAll();
                fasesConcluidas = 0;
                chave = 0;
                for (int i = 0; i < FaseID.bitEstrelas.Length; i++)
                {
                    FaseID.bitEstrelas[i] = 0;
                }
                MostraEstrela();
                ArmazenaFases();
                SoundManager.intanceSound.PlayResetDados();
                yield return new WaitForSeconds(1);
                txt_popUp.text = "";
                break;
        }
    }

    public void ReiniciaBotao()
    {
        chave = 0;
        txt_popUp.text = "";
    }
}
