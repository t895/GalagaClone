using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupStatus : MonoBehaviour
{
    private Image powerupIcon;
    private PowerupType pastPowerup;

    private void Start()
    {
        powerupIcon = GetComponent<Image>();
        pastPowerup = PlayerVariables.playerGun.defaultBullet.powerupType;
    }

    private void Update()
    {
        if(PlayerVariables.playerGun.currentBullet.powerupType != pastPowerup)
        {
            powerupIcon.sprite = PlayerVariables.playerGun.currentBullet.icon;
            pastPowerup = PlayerVariables.playerGun.currentBullet.powerupType;
        }
    }
}
