using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Linq;


[Serializable]
public class JSONData{
    public string title;
    public string model_name;
    public string filename;
    public string config;
}

[Serializable]
public class ImageData{
    public string[] images;
}

public class imageChange : MonoBehaviour
{
    public GameObject obj;
    public RawImage rawImage;

    public void onClicked() 
    {
        StartCoroutine(PostConnect());
    }

    IEnumerator PostConnect() {
        UnityWebRequest www = new UnityWebRequest();
        string prompt = "{\"prompt\" : \"rabbit\", \"steps\": 20}";
        byte[] payload = System.Text.Encoding.UTF8.GetBytes(prompt);

        UploadHandlerRaw uploader = new UploadHandlerRaw(payload);
        www.url = "http://192.168.0.10:7864/sdapi/v1/txt2img"; //site
        www.method= UnityWebRequest.kHttpVerbPOST;
        uploader.contentType = "application/json";
        
        www.uploadHandler = uploader;

        www.downloadHandler = new DownloadHandlerBuffer();

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success) {
            Debug.Log(www.error);
            Debug.Log(www.downloadHandler.text);
        } else {
            Debug.Log(www.downloadHandler.text);
            ImageData image = JsonConvert.DeserializeObject<ImageData>(www.downloadHandler.text);
            Debug.Log(image.images);
            Byte[] imageByte = Convert.FromBase64String(image.images[0]);
            Texture2D tex = new Texture2D(512, 512, TextureFormat.DXT5, false);
            tex.LoadImage(imageByte);
            rawImage.texture = tex;
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
            rawImage.texture = myTexture;
        } else {
            Debug.Log("error");
        }
    }

    IEnumerator httpConnectTest()
    {
        string url = "http://192.168.0.10:7864/sdapi/v1/sd-models";
        UnityWebRequest www = UnityWebRequest.Get(url);


        yield return www.SendWebRequest();

        if(www.result == UnityWebRequest.Result.Success) {
            Debug.Log(www.downloadHandler.text);
            JSONData[] jsonData = JsonConvert.DeserializeObject<JSONData[]>(www.downloadHandler.text);

            Debug.Log(jsonData[0]);
            Debug.Log(jsonData[0].ToString());
            Debug.Log(jsonData[0].title);
            Debug.Log(jsonData[0].model_name);
            //Debug.Log(jsonData[0].title);
        } else {
            Debug.Log("error");
        }
    }
}