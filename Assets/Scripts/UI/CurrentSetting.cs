using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrentSetting : MonoBehaviour
{
    public TextMeshProUGUI settingText;
    private int currentSettingIndex;
    private int pastSettingIndex;

    void Update()
    {
        currentSettingIndex = QualitySettings.GetQualityLevel();
        if(currentSettingIndex != pastSettingIndex)
            UpdateSettingsText(currentSettingIndex);
        pastSettingIndex = currentSettingIndex;
    }

    void UpdateSettingsText(int _index)
    {
        switch(_index)
        {
            case 0:
                settingText.text = "Current - Very Low";
                break;
            case 1:
                settingText.text = "Current - Low";
                break;
            case 2:
                settingText.text = "Current - Medium";
                break;
            case 3:
                settingText.text = "Current - High";
                break;
            case 4:
                settingText.text = "Current - Very High";
                break;
            case 5:
                settingText.text = "Current - Ultra";
                break;
        }
    }
}
