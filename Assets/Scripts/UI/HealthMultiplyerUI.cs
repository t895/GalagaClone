using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthMultiplyerUI : MonoBehaviour
{
    private TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if(PlayerVariables.playerMultiplyer > 0)
            text.text = PlayerVariables.playerMultiplyer.ToString("F2") + "x";
        else
            text.text = "";
    }
}
