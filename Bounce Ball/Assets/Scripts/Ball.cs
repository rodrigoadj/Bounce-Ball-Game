using UnityEngine;

public class Ball : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D coll) 
    {
        GameManager.contadorQuicada++;

        if(coll.gameObject.CompareTag("Chegada"))
        {
            GameManager.venceu = true;
            print("Chegou!!");
        }
    }
}
