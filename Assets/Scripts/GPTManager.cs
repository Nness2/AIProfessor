// more info https://unitycoder.com/blog/2022/02/05/using-open-ai-gpt-3-api-in-unity/
using OpenAI_API;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GPTManager : MonoBehaviour
{

    public MessageManager UIManager;
    public InputField textInput;
    public TMP_Dropdown UnderstandDD;

    public VoiceController VC;
    private void Start()
    {
        //var task = StartAsync();
        //var task = StartInitAsync("For the next messages you are an native english speaker and you help me to practice my english. For each message start by correcting my misstakes then respond to the message. You speak only in English.");
    }

    private void Update()
    {
    }
    public void SendRequest()
    {
        var task = StartAsync(textInput.text);

    }

    // Start is called before the first frame update
    async Task StartAsync(string msg)
    {

        //Debug.Log("running..");

        /*var keypath = Path.Combine(Application.streamingAssetsPath, "apikey.txt");
        if (File.Exists(keypath) == false)
        {
            Debug.LogError("Apikey missing: " + keypath);
        }*/

        //Debug.Log("Load apikey: " + keypath);
        var apikey = "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";// File.ReadAllText(keypath);
        if(msg == "")
        {
            return;
        }
        UIManager.AddMessage("You : " + msg, true);
        var answer = "";
        try
        {
            var api = new OpenAI_API.OpenAIAPI(apikey);
            //var result = await api.Completions.CreateCompletionAsync("One Two Three One Two", temperature: 0.1);
            //var result = await api.Completions.CreateCompletionAsync("Lorem ipsum dolor sit amet,", temperature: 0.1);
            //var result = await api.Completions.CreateCompletionAsync("o be, or not to be:", temperature: 0.1);
            //var result = await api.Completions.CreateCompletionAsync("if (Physics.Raycast(transform.position,", temperature: 0.1);
            //var result = await api.Completions.CreateCompletionAsync("Once upon a time ", temperature: 0.1);
            var result = await api.Completions.CreateCompletionAsync(
                model : "text-davinci-003",
                prompt: msg ,//+ "(Don't take this section in your response, respond first by correcting each misstake on my text, then take a natif speaker role and discuss with me to help me to improve my english)",
                temperature: 0.6, 
                max_tokens: 150, 
                top_p: 1, 
                frequencyPenalty: 1, 
                presencePenalty: 1);

            //var result = await api.Completions.CreateCompletionAsync("3.14159265359", temperature: 0.1);
            //var result = await api.Search.GetBestMatchAsync("Washington DC", "Canada", "China", "USA", "Spain");
            //var result = await api.Search.GetBestMatchAsync("RaycastHit", "Unity3D", "Godot", "Unreal Engine", "GameMaker");
            //Console.WriteLine(result.ToString());
            //Debug.Log(result.ToString());
            answer = result.ToString();
        }

        catch (System.Exception e)
        {
            Debug.LogError(e.Message);
        }

        string responce = answer.Substring(2);
        UIManager.AddMessage("Fellow : " + responce, false);
        if (UnderstandDD.value == 1) // if vocal actif
        {
            VC.StartSpeacking(responce);
        }
    }


    async Task StartInitAsync(string msg)
    {

        var apikey = "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";// File.ReadAllText(keypath);

        try
        {
            var api = new OpenAI_API.OpenAIAPI(apikey);
            //var result = await api.Completions.CreateCompletionAsync("One Two Three One Two", temperature: 0.1);
            //var result = await api.Completions.CreateCompletionAsync("Lorem ipsum dolor sit amet,", temperature: 0.1);
            //var result = await api.Completions.CreateCompletionAsync("o be, or not to be:", temperature: 0.1);
            //var result = await api.Completions.CreateCompletionAsync("if (Physics.Raycast(transform.position,", temperature: 0.1);
            //var result = await api.Completions.CreateCompletionAsync("Once upon a time ", temperature: 0.1);
            var result = await api.Completions.CreateCompletionAsync(
                model: "text-davinci-002",
                prompt: msg,//+ "(Don't take this section in your response, respond first by correcting each misstake on my text, then take a natif speaker role and discuss with me to help me to improve my english)",
                temperature: 1,
                max_tokens: 500,
                top_p: 1,
                frequencyPenalty: 0,
                presencePenalty: 0);

            //var result = await api.Completions.CreateCompletionAsync("3.14159265359", temperature: 0.1);
            //var result = await api.Search.GetBestMatchAsync("Washington DC", "Canada", "China", "USA", "Spain");
            //var result = await api.Search.GetBestMatchAsync("RaycastHit", "Unity3D", "Godot", "Unreal Engine", "GameMaker");
            //Console.WriteLine(result.ToString());
            Debug.Log(result.ToString());

        }

        catch (System.Exception e)
        {
            Debug.LogError(e.Message);
        }

    }

    public void clearEditText()
    {
        textInput.text = ""; 
    }


}