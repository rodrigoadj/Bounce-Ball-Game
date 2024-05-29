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

        if (coll.gameObject.CompareTag("Chegada"))
        {
            GameManager.venceu = true;
            SoundManager.intanceSound.PlayVitoria();
            Invoke(nameof(Delay), 3);

        }
    }


    void Delay()
    {
        gameManager.AbrirPainelVitoria();
        Destroy(gameObject);
    }
}
