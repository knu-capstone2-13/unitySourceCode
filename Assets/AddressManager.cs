using System.Net;
using System.Net.Sockets;
using TMPro;
using UnityEngine;

public class AddressManager : MonoBehaviour
{
    public GameObject addressObj;
    public GameObject portObj;
    private IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
    private IPAddress local;
    private int portNumber;

    void Awake()
    {
        foreach (IPAddress ip in host.AddressList)
            if (ip.AddressFamily == AddressFamily.InterNetwork)
                local = ip;
        portNumber = 7860;

        addressObj.GetComponent<TMP_InputField>().text = local.ToString() ;
        portObj.GetComponent<TMP_InputField>().text = portNumber.ToString();
    }

    public string GetLocalAddress()
    {
        return local.ToString();
    }

    public int GetPortNumber()
    {
        return portNumber;
    }

    public string GetFullAddress()
    {
        return addressObj.GetComponent<TMP_InputField>().text + ":" + portObj.GetComponent<TMP_InputField>().text;
    }
}
