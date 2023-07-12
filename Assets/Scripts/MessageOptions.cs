using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageOptions : MonoBehaviour
{

    public string msg;
    public VoiceController VC;
    public FileManager FM;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ListenText()
    {
        VC.StartSpeacking(msg);
    }

    public void SaveToFile()
    {
        FM.AppendText(msg);
    }


}
