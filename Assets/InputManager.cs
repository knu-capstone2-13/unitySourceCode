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

    //    // arrayClass 배열에 있는 모든 문자열 데이터를 불러와서 optionList에 저장
    //    foreach (string str in arrayClass)
    //    {
    //        optionList.Add(new TMP_Dropdown.OptionData(str));
    //    }

    //    // 위에서 생성한 optionList를 dropdown의 옵션 값에 추가
    //    dropdown.AddOptions(optionList);

    //    // 현재 dropdown에 선택된 옵션을 0번으로 설정
    //    dropdown.value = 0;
    //}

}
