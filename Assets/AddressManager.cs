using System.Net;
using System.Net.Sockets;
using TMPro;
using UnityEngine;

// ����IP �ּҿ� Port��ȣ�� �����ϴ� �Ŵ���
public class AddressManager : MonoBehaviour
{
    [SerializeField]
    private GameObject addressObj;
    [SerializeField]
    private GameObject portObj;

    private IPHostEntry host = null;
    private IPAddress local = null;
    private int portNumber = 0;

    void Awake()
    {
        host = Dns.GetHostEntry(Dns.GetHostName());
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
