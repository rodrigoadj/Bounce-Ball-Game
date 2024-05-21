using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class MenuScript : MonoBehaviour
{
    [SerializeField]private TMP_Text txt_InfoBuild, txt_popUp;

    public GameObject painel_Fases;
    private int fasesConcluidas, chave;

    [SerializeField] private Button[] btn_Fases;

    [SerializeField] Button removeDados;
    

    void Start()
    {
        removeDados.onClick.AddListener(() => StartCoroutine(RemoveDados()));
        painel_Fases.SetActive(false);
        InformacaoJogo();
        ArmazenaFases();
        ArmazenaInformacoes();
        LiberaFases();
    }

    void ArmazenaFases()
    {
        btn_Fases = new Button[painel_Fases.transform.childCount];
        for(int _index=0; _index < painel_Fases.transform.childCount; _index++)
        {
            btn_Fases[_index] = painel_Fases.transform.GetChild(_index).GetComponent<Button>();
            if(_index > 0)
            {
                if(btn_Fases[_index].gameObject.name == "BTN_Voltar")
                    return;
                else
                    btn_Fases[_index].interactable = false;
            }  
        }
    }
    
    void ArmazenaInformacoes()
    {
        PlayerPrefs.SetInt("liberar", FaseID.idFaseConcluida);
        fasesConcluidas = PlayerPrefs.GetInt("liberar");
    }

    void LiberaFases()
    {
        for(int i=0; i <= fasesConcluidas; i++)
        {
            btn_Fases[i].interactable = true; 
            print("FaseConcluidas " + fasesConcluidas);
        }
    }

    public void ComecaJogo()
    {
        painel_Fases.SetActive(true);
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
    public void IndexFaseButton(int ID=0)
    {
        FaseID.faseID = ID;
        SceneManager.LoadScene("Game");
    }

    IEnumerator RemoveDados()
    {
        chave++;
        print(chave);
        switch(chave)
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
                yield return new WaitForSeconds(1);
                txt_popUp.text = "";
            break;
        }
    }
}
