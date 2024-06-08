using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

// bat파일이 있는 stable-diffusion-webui폴더는, 유니티 파일 폴더의 상위 폴더에 함께 들어있다고 가정한다.
// 사용한 포트 비우기는 보류
public class WebUIManager : MonoBehaviour
{
    public Button btn;
    public GameObject inputF;
    private string newPath;
    private string filePath;

    void Awake()
    {
        string currentPath = Directory.GetCurrentDirectory(); // 현재 경로
        DirectoryInfo parentPath = Directory.GetParent(currentPath); // 부모 경로
        newPath = parentPath.FullName + "\\" + "stable-diffusion-webui"; // 새 경로
        filePath = newPath + "\\" + "webui-user.bat"; // 파일 경로

        if (!Directory.Exists(newPath)) // webui 폴더가 없으면, webui 실행 버튼을 비활성화
        {
            btn.interactable = false;
            inputF.GetComponent<TMP_Text>().text = "There is No webUI folder";
        }
        else if (!File.Exists(filePath)) // .bat 파일이 없으면, webui 실행 버튼을 비활성화
        {
            btn.interactable = false;
            inputF.GetComponent<TMP_Text>().text = "There is No .bat file";
        }
        else
            inputF.GetComponent<TMP_Text>().text = "Can execute webUI";
    }

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
        btn.interactable = false;
    }
}