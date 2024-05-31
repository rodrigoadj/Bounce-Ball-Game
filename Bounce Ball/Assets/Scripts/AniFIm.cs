using UnityEngine;
using UnityEngine.SceneManagement;

public class AniFIm : MonoBehaviour
{
    public void Iniciarmenu()   //SCRIPT USADO EM >>>>>>>>>> PN_Vitoria/TXTcredito.
    {
        Invoke(nameof(menu), 1.5f);
    }


    public void menu()
    {
        SceneManager.LoadScene("Menu");
    }

}
