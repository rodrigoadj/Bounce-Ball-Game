using UnityEngine;

public class Ball : MonoBehaviour
{
    AudioSource somColisao;
    
    private void OnCollisionEnter2D(Collision2D coll) 
    {
        GameManager.contadorQuicada++;
        somColisao = GetComponent<AudioSource>();
        somColisao.Play();

        if(coll.gameObject.CompareTag("Chegada"))
        {
            GameManager.venceu = true;
            print("Chegou!!");
            Destroy(gameObject, 0.3f);
        }
    }
}
