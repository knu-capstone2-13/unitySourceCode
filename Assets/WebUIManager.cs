using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

// WebUI를 실행하여 정보를 가져오는 매니저
// bat파일이 있는 stable-diffusion-webui폴더는, 유니티 파일 폴더의 상위 폴더에 함께 들어있다고 가정한다.
// 사용한 포트 비우기는 보류
public class WebUIManager : MonoBehaviour
{
    [SerializeField]
    private Button GenerateBtn;
    [SerializeField]
    private Button LoadBtn;
    [SerializeField]
    private GameObject inputF;
    [SerializeField]
    private GenerateImage generateImage;

    private string newPath = "";
    private string filePath = "";

    void Awake()
    {
        GenerateBtn.interactable = true;
        LoadBtn.interactable = false;

        string currentPath = Directory.GetCurrentDirectory(); // 현재 경로
        DirectoryInfo parentPath = Directory.GetParent(currentPath); // 부모 경로
        newPath = parentPath.FullName + "\\" + "stable-diffusion-webui"; // 새 경로
        filePath = newPath + "\\" + "webui-user.bat"; // 파일 경로

        if (!Directory.Exists(newPath)) 
        {
            GenerateBtn.interactable = false;
            inputF.GetComponent<TMP_Text>().text = "There is No webUI folder"; // webui 폴더가 없으면, webui 실행 버튼을 비활성화
        }
        else if (!File.Exists(filePath)) 
        {
            GenerateBtn.interactable = false;
            inputF.GetComponent<TMP_Text>().text = "There is No .bat file"; // .bat 파일이 없으면, webui 실행 버튼을 비활성화
        }
        else
            inputF.GetComponent<TMP_Text>().text = "Can execute webUI"; // 실행 가능
    }

    // WebUI bat파일 실행
    public void ExecuteWebUI()
    {
        Process process = new Process();
        ProcessStartInfo startInfo = new ProcessStartInfo();

        startInfo.FileName = "cmd";
        startInfo.Arguments = "/C webui-user.bat";
        startInfo.WorkingDirectory = newPath;
        startInfo.UseShellExecute = false;
        startInfo.CreateNoWindow = false;

        process.StartInfo = startInfo;
        process.Start();
        
        inputF.GetComponent<TMP_Text>().text = "Executing";
        GenerateBtn.interactable = false;
        LoadBtn.interactable = true;
    }

    // 모델과 로라 GET 통신 요청
    public void GetInputValues()
    {
        generateImage.LoadInputValues();
    }
}