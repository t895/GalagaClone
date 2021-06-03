using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeleeBar : MonoBehaviour
{
    private Slider slider;
    private PlayerManager player;
    private float pastMelee;

    void Start()
    {
        slider = GetComponent<Slider>();
        player = PlayerVariables.player;
    }

    void Update()
    {
        if(player.meleePower != pastMelee)
            SetMelee(player.meleePower);
        pastMelee = player.meleePower;
    }

    public void SetMelee(float _melee)
    {
        slider.value = _melee / 100;
    }
}
