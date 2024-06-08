using TMPro;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public GameObject promptObj;
    public GameObject stepObj;
    public GameObject modelObj;

    private JSONModelData[] models;
    private JSONLoraData[] loras;

    [SerializeField]
    private TextMeshProUGUI TMPObJ;
    [SerializeField]
    private TMP_Dropdown dropdown;

    public void LoadModels(JSONModelData[] jsonData)
    {
        models = jsonData;
    }

    public void LoadLoras(JSONLoraData[] jsonData)
    {
        loras = jsonData;
    }

    public string GetPrompt()
    {
        return promptObj.GetComponent<TMP_InputField>().text;
    }

    public string GetStep()
    {
        return stepObj.GetComponent<TMP_InputField>().text;
    }

    public string GetModel()
    {
        return promptObj.GetComponent<TMP_InputField>().text;
    }

   

    //public void OnDropdownEvent(int index)
    //{
    //    TMPObJ.text = $"Dropdown Value : {index}";
    //}

    //private void Awake()
    //{
    //    dropdown.ClearOptions();
    //    List<TMP_Dropdown.OptionData> optionList = new List<TMP_Dropdown.OptionData>();

    //    // arrayClass �迭�� �ִ� ��� ���ڿ� �����͸� �ҷ��ͼ� optionList�� ����
    //    foreach (string str in arrayClass)
    //    {
    //        optionList.Add(new TMP_Dropdown.OptionData(str));
    //    }

    //    // ������ ������ optionList�� dropdown�� �ɼ� ���� �߰�
    //    dropdown.AddOptions(optionList);

    //    // ���� dropdown�� ���õ� �ɼ��� 0������ ����
    //    dropdown.value = 0;
    //}

}
