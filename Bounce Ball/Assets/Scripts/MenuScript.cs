using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

public class MenuScript : MonoBehaviour
{
    [SerializeField]private TMP_Text txt_InfoBuild;

    public GameObject painel_Fases;
    private int fasePref = 0;

    [SerializeField] private Button[] btn_Fases;

    [SerializeField] Button removepref;
    

    void Start()
    {
        removepref.onClick.AddListener(() => RemoveKeys());
        painel_Fases.SetActive(false);
        InformacaoJogo();
        ArmazenaFases();
        LiberaFases();
    }

    void ArmazenaFases()
    {
        
        btn_Fases = new Button[painel_Fases.transform.childCount];
        for(int _index=0; _index < painel_Fases.transform.childCount; _index++)
        {
            btn_Fases[_index] = painel_Fases.transform.GetChild(_index).GetComponent<Button>();
            btn_Fases[_index].interactable = false;
        }
    }
    

    void LiberaFases()
    {
        fasePref = PlayerPrefs.GetInt("liberar");
        for(int i=0; i <= fasePref; i++)
            btn_Fases[i].interactable = true;

    }

    public void ComecaJogo()
    {
        painel_Fases.SetActive(true);
    }

    public void Créditos()
    {
        
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

    void RemoveKeys()
    {
        PlayerPrefs.DeleteAll();
    }
}
