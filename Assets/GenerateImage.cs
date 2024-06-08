using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Newtonsoft.Json;
using TMPro;

[Serializable]
public class JSONModelData{
    public string title;
    public string model_name;
    public string filename;
}

[Serializable]
public class JSONLoraData
{
    public string name;
    public string alias;
    public string path;
}

[Serializable]
public class ImageData{
    public string[] images;
}

public class GenerateImage : MonoBehaviour
{
    public RawImage rawImage;
    public AddressManager addressManager;
    public InputManager inputManager;

    public void Start()
    {
        //StartCoroutine(httpModelTest());
        //StartCoroutine(httpLoraTest());
    }

    public void onClicked()
    {
        StartCoroutine(PostConnect());
    }

    //IEnumerator httpConnectTest()
    //{
    //    string url = "http://" + addressManager.GetFullAddress() + "/user";
    //    UnityWebRequest www = UnityWebRequest.Get(url);

    //    yield return www.SendWebRequest();

    //    if (www.result == UnityWebRequest.Result.Success)
    //    {

    //    }
    //    else
    //        Debug.Log("error");
    //}

    // 모델 로드
    IEnumerator httpModelTest()
    {
        string url = "http://" + addressManager.GetFullAddress() + "/sdapi/v1/sd-models";
        UnityWebRequest www = UnityWebRequest.Get(url);

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            JSONModelData[] jsonData = JsonConvert.DeserializeObject<JSONModelData[]>(www.downloadHandler.text);
            inputManager.LoadModels(jsonData);
        }
        else
            Debug.Log("Model Error  :  " + www.error);
    }

    // 로라 로드
    IEnumerator httpLoraTest()
    {
        string url = "http://" + addressManager.GetFullAddress() + "/sdapi/v1/loras";
        UnityWebRequest www = UnityWebRequest.Get(url);

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            JSONLoraData[] jsonData = JsonConvert.DeserializeObject<JSONLoraData[]>(www.downloadHandler.text);
            inputManager.LoadLoras(jsonData);
        }
        else
            Debug.Log("Lora Error  :  " + www.error);
    }

    IEnumerator PostConnect() {
        UnityWebRequest www = new UnityWebRequest();
        string prompt = "{\"prompt\" : \"" + inputManager.GetPrompt() + "\", \"steps\" : " + inputManager.GetStep() + "}";

        byte[] payload = System.Text.Encoding.UTF8.GetBytes(prompt);

        www.url = "http://" + addressManager.GetFullAddress() + "/sdapi/v1/txt2img";
        www.method = UnityWebRequest.kHttpVerbPOST;
        UploadHandlerRaw uploader = new UploadHandlerRaw(payload);
        uploader.contentType = "application/json";
        www.uploadHandler = uploader;
        www.downloadHandler = new DownloadHandlerBuffer();

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            ImageData image = JsonConvert.DeserializeObject<ImageData>(www.downloadHandler.text);
            Byte[] imageByte = Convert.FromBase64String(image.images[0]);
            Texture2D tex = new Texture2D(1024, 1024, TextureFormat.DXT5, false);
            tex.LoadImage(imageByte);
            rawImage.texture = tex;
        } 
        else 
            Debug.Log("Image Error  :  " + www.error);
    }
}