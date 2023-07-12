using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.UI;
public class FileManager : MonoBehaviour
{

    public InputField uiText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void WriteString(string text)
    {
        //string path = "Assets/Resources/notes.txt";
        string path = Application.persistentDataPath + "/notes.txt";
        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, false);
        writer.WriteLine(text);
        writer.Close();
        StreamReader reader = new StreamReader(path);
        //Print the text from the file
        //Debug.Log(reader.ReadToEnd());
        reader.Close();
    }
    public static string ReadString()
    {
        //string path = "Assets/Resources/notes.txt";
        string path = Application.persistentDataPath + "/notes.txt";
        //Read the text from directly from the test.txt file
        StreamReader writer = new StreamReader(path);
        //StreamReader reader = new StreamReader(path);
        string text = writer.ReadToEnd();
        //Debug.Log(text);
        writer.Close();
        return text;
    }


    public void AppendText(string msg)
    {
        string concat = ReadString();
        concat = concat + "\n\n" + msg;
        WriteString(concat);
    }

    public void SetText(string msg)
    {
        WriteString(msg);
    }

    public void GetText()
    {
        string path = "Assets/Resources/notes.txt";
        if (!File.Exists(path))
        {
            WriteString("");
        }

        uiText.text = ReadString();
    }
}
