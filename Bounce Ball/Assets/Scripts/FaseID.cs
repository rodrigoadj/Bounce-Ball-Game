using UnityEngine;
using TMPro;

public class FaseID : MonoBehaviour
{

    public static int faseID, idFaseConcluida, countTentativas;

    public static int[] bitEstrelas = new int[10];

    public TMP_Text txt_fps;

    void Update()
    {
        txt_fps.text = (1 / Time.deltaTime).ToString("f2");
    }
}
