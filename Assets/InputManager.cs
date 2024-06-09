using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// 프롬프트, 스텝, 스케일, 모델, 로라를 관리하는 매니저
public class InputManager : MonoBehaviour
{
    [SerializeField]
    private GameObject promptObj;
    [SerializeField]
    private GameObject stepObj;
    [SerializeField]
    private GameObject scaleObj;
    [SerializeField]
    private TMP_Dropdown modelDropdown;
    [SerializeField]
    private TMP_Dropdown loraDropdown;
    [SerializeField]
    private Toggle loraToggle;

    private JSONModelData[] models;
    private JSONLoraData[] loras;

    // GET한 모델을 드롭다운으로 변환
    public void LoadModels(JSONModelData[] jsonData)
    {
        models = jsonData;

        modelDropdown.ClearOptions();
        List<TMP_Dropdown.OptionData> optionList = new List<TMP_Dropdown.OptionData>();

        foreach (JSONModelData model in models)
        {
            optionList.Add(new TMP_Dropdown.OptionData(model.model_name));
        }

        modelDropdown.AddOptions(optionList);

        modelDropdown.value = 0;
    }

    // GET된 로라를 드롭다운으로 변환
    public void LoadLoras(JSONLoraData[] jsonData)
    {
        loras = jsonData;
        loraToggle.isOn = true;

        loraDropdown.ClearOptions();
        List<TMP_Dropdown.OptionData> optionList = new List<TMP_Dropdown.OptionData>();

        foreach (JSONLoraData lora in loras)
        {
            optionList.Add(new TMP_Dropdown.OptionData(lora.name));
        }

        loraDropdown.AddOptions(optionList);

        loraDropdown.value = 0;
    }

    public Boolean isLoraUsing()
    {
        if (loraToggle.isOn == true)
            return true;
        else
            return false;
    }

    public string GetPrompt()
    {
        return promptObj.GetComponent<TMP_InputField>().text;
    }

    public string GetStep()
    {
        return stepObj.GetComponent<TMP_InputField>().text;
    }
    public string GetScale()
    {
        return scaleObj.GetComponent<TMP_InputField>().text;
    }

    public string GetModel()
    {
        int index = modelDropdown.value;
        return modelDropdown.GetComponent<TMP_Dropdown>().options[index].text;
    }

    public string GetLora()
    {
        int index = loraDropdown.value;
        return loraDropdown.GetComponent<TMP_Dropdown>().options[index].text;
    }
}