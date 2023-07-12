using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TextSpeech;
using UnityEngine.Android;
using TMPro;

public class VoiceController : MonoBehaviour
{
    const string LANG_CODE = "en-US";

    [SerializeField]
    InputField uiText;

    private void Start()
    {
        Setup(LANG_CODE);
#if UNITY_ANDROID
        SpeechToText.Instance.onPartialResultsCallback = OnPartialSpeechResult;
#endif
        SpeechToText.Instance.onResultCallback = OnFinalSpeechResult;
        TextToSpeech.Instance.onStartCallBack = OnSpeakStart;
        TextToSpeech.Instance.onDoneCallback   = OnSpeakStop;
        CheckPermission();
    }

    void CheckPermission()
    {
#if UNITY_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            Permission.RequestUserPermission(Permission.Microphone);
        }
#endif
    }
    #region Text to Speech
    public void StartSpeacking(string message)
    {
        TextToSpeech.Instance.StartSpeak(message);
    }    
    
    public void StopSpeaking()
    {
        TextToSpeech.Instance.StopSpeak();
    }

    public void OnSpeakStart()
    {
        Debug.Log("Talking started");
    }

    public void OnSpeakStop()
    {
        Debug.Log("Talking stop");
    }

    #endregion

    #region Speech to Text

    public void StartListening()
    {
        SpeechToText.Instance.StartRecording();
    }    
    
    public void StopListening()
    {
        SpeechToText.Instance.StopRecording();
    }

    void OnFinalSpeechResult(string result)
    {
        uiText.text = result;
    }

    void OnPartialSpeechResult(string result)
    {
        uiText.text = result;
    }
    #endregion




    void Setup(string code)
    {
        TextToSpeech.Instance.Setting(code, 1, 1);
        SpeechToText.Instance.Setting(code);
    }
}
