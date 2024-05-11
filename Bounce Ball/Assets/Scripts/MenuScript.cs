using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;
using TMPro;

public class MenuScript : MonoBehaviour
{
    [SerializeField]private TMP_Text txt_InfoBuild;

    public GameObject painel_Fases;

    PlayerPrefs faseID;
    
    
    void Start()
    {
        InformacaoJogo();
    }

    void Update()
    {
    
    }

    public void ComecaJogo()
    {
        //SceneManager.LoadScene("Game");
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
}
