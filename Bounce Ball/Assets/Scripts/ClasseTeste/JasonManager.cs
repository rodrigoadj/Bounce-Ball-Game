using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JasonManager : MonoBehaviour
{
    public static JasonManager instance;
    readonly string caminho = "Assets/Jason.txt";
    public Jason jason;
    public static bool fps_Ligar;
    [SerializeField] Toggle toggle_FPS;
    public static bool pos_Processamento;
    [SerializeField] GameObject obj_FPS;

    void Start()
    {
        instance = this;
        CarregarConfig();
        AplicarConfig();
    }
    void CarregarConfig()
    {
        if (!File.Exists(caminho))
        {
            jason = new Jason();
            SalvarConfig();
        }
        else
        {
            string conteudo = File.ReadAllText(caminho);
            jason = JsonUtility.FromJson<Jason>(conteudo);
        }
    }

    void AplicarConfig()
    {
        pos_Processamento = jason.pos_Processamento;
        fps_Ligar = jason.ligarFPS;
        toggle_FPS.isOn = jason.ligarFPS;

        obj_FPS.SetActive(jason.ligarFPS);
    }

    void SalvarConfig()
    {
        string conteudo = JsonUtility.ToJson(jason, true);
        File.WriteAllText(caminho, conteudo);
    }

    public void FPS_LigarSet()
    {
        jason.ligarFPS = toggle_FPS.isOn;
        SalvarConfig();
        AplicarConfig();
    }
}
