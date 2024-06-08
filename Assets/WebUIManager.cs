using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

// bat������ �ִ� stable-diffusion-webui������, ����Ƽ ���� ������ ���� ������ �Բ� ����ִٰ� �����Ѵ�.
// ����� ��Ʈ ����� ����
public class WebUIManager : MonoBehaviour
{
    public Button btn;
    public GameObject inputF;
    private string newPath;
    private string filePath;

    void Awake()
    {
        string currentPath = Directory.GetCurrentDirectory(); // ���� ���
        DirectoryInfo parentPath = Directory.GetParent(currentPath); // �θ� ���
        newPath = parentPath.FullName + "\\" + "stable-diffusion-webui"; // �� ���
        filePath = newPath + "\\" + "webui-user.bat"; // ���� ���

        if (!Directory.Exists(newPath)) // webui ������ ������, webui ���� ��ư�� ��Ȱ��ȭ
        {
            btn.interactable = false;
            inputF.GetComponent<TMP_Text>().text = "There is No webUI folder";
        }
        else if (!File.Exists(filePath)) // .bat ������ ������, webui ���� ��ư�� ��Ȱ��ȭ
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