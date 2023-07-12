using UnityEngine;
using UnityEngine.Events;

public class ClickHandler : MonoBehaviour
{
    //public UnityEvent upEvent;
    //public UnityEvent downEvent;
    VoiceController VC;

    public void ButtonPush()
    {
        Debug.Log("Down");
        //VC.StopSpeaking();
        //downEvent?.Invoke();
    }

    public void ButtonRelease()
    {
        Debug.Log("Up");
        //upEvent?.Invoke();
    }
}
