using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

// WebUI�� �����Ͽ� ������ �������� �Ŵ���
// bat������ �ִ� stable-diffusion-webui������, ����Ƽ ���� ������ ���� ������ �Բ� ����ִٰ� �����Ѵ�.
// ����� ��Ʈ ����� ����
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

        string currentPath = Directory.GetCurrentDirectory(); // ���� ���
        DirectoryInfo parentPath = Directory.GetParent(currentPath); // �θ� ���
        newPath = parentPath.FullName + "\\" + "stable-diffusion-webui"; // �� ���
        filePath = newPath + "\\" + "webui-user.bat"; // ���� ���

        if (!Directory.Exists(newPath)) 
        {
            GenerateBtn.interactable = false;
            inputF.GetComponent<TMP_Text>().text = "There is No webUI folder"; // webui ������ ������, webui ���� ��ư�� ��Ȱ��ȭ
        }
        else if (!File.Exists(filePath)) 
        {
            GenerateBtn.interactable = false;
            inputF.GetComponent<TMP_Text>().text = "There is No .bat file"; // .bat ������ ������, webui ���� ��ư�� ��Ȱ��ȭ
        }
        else
            inputF.GetComponent<TMP_Text>().text = "Can execute webUI"; // ���� ����
    }

    // WebUI bat���� ����
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

    // �𵨰� �ζ� GET ��� ��û
    public void GetInputValues()
    {
        generateImage.LoadInputValues();
    }
}