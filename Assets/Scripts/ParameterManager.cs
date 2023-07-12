using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ParameterManager : MonoBehaviour
{
    public GameObject paracanvas;
    public GameObject inputField;
    public GameObject VoiceRecord;
    public GameObject NotesCanvas;
    public TMP_Dropdown ExpretionDD;
    public TMP_Dropdown UnderstandDD;
    public FileManager FM;

    // Start is called before the first frame update

    public void ExpretionManager()
    {
        //Debug.Log(ExpretionDD.value);
        if(ExpretionDD.value == 0)
        {
            VoiceRecord.transform.gameObject.SetActive(false);
        }
        else if (ExpretionDD.value == 1)
        {
            VoiceRecord.transform.gameObject.SetActive(true);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableDisableCanvas()
    {
        if(paracanvas.activeSelf)
        {
            paracanvas.SetActive(false);
        }
        else
        {
            paracanvas.SetActive(true);

        }
    }

    public void OpenNotes(bool open)
    {
        if (open)
        {
            FM.GetText();
            NotesCanvas.SetActive(true);
        }
        else
        {
            FM.SetText(FM.uiText.text);
            NotesCanvas.SetActive(false);
        }
    }

}
