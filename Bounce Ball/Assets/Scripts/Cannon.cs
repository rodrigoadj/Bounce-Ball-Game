using Unity.Mathematics;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject prefab_Bala, cano, bala;
    public GameObject barraPotencia;
    private Vector2 tamanhoInicialBarra = new Vector2(0, 0.1f);
    public int recarregar;
    float time;
    GameManager gameManager;
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        barraPotencia.transform.localScale = tamanhoInicialBarra;
    }

    void Update()
    {
        if (gameManager.comecarJogo)
            if (Input.touchCount > 0)
            {
                Touch toque = Input.GetTouch(0);
                Vector2 posToque = Camera.main.ScreenToWorldPoint(toque.position);
                Vector2 direcao = new Vector2(posToque.x - transform.position.x, posToque.y - transform.position.y);

                if (toque.phase == TouchPhase.Began)
                {
                    recarregar++;
                    if (recarregar > 2)
                        recarregar = 0;
                }

                if (!gameManager.movCamera)
                    transform.right = direcao;

                if (gameManager.podeAtirar && !gameManager.movCamera)
                {
                    Potenciometro(direcao);

                    if (toque.phase == TouchPhase.Ended)
                    {
                        bala = Instantiate(prefab_Bala, cano.transform.position, quaternion.identity);
                        Rigidbody2D rb_Bala = bala.GetComponent<Rigidbody2D>();
                        rb_Bala.AddForce(direcao * 100, ForceMode2D.Force);
                        barraPotencia.transform.localScale = tamanhoInicialBarra;
                        Destroy(bala, 5);
                    }
                }
            }

        DelayToque();
    }

    void Potenciometro(Vector2 _direcao)
    {
        if (_direcao.x <= 0)
            barraPotencia.transform.localScale = new Vector2(0, 0.1f);
        else if (_direcao.x > 0)
            barraPotencia.transform.localScale = new Vector2(_direcao.x * 0.1f, 0.1f);
    }

    void DelayToque()
    {
        if (recarregar > 0)
        {
            time += Time.deltaTime * 1;
            if (time > 0.5f)
            {
                recarregar = 0;
                time = 0;
            }
        }
    }
}
