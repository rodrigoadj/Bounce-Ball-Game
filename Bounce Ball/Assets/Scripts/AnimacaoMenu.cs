using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;


public class AnimacaoMenu : MonoBehaviour
{
    [SerializeField]private GameObject[] imgOBJ;
    [SerializeField]private GameObject[] objMoveis;
    [SerializeField]private GameObject prefabObject;
    float larguraTotal; // Largura da grade
    int larguraDividida; //largura dividida da grade
    float alturaTotal, alturaDividida; // Altura da grade e altura dividida
    private int multiplicador = 5, objetosEmCena;
    private bool podeInstanciar;

    void Start()
    {
        alturaTotal = Camera.main.orthographicSize * 2.0f;
        alturaDividida = (alturaTotal/2); //+1;
        larguraTotal = alturaTotal * Camera.main.aspect;
        larguraDividida = (int)(larguraTotal/2)+1;
        GeraBarreira();
        Invoke(nameof(InstanciaObjeto),2);
    }


    void Update()
    {
        MoveObj(); 
    }


    void MoveObj()
    {
        int speedX = (int)(Input.acceleration.x * multiplicador);
        int speedY = (int)(Input.acceleration.y * multiplicador);
        print("Acelerometro X: " + Input.acceleration.x +"  "+ "Acelerometro Y: " + Input.acceleration.y);
        if(Input.acceleration.x != 0.0f || Input.acceleration.y != 0.0f)
        {
            for(int i=0; i<objMoveis.Length; i++)
            {
                float moveX = i%2==0? speedX:-speedX;
                float moveY = i%2==0? speedY:-speedY;
                objMoveis[i].transform.position += new Vector3(moveY * Time.deltaTime, moveX * Time.deltaTime,0);
            }
        }
    }

    void InstanciaObjeto(int _quant)
    {
        if(objetosEmCena < _quant)
        {
            Instantiate(prefabObject, Vector2.zero, Quaternion.identity);
            objMoveis = GameObject.FindGameObjectsWithTag("CVIMG");
        }
        Invoke(nameof(InstanciaObjeto),1);
    }

    void MultiplicadorVariado()
    {
        multiplicador = Random.Range(5,10);
        Invoke(nameof(MultiplicadorVariado),3);
    }

    void GeraBarreira()
    {
        imgOBJ[0].transform.localScale = new Vector2(transform.localScale.x,alturaTotal);
        imgOBJ[0].transform.position = new Vector2(-larguraDividida,0);
        imgOBJ[1].transform.localScale = new Vector2(transform.localScale.x,alturaTotal);
        imgOBJ[1].transform.position = new Vector2(larguraDividida,0);
        imgOBJ[2].transform.localScale = new Vector2(larguraTotal,transform.localScale.y);
        imgOBJ[2].transform.position = new Vector2(0,-alturaDividida);
        imgOBJ[3].transform.localScale = new Vector2(larguraTotal,transform.localScale.y);
        imgOBJ[3].transform.position = new Vector2(0,alturaDividida);
    }
}
