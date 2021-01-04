using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    public float fireRate = 1f;
    public PowerupType powerupType;
    [HideInInspector] public AudioSource audioPlayer;
    
    //[SerializeField] private AudioClip shotSound;
    [SerializeField] private List<Transform> baseShotOrigins;
    [SerializeField] private List<Transform> octaShotOrigins;
    private float nextFire = 0f;
    private PlayerManager player;
    private PlayerController controller;
    
    private GameObject customBullet;
    private float customRate = 0;

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
            if(customRate == 0)
            {
                if(player.isAlive && Time.time > nextFire)
                {
                    nextFire = Time.time + fireRate;
                    PickShot();
                }  
            }
            else
            {
                if(player.isAlive && Time.time > nextFire)
                {
                    nextFire = Time.time + customRate;
                    PickShot();
                }
            }
        }
    }

    void PickShot()
    {
        if(powerupType == PowerupType.none)
            Shoot();
        else if (powerupType == PowerupType.bigBullets)
            BigShot();
        else if (powerupType == PowerupType.tripleShot)
            TripleShot();
        else if (powerupType == PowerupType.octaShot)
            OctaShot();
    }

    #region PowerupTaken
    public void PowerupTaken(PowerupType _powerupType, int _powerupDuration)
    {
        StopAllCoroutines();
        PowerupTaken(_powerupType, _powerupDuration, null);
    }

    public void PowerupTaken(PowerupType _powerupType, int _powerupDuration, GameObject _customBullet)
    {
        StopAllCoroutines();
        PowerupTaken(_powerupType, _powerupDuration, _customBullet, 0);
        
    }

    public void PowerupTaken(PowerupType _powerupType, int _powerupDuration, GameObject _customBullet, float _customRate)
    {
        StopAllCoroutines();
        StartCoroutine(PowerupTimeout(_powerupDuration));
        powerupType = _powerupType;
        if(_customBullet != null)
            customBullet = _customBullet;
        if(_customRate != 0)
            customRate = _customRate;
    }
    #endregion

    private IEnumerator PowerupTimeout(float _time)
    {
        yield return new WaitForSeconds(_time);
        powerupType = PowerupType.none;
        customBullet = null;
        customRate = 0;
    }

    private void Shoot()
    {
        //audioPlayer.PlayOneShot(shotSound);
        GameObject liveBullet = Instantiate(bullet, baseShotOrigins[0]);
        liveBullet.GetComponent<Bullet>().Initialize(this);
    }

    private void BigShot()
    {
        //audioPlayer.PlayOneShot(shotSound);
        GameObject liveBullet = Instantiate(customBullet, baseShotOrigins[0]);
        liveBullet.GetComponent<Bullet>().Initialize(this);
    }

    private void TripleShot()
    {
        //audioPlayer.PlayOneShot(shotSound);
        for(int i = 0; i <= 2; i++) 
        {
            GameObject liveBullet = Instantiate(bullet, baseShotOrigins[i]);
            liveBullet.GetComponent<Bullet>().Initialize(this);
        }
    }

    private void OctaShot()
    {
        //audioPlayer.PlayOneShot(shotSound);
        for(int i = 0; i < octaShotOrigins.Count; i++) 
        {
            GameObject liveBullet = Instantiate(bullet, octaShotOrigins[i]);
            liveBullet.GetComponent<Bullet>().Initialize(this);
        }
    }
}
