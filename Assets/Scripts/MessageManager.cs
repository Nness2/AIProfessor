using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour
{
    public GameObject AIMessagePrefab;
    public GameObject UserMessagePrefab;
    public RectTransform Canvas;
    public ScrollRect scrollRect;
    public GameObject message;
    public List<GameObject> msgList;
    public InputField textInput;
    public bool gate;
    public bool Scrollgate;
    public Slider slider;
    private float initialFrontSize;
    public TMP_Dropdown UnderstandDD;
    public VoiceController VC;
    public MessageOptions MO;
    public RectTransform ViewPort;
    public TextMeshProUGUI debug;
    public int test = 0;

    public TextMeshProUGUI TextPoliceVisu;
    // Start is called before the first frame update
    void Start()
    {
        msgList = new List<GameObject>();
        gate = true;
        Scrollgate = false;
        initialFrontSize = AIMessagePrefab.transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontSize;
        TextPoliceVisu.fontSize = initialFrontSize;
        //textInput.GetComponent<TMP_InputField>().pointSize = (initialFrontSize * slider.value * (1/Canvas.transform.localScale.x))/2;
    }
    private void Update()
    {
        debug.text = GetKeyboardSize().ToString();
        //Debug.Log(TouchScreenKeyboard.area.height);
        ViewPort.offsetMin = new Vector2(ViewPort.offsetMin.x, GetKeyboardSize());
        if (Scrollgate)
        {
            if (scrollRect.normalizedPosition != new Vector2(0, 0))
            {
                scrollRect.normalizedPosition = new Vector2(0, 0);
                Scrollgate = false;
            }
        }
        if (msgList.Count > 0)
        {
            if(msgList[msgList.Count - 1] != null)
            {
                int nbrLine = msgList[msgList.Count - 1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().textInfo.lineCount;
                if (nbrLine != 0)
                {
                    if(nbrLine != 0)
                    {
                        if (gate)
                        {
                            gate = false;
                            //multiplier la taille de la police, puis refaire le calcule 40 + (29 * (nbrLine - 1)) et multiplier par le meme nombre. Sur tout les messages

                            //nbrLine = msgList[msgList.Count - 1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().textInfo.lineCount;

                            msgList[msgList.Count - 1].GetComponent<RectTransform>().sizeDelta = new Vector2(Canvas.GetComponent<RectTransform>().position.x * 2, (70 + (58 * (nbrLine - 1))) * slider.value);
                            //msgList[msgList.Count - 1].GetComponent<RectTransform>().sizeDelta = new Vector2(Canvas.GetComponent<RectTransform>().position.x * 2, 40 + (29 * (nbrLine - 1))* multiply);
                            Scrollgate = true;

                            if (UnderstandDD.value == 2) // if vocal actif
                                msgList[msgList.Count - 1].transform.GetChild(0).gameObject.SetActive(false);
                            else
                                msgList[msgList.Count - 1].transform.GetChild(0).gameObject.SetActive(true);
                        }
                    }
                }
            }
        }
    }
    public int GetKeyboardSize()
    {
        using (AndroidJavaClass UnityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            AndroidJavaObject View = UnityClass.GetStatic<AndroidJavaObject>("currentActivity").Get<AndroidJavaObject>("mUnityPlayer").Call<AndroidJavaObject>("getView");

            using (AndroidJavaObject Rct = new AndroidJavaObject("android.graphics.Rect"))
            {
                View.Call("getWindowVisibleDisplayFrame", Rct);

                return Screen.height - Rct.Call<int>("height");
            }
        }
    }
    public void AddMessage(string msg, bool isUser)
    {
        gate = true;
        GameObject message;
        if (isUser)
            message = (GameObject)Instantiate(AIMessagePrefab);
        else
            message = (GameObject)Instantiate(UserMessagePrefab);
        message.transform.SetParent(transform);//Setting button parent
        message.GetComponent<RectTransform>().sizeDelta = new Vector2(Canvas.GetComponent<RectTransform>().position.x * 2, 1);
        message.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = msg;//Changing text
        message.transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontSize = initialFrontSize * slider.value;


        msgList.Add(message);
    }

    public void sliderChange()
    {
        float multiply = slider.value;

        foreach (GameObject msg in msgList)
        {
            msg.transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontSize = initialFrontSize * multiply;

            int nbrLine = msg.transform.GetChild(0).GetComponent<TextMeshProUGUI>().textInfo.lineCount;

            msg.GetComponent<RectTransform>().sizeDelta = new Vector2(Canvas.GetComponent<RectTransform>().position.x * 2, (70 + (58 * (nbrLine - 1)))*multiply);
            //textInput.GetComponent<TMP_InputField>().pointSize = (initialFrontSize * slider.value * (1 / Canvas.transform.localScale.x))/2;
        }
        TextPoliceVisu.fontSize = initialFrontSize * multiply;

    }

    public void HideText()
    {
        if (UnderstandDD.value == 2) // if vocal actif
        {
            foreach (GameObject msg in msgList)
            {
                msg.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
        else
        {
            foreach (GameObject msg in msgList)
            {
                msg.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }

}
