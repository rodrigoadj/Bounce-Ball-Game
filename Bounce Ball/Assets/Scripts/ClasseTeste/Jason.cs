using System;
using UnityEngine;


[Serializable]
public class Jason
{
    public float volumeGeral = 1f;
    public bool ligarPos_Processamento = true;
    public bool ligarFPS = false;

    [Range(0, 20)]
    public float intensidadeBloom = 1.25f;

    [Range(0, 10)]
    public float DifusaoBloom = 8.0f;


}

