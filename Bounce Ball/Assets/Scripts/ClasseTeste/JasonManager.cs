using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JasonManager : MonoBehaviour
{
    string caminho = "Jason.txt";
    public static JasonManager instance;
    public Jason jason;
    public static bool fps_Ligar;
    [SerializeField] Toggle toggle_FPS;
    [SerializeField] GameObject obj_FPS;
    public static bool pos_ProcessamentoLigar;
    [SerializeField] Toggle toggle_PosProcessamento;
    [SerializeField] GameObject obj_PosProcessamento;
    [SerializeField] Slider slider_Audio;

    public static float volume;


    void Start()
    {
        caminho = Path.Combine(Application.persistentDataPath, caminho);
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
        fps_Ligar = jason.ligarFPS;
        toggle_FPS.isOn = jason.ligarFPS;
        obj_FPS.SetActive(jason.ligarFPS);

        pos_ProcessamentoLigar = jason.ligarPos_Processamento;
        toggle_PosProcessamento.isOn = jason.ligarPos_Processamento;
        obj_PosProcessamento.SetActive(jason.ligarPos_Processamento);

        volume = jason.volumeGeral;
        slider_Audio.value = jason.volumeGeral;
        SoundManager.intanceSound.audioReprodutorSource[0].volume = jason.volumeGeral;
        SoundManager.intanceSound.audioReprodutorSource[1].volume = jason.volumeGeral;


    }

    void SalvarConfig()
    {
        string conteudo = JsonUtility.ToJson(jason, true);
        File.WriteAllText(caminho, conteudo);
    }

    public void FPS_LigarSet()
    {
        jason.ligarFPS = toggle_FPS.isOn;
    }

    public void Pos_ProcessamentoSet()
    {
        jason.ligarPos_Processamento = toggle_PosProcessamento.isOn;
    }

    public void VolumeSet()
    {
        jason.volumeGeral = slider_Audio.value;
    }

    public void BTN_Aplicar()
    {
        SalvarConfig();
        AplicarConfig();
    }
}
