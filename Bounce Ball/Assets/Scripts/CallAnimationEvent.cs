using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CallAnimationEvent : MonoBehaviour
{
    [SerializeField] UnityEvent evento;
    public void CallEnventAnimation(int rola)
    {
        SoundManager.intanceSound.PlayCarregar(rola);
    }
}
