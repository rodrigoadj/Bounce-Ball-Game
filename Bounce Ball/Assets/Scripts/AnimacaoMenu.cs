using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AnimacaoMenu : MonoBehaviour
{
    GameObject[] imgOBJ;
    Image[] image;

    [SerializeField]private float larguraTotal; // Largura da grade
    [SerializeField]private int larguraDividida; //largura dividida da grade
    [SerializeField]private float alturaTotal, alturaDividida; // Altura da grade e altura dividida
    [SerializeField]private float cellSize = 1; // Tamanho da célula
    [SerializeField]private int totalDeCelulas;

    void Start()
    {
        imgOBJ = GameObject.FindGameObjectsWithTag("CVIMG");

        alturaTotal = Camera.main.orthographicSize * 2.0f;
        alturaDividida = (alturaTotal/2)+1;
        larguraTotal = alturaTotal * Camera.main.aspect;
        larguraDividida = (int)(larguraTotal/2)+1;
        //Camera.main.orthographicSize = 6.0f;
    }


    void Update()
    {
        //print("Acelerometro X: " + Input.acceleration.x +"  "+ "Acelerometro Y: " + Input.acceleration.y);
        print(Screen.safeArea.x + " " + Screen.safeArea.y);
        if(Input.acceleration.x > 0.0f)
        {
            
            for(int i=0; i< imgOBJ.Length; i++)
            {
                image = new Image[imgOBJ.Length];
                image[i] = imgOBJ[i].GetComponent<Image>();
                image[i].rectTransform.localScale = new Vector2(Input.acceleration.x * 0.1f * Time.deltaTime,Input.acceleration.y * Time.deltaTime);
            }
        }
    }

    void GeraBarreira()
    {
        
    }
}
