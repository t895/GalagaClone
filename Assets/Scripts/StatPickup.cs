using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupType { health, melee };

public class StatPickup : MonoBehaviour
{
    [SerializeField] private PickupType pickupType;
    [SerializeField] private float amount = 0f;
    
    void OnTriggerEnter2D(Collider2D _collider)
    {
        Take(_collider.GetComponent<PlayerManager>());
    }

    void Take(PlayerManager _player)
    {
        if(pickupType == PickupType.health && _player.health < _player.maxHealth)
        {
            _player.Heal(amount);
            Destroy(gameObject);
        }
        else if(pickupType == PickupType.melee && _player.meleePower < _player.maxMeleePower)
        {
            _player.Recharge(amount);
            Destroy(gameObject);
        }
    }
}
