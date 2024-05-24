using UnityEngine;

public class Ball : MonoBehaviour
{
    AudioSource somColisao;
    GameManager gameManager;
    
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        gameManager.contadorTentativa++;
    }

    private void OnCollisionEnter2D(Collision2D coll) 
    {
        GameManager.contadorQuicada++;
        somColisao = GetComponent<AudioSource>();
        somColisao.Play();

        if(coll.gameObject.CompareTag("Chegada"))
        {
            GameManager.venceu = true;
            gameManager.AbrirPainelVitoria();
            //print("Chegou!!");
            Destroy(gameObject, 0.3f);
        }
    }
}
