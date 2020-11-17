using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeleeBar : MonoBehaviour
{
    private Slider slider;
    private Melee melee;
    private float pastMelee;

    void Start()
    {
        slider = GetComponent<Slider>();
        melee = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Melee>();
    }

    void Update()
    {
        if(melee.meleePower != pastMelee)
            SetMelee(melee.meleePower);
        pastMelee = melee.meleePower;
    }

    public void SetMelee(float _melee)
    {
        slider.value = _melee / 100;
    }
}
