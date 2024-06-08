using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using TMPro;

[InitializeOnLoad]
public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        /*string currentPath = Directory.GetCurrentDirectory(); // 현재 경로
        DirectoryInfo parentPath = Directory.GetParent(currentPath); // 부모 경로
        string newPath = parentPath.FullName + "\\" + "stable-diffusion-webui"; // 새 경로

        if (!Directory.Exists(newPath))
            obj.GetComponent<TMP_InputField>().text = "Please input stable-diffusion-webui folder address"; // 폴더가 없으면, 입력란에 경로가 없음을 기입
        else
            obj.GetComponent<TMP_InputField>().text = newPath; // 폴더가 있으면, 입력란에 경로를 기입*/
    }
}
