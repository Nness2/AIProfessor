using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageButton : MonoBehaviour
{
    public MessageOptions MO;
    public TextMeshProUGUI selfMsg;
    void Start()
    {
        MO = GameObject.FindGameObjectWithTag("MsgManager").GetComponent<MessageManager>().MO;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OpenOptionCanvas()
    {
        MO.transform.gameObject.SetActive(true);
        string[] text = selfMsg.text.Split(':');
        int rep = text.Length;

        string newtext = "";
        for (int i = 1; i<rep; i++)
        {
            newtext += " " + text[i];
        }
        MO.msg = newtext;

    }

}
