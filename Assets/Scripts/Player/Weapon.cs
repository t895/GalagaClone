using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bullet;
    public Transform shotOrigin;
    public float fireRate = 1f;
    private float nextFire = 0f;
    private PlayerManager player;

    void Start()
    {
        player = GetComponent<PlayerManager>();
    }

    void Update()
    {
        if(Input.GetButton("Fire1") && player.isAlive && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject liveBullet = Instantiate(bullet, shotOrigin);
    }
}
