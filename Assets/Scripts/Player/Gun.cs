using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    public AudioClip shotSound;
    public float fireRate = 1f;
    public PowerupType powerupType;
    
    [SerializeField] private List<Transform> baseShotOrigins;
    [SerializeField] private List<Transform> octaShotOrigins;
    private float nextFire = 0f;
    private PlayerManager player;
    private PlayerController controller;
    private AudioSource audioPlayer;

    void Start()
    {
        player = GetComponent<PlayerManager>();
        controller = GetComponent<PlayerController>();
        audioPlayer = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0) && !GameState.paused)
        {
            if(powerupType == PowerupType.none)
                Shoot();
            else if (powerupType == PowerupType.bigBullets)
                BigShot();
            else if (powerupType == PowerupType.multiShot)
                MultiShot();
        }
    }

    private void Shoot()
    {
        if(player.isAlive && Time.time > nextFire)
        {
            audioPlayer.PlayOneShot(shotSound);
            nextFire = Time.time + fireRate;
            GameObject liveBullet = Instantiate(bullet, baseShotOrigins[0]);
        }
    }

    private void BigShot()
    {

    }

    private void MultiShot()
    {
        if(player.isAlive && Time.time > nextFire)
        {
            audioPlayer.PlayOneShot(shotSound);
            nextFire = Time.time + fireRate;
            for(int i = 0; i <= 2; i++) 
            {
                GameObject liveBullet = Instantiate(bullet, baseShotOrigins[i]);
            }
        }
    }

}
