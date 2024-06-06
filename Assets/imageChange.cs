using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class imageChange : MonoBehaviour
{
    public GameObject obj;
    RawImage rawIamge;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(httpConnect());
        rawIamge = GetComponent<RawImage>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClicked() 
    {
        //StartCoroutine(PostConnect());
        //StartCoroutine(httpConnect());
        StartCoroutine(httpConnectTest());
        //rawIamge = GetComponent<RawImage>();
    }

    public void changeField () {
        
    }
    
    IEnumerator PostConnect() {
        UnityWebRequest www = new UnityWebRequest();
        string prompt = "{\"prompt\" : \"rabbit\", \"steps\": 5}";
        byte[] payload = System.Text.Encoding.Default.GetBytes(prompt);
        
        UploadHandler uploader = new UploadHandlerRaw(payload);
        www.url = "127.0.0.1:7860/sdapi/v1/sd-models"; //site
        www.method= UnityWebRequest.kHttpVerbPOST;
        uploader.contentType = "Content-Type: application/json";
        
        www.uploadHandler = uploader;

        www.downloadHandler = new DownloadHandlerBuffer();

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success) {
            Debug.Log(www.error);
        } else {
            Debug.Log(www.downloadHandler.text);
            Debug.Log("앨랠래");
        }
    }

   
    IEnumerator httpConnect()
    {
        string url = @"https://http.cat/" + obj.GetComponent<TMP_InputField>().text;
        
        //Debug.Log(obj.GetComponent<InputField>()).text;
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);


        yield return www.SendWebRequest();

        if(www.result == UnityWebRequest.Result.Success) {
            Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            rawIamge.texture = myTexture;
        } else {
            Debug.Log("error");
        }
    }

    IEnumerator httpConnectTest()
    {
        string url ="http://192.168.25.10:7860/sdapi/v1/sd-models";
        UnityWebRequest www = UnityWebRequest.Get(url);


        yield return www.SendWebRequest();

        if(www.result == UnityWebRequest.Result.Success) {
           Debug.Log(www.downloadHandler.text);
        } else {
            Debug.Log("error");
        }
    }
}
