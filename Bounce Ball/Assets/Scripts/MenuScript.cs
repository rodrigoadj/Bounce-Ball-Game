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
    private int fasesConcluidas, chave, estrelaGanha;
    [SerializeField] Button removeDados;
    [SerializeField] private AnimacaoMenu animMenu;
    public GameObject[] filhos;

    [SerializeField] GameObject obj_creditos;

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
        FaseID.instance.CarregarEstrelas();
        MostraEstrela();
    }

    void Update()
    {
        if (animMenu.boolTemporizada)
            painel_Fases.SetActive(true);

        if (obj_creditos.activeInHierarchy)
        {
            if (Input.GetMouseButton(0))
            {
                if (obj_creditos.GetComponent<RectTransform>().anchoredPosition.y > 2500)
                {
                    Vector2 posToque = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    obj_creditos.transform.position += new Vector3(0, -10, 0);
                }
                else
                {
                    Vector2 posToque = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    obj_creditos.transform.position += new Vector3(0, posToque.y * Time.deltaTime * 2, 0);
                }

                if (obj_creditos.GetComponent<RectTransform>().anchoredPosition.y < -500)
                {
                    Vector2 posToque = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    obj_creditos.transform.position += new Vector3(0, +3, 0);
                }
                else
                {
                    Vector2 posToque = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    obj_creditos.transform.position += new Vector3(0, posToque.y * Time.deltaTime * 2, 0);
                }


            }
        }
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
        for (int i = 0; i <= fasesConcluidas + 1; i++)
        {
            if (i < btn_Fases.Length)
                btn_Fases[i].interactable = true;
        }
    }

    void MostraEstrela()
    {
        for (int i = 0; i < FaseID.bitEstrelas.Length; i++)
        {
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
        Slow Trap by Anton_Vlasov: https://pixabay.com/pt/music/batidas-slow-trap-18565/
        Midnight Forest by Syouki Takahashi: https://pixabay.com/pt/music/ambiente-midnight-forest-184304/
        Otimista Movement by SoulProdMusic: https://pixabay.com/pt/music/otimista-movement-200697/
        Ambient Metal Whoosh 2 by Floraphonic: https://pixabay.com/pt/sound-effects/ambient-metal-whoosh-2-174462/
        Life In A Droplet by drebddronefish: https://pixabay.com/pt/sound-effects/life-in-a-droplet-128269/
        Selection Sounds by Pixabay: https://pixabay.com/pt/sound-effects/selection-sounds-73225/
        Blaster 2 by Pixabay: https://pixabay.com/pt/sound-effects/blaster-2-81267/
        Eletronic Hit by Pixabay: https://pixabay.com/pt/sound-effects/electronic-hit-98242/
        Futuristic Beam by Pixabay: https://pixabay.com/pt/sound-effects/futuristic-beam-81215/
        Eletronic Glitter by Pixabay: https://pixabay.com/pt/sound-effects/electronic-glitter-68293/
        Interface 1 by UNIVERSFIELD: https://pixabay.com/pt/sound-effects/interface-1-126517/
        Ghosts On Film by UNIVERSFIELD: https://pixabay.com/pt/sound-effects/ghosts-on-film-185898/
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
                txt_popUp.text = "Do you really want to delete everything?";
                break;

            case 2:
                txt_popUp.text = "Last chance to go back. Are you sure?";
                break;

            case 3:
                txt_popUp.text = "Successfully deleted records";
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
