using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider slider;
    private PlayerManager player;
    private float pastHealth;

    void Start()
    {
        slider = GetComponent<Slider>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
    }

    void Update()
    {
        if(player.health != pastHealth)
            SetHealth(player.health);
        pastHealth = player.health;
    }

    public void SetHealth(float _health)
    {
        slider.value = _health / 100;
    }
}
