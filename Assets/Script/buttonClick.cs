using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class buttonClick : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void onClicked() 
    {
        StartCoroutine(httpConnect());
    }

    IEnumerator httpConnect()
    {
        string url = @"https://http.cat/100";
        UnityWebRequest www = UnityWebRequest.Get(url);


        yield return www.SendWebRequest();

        if(www.error == null) {
            Debug.Log(www.downloadHandler.text);
            
        } else {
            Debug.Log("error");
        }
    }
}
