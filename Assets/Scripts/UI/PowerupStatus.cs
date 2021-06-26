using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupStatus : MonoBehaviour
{
    [SerializeField] private List<PowerupDefinition> powerups;
    private Image powerupIcon;
    //private PlayerGun playerWeapon;
    private PowerupType pastPowerup;

    private void Start()
    {
        powerupIcon = GetComponent<Image>();
        //playerWeapon = GameObject.FindWithTag("Player").GetComponent<PlayerGun>();
        pastPowerup = PlayerVariables.playerGun.powerupType;

    }

    private void Update()
    {
        if(PlayerVariables.playerGun.powerupType != pastPowerup)
        {
            powerupIcon.sprite = FindIcon(PlayerVariables.playerGun.powerupType);
            pastPowerup = PlayerVariables.playerGun.powerupType;
        }
            
    }

    private Sprite FindIcon(PowerupType _powerupType)
    {
        for(int i = 0; i < powerups.Count; i++)
        {
            if(powerups[i].Powerup == _powerupType)
                return powerups[i].Sprite;
        }
        return null;
    }

    [System.Serializable]
    private class PowerupDefinition
    {
        [SerializeField] private PowerupType powerupType;
        [SerializeField] private Sprite powerupSprite;

        public PowerupType Powerup { get { return powerupType; } }
        public Sprite Sprite { get { return powerupSprite; } }
    }
}
