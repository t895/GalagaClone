using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    public Transform shotOrigin;
    public float fireRate = 1f;
    
    private float nextFire = 0f;
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

    private void Shoot()
    {
        if(player.isAlive && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            GameObject liveBullet = Instantiate(bullet, shotOrigin);
        }
    }

}
