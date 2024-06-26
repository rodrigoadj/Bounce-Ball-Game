using System.Collections;
using UnityEngine;


public class AnimacaoMenu : MonoBehaviour
{
    [SerializeField] private GameObject[] objMoveis;
    [SerializeField] private GameObject prefabObject, prefabObjAnimado;
    public float larguraTotal; // Largura da grade
    public int larguraDividida; //largura dividida da grade
    public float alturaTotal, alturaDividida; // Altura da grade e altura dividida
    private int multiplicador = 5, objetosEmCena;
    public bool boolTemporizada;

    void Awake()
    {
        alturaTotal = Camera.main.orthographicSize * 2.0f;
        alturaDividida = (alturaTotal / 2); //+1;
        larguraTotal = alturaTotal * Camera.main.aspect;
        larguraDividida = (int)(larguraTotal / 2) + 1;
    }

    void MoveObj()
    {
        int speedX = (int)(Input.acceleration.x * multiplicador);
        int speedY = (int)(Input.acceleration.y * multiplicador);
        if ((Input.acceleration.x > 0.2f || Input.acceleration.y > 0.2f) || (Input.acceleration.x < -0.2f || Input.acceleration.y < -0.2f))
        {
            for (int i = 0; i < objMoveis.Length; i++)
            {
                float moveX = i % 2 == 0 ? speedX : -speedX;
                float moveY = i % 2 == 0 ? speedY : -speedY;
                objMoveis[i].GetComponent<Rigidbody2D>().AddForce(new Vector2(moveX, moveY), ForceMode2D.Impulse);
            }
        }
    }

    public IEnumerator InstanciaObjeto(int _quant)
    {
        yield return new WaitForSeconds(2);
        if (objetosEmCena < _quant)
        {
            Instantiate(prefabObject, Vector2.zero, Quaternion.identity);
            objMoveis = GameObject.FindGameObjectsWithTag("CVIMG");
            objetosEmCena++;
        }
        MoveObj();
        yield return StartCoroutine(InstanciaObjeto(15)); ;

    }

    public IEnumerator FadeIn(string _nomeObjeto, string _nomeAnimacao, float _tempoEspera, int _objetoFilhoID)
    {
        prefabObjAnimado = GameObject.Find(_nomeObjeto).transform.GetChild(_objetoFilhoID).gameObject;
        prefabObjAnimado.SetActive(true);
        yield return new WaitForSeconds(_tempoEspera);
        boolTemporizada = true;
        prefabObjAnimado.GetComponent<Animator>().Play(_nomeAnimacao);
        yield return new WaitForSeconds(0.5f);
        boolTemporizada = false;
        prefabObjAnimado.SetActive(false);
    }
}
