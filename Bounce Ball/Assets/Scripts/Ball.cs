using System.Collections;
using UnityEngine;
using TMPro;

public class Ball : MonoBehaviour
{
    AudioSource somColisao;
    GameManager gameManager;
    [SerializeField] GameObject bola;
    [SerializeField] TMP_Text txt_Quick;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        gameManager.contadorTentativa++;
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        GameManager.contadorQuicada++;
        txt_Quick.text = GameManager.contadorQuicada.ToString();
        StartCoroutine(mostrarContador());
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

    IEnumerator mostrarContador()
    {
        bola.SetActive(true);
        yield return new WaitForSeconds(1f);
        bola.SetActive(false);

    }
}
