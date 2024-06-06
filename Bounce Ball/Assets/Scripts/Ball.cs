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
        Collider2D ballcoll = GetComponent<CircleCollider2D>();
        somColisao.Play();

        if (coll.gameObject.CompareTag("Chegada"))
        {
            GameManager.venceu = true;
            SoundManager.intanceSound.PlayVitoria();
            ballcoll.enabled = false;
            gameManager.AbrirPainelVitoria();
            Destroy(gameObject, 0.3f);

        }
    }
}
