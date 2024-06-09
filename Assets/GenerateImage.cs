using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Newtonsoft.Json;

[Serializable]
public class JSONModelData
{
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

[Serializable]
public class ProgressData
{
    public float progress;
}

public class GenerateImage : MonoBehaviour
{
    [SerializeField]
    private RawImage rawImage;
    [SerializeField]
    private Image progressBar;
    [SerializeField]
    private AddressManager addressManager;
    [SerializeField]
    private InputManager inputManager;
    private UnityWebRequest imageWWW = null;

    public void Start()
    {
        progressBar.fillAmount = 0.0f;
    }

    // Load 버튼으로 모델/로라 GET 요청
    public void LoadInputValues()
    {
        StartCoroutine(httpModelConnect());
        StartCoroutine(httpLoraConnect());
    }

    // Generate 버튼으로 모델 세팅/이미지 생성 POST 요청
    public void onClicked()
    {
        StartCoroutine(PostModelSetting());
        StartCoroutine(PostImageGenerate());
        StartCoroutine(CheckProgress());
    }
    // 모델 로드
    IEnumerator httpModelConnect()
    {
        string url = "http://" + addressManager.GetFullAddress() + "/sdapi/v1/sd-models";
        UnityWebRequest www = UnityWebRequest.Get(url);

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("ModelConnectSuccess");
            JSONModelData[] jsonData = JsonConvert.DeserializeObject<JSONModelData[]>(www.downloadHandler.text);
            inputManager.LoadModels(jsonData);
        }
        else
            Debug.Log("Model Error  :  " + www.error);
    }

    // 로라 로드
    IEnumerator httpLoraConnect()
    {
        string url = "http://" + addressManager.GetFullAddress() + "/sdapi/v1/loras";
        UnityWebRequest www = UnityWebRequest.Get(url);

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("LoraConnectSuccess");
            JSONLoraData[] jsonData = JsonConvert.DeserializeObject<JSONLoraData[]>(www.downloadHandler.text);
            inputManager.LoadLoras(jsonData);
        }
        else
            Debug.Log("Lora Error  :  " + www.error);
    }

    // 모델 세팅
    IEnumerator PostModelSetting()
    {
        UnityWebRequest www = new UnityWebRequest();

        string prompt = "{\"sd_model_checkpoint\" : \"" + inputManager.GetModel() + "\"}";
        byte[] payload = System.Text.Encoding.UTF8.GetBytes(prompt);
        Debug.Log(prompt);
        www.url = "http://" + addressManager.GetFullAddress() + "/sdapi/v1/options";
        www.method = UnityWebRequest.kHttpVerbPOST;
        UploadHandlerRaw uploader = new UploadHandlerRaw(payload);
        uploader.contentType = "application/json";
        www.uploadHandler = uploader;

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
            Debug.Log("ModelSettingSuccess");
        else
            Debug.Log("Setting Error  :  " + www.error);
    }

    // 이미지 생성
    IEnumerator PostImageGenerate() 
    {
        imageWWW = new UnityWebRequest();

        string prompt;
        if (inputManager.isLoraUsing())
            prompt = "{\"prompt\" : \"" + inputManager.GetPrompt() + "<lora:" + inputManager.GetLora() + ":1>" + "\", \"steps\" : " + inputManager.GetStep() + ", \"cfg_scale\" : " + inputManager.GetScale() + ", \"sampler_index\" : \"Euler a\"}";
        else
            prompt = "{\"prompt\" : \"" + inputManager.GetPrompt() + "\", \"steps\" : " + inputManager.GetStep() + ", \"cfg_scale\" : " + inputManager.GetScale() + ", \"sampler_index\" : \"Euler a\"}";

        Debug.Log(prompt);
        byte[] payload = System.Text.Encoding.UTF8.GetBytes(prompt);

        imageWWW.url = "http://" + addressManager.GetFullAddress() + "/sdapi/v1/txt2img";
        imageWWW.method = UnityWebRequest.kHttpVerbPOST;
        UploadHandlerRaw uploader = new UploadHandlerRaw(payload);
        uploader.contentType = "application/json";
        imageWWW.uploadHandler = uploader;
        imageWWW.downloadHandler = new DownloadHandlerBuffer();

        yield return imageWWW.SendWebRequest();

        if (imageWWW.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("ImageGeneratingSuccess");
            ImageData image = JsonConvert.DeserializeObject<ImageData>(imageWWW.downloadHandler.text);
            Byte[] imageByte = Convert.FromBase64String(image.images[0]);
            Texture2D tex = new Texture2D(1024, 1024, TextureFormat.DXT5, false);
            tex.LoadImage(imageByte);
            rawImage.texture = tex;
        } 
        else 
            Debug.Log("Image Error  :  " + imageWWW.error);
    }

    // 진행바 생성
    IEnumerator CheckProgress()
    {
        string url = "http://" + addressManager.GetFullAddress() + "/sdapi/v1/progress";
        UnityWebRequest www;

        while(true)
        {
            www = UnityWebRequest.Get(url);
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
                if (imageWWW.result == UnityWebRequest.Result.Success)
                {
                    progressBar.fillAmount = 0.0f;
                    yield break;
                }
                else
                    progressBar.fillAmount = JsonConvert.DeserializeObject<ProgressData>(www.downloadHandler.text).progress;
            else
                Debug.Log("Progress Error  :  " + www.error);
        }
    }
}