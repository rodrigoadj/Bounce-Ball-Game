using UnityEngine.EventSystems;
using UnityEngine;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Transform t_Camera;
    private bool moveCam;
    [SerializeField]private int velocidadeCamera = 5;

    void Start()
    {
        t_Camera = GameObject.Find("Camera").transform;
    }

    void Update()
    {
        if(moveCam)
        {
            if(gameObject.name == "BTN_Dir")
            {
                t_Camera.transform.position += Time.deltaTime * velocidadeCamera * Vector3.right;
                print("Entrando");
            }
            
            if(gameObject.name == "BTN_Esq")
            {
                t_Camera.transform.position += Time.deltaTime * velocidadeCamera * Vector3.left;
                print("Entrando");
            }
        }
        
    }

    void IPointerDownHandler.OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
    {
        moveCam = true;
    }

    void IPointerUpHandler.OnPointerUp(UnityEngine.EventSystems.PointerEventData eventData)
    {
        moveCam = false;
    }

}
