using UnityEngine;
using TMPro;

public class FaseID : MonoBehaviour
{
    public static FaseID instance;

    public static int faseID, idFaseConcluida, countTentativas;

    public static int[] bitEstrelas = new int[10];

    public string[] chavesPrefs = new string[10];

    public TMP_Text txt_fps;

    void Start()
    {
        instance = GetComponent<FaseID>();
    }

    void Update()
    {
        txt_fps.text = (1 / Time.deltaTime).ToString("f2");
    }

    public void SalvarEstrelas(int _faseAtual, int _valor)
    {
        if (_valor > PlayerPrefs.GetInt(chavesPrefs[_faseAtual], -1))
            PlayerPrefs.SetInt(chavesPrefs[_faseAtual], _valor);
    }

    public void CarregarEstrelas()
    {
        for (int i = 0; i < bitEstrelas.Length; i++)
        {
            bitEstrelas[i] = PlayerPrefs.GetInt(chavesPrefs[i]);
        }
    }

}
