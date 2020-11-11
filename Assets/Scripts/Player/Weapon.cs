using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bullet;
    public Transform shotOrigin;
    public float fireRate = 1f;
    public float meleeRate = 0f;
    private float nextFire = 0f;
    private float nextMelee = 0f;
    private PlayerManager player;
    private PlayerController controller;

    void Start()
    {
        player = GetComponent<PlayerManager>();
        controller = GetComponent<PlayerController>();
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0))
            Shoot();
    }

    public void Melee()
    {
        if(player.isAlive && Time.time > nextMelee)
        {
            nextMelee = Time.time + meleeRate;
            
        }
    }

    private void Shoot()
    {
        if(player.isAlive && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            GameObject liveBullet = Instantiate(bullet, shotOrigin);
        }
    }

}
