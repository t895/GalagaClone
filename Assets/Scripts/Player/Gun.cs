using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject defaultBullet;
    public float fireRate = 1f;
    public PowerupType powerupType;
    [SerializeField] private AudioClip defaultShotSound;
    [SerializeField] private List<PowerupAudio> powerupAudio;
    [HideInInspector] public AudioSource audioPlayer;
    
    [SerializeField] private List<Transform> baseShotOrigins;
    [SerializeField] private List<Transform> octaShotOrigins;
    private float nextFire = 0f;
    private PlayerManager player;
    private PlayerController controller;
    
    private GameObject currentBullet;
    private float customRate = 0;
    private AudioClip currentSound;

    void Start()
    {
        player = GetComponent<PlayerManager>();
        controller = GetComponent<PlayerController>();
        audioPlayer = GetComponent<AudioSource>();
        currentSound = defaultShotSound;
        currentBullet = defaultBullet;
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

    private void PickShot()
    {
        if(powerupType == PowerupType.none)
            Shoot();
        else if (powerupType == PowerupType.bigBullets)
            BigShot();
        else if (powerupType == PowerupType.tripleShot)
            TripleShot();
        else if (powerupType == PowerupType.octaShot)
            OctaShot();

        audioPlayer.PlayOneShot(currentSound);
    }

    private AudioClip FindAudio(PowerupType _powerupType)
    {
        for(int i = 0; i < powerupAudio.Count; i++)
        {
            if(powerupAudio[i].Powerup == _powerupType)
                return powerupAudio[i].Audio;
        }
        return defaultShotSound;
    }

    private IEnumerator PowerupTimeout(float _time)
    {
        yield return new WaitForSeconds(_time);
        powerupType = PowerupType.none;
        currentBullet = defaultBullet;
        customRate = 0;
        currentSound = defaultShotSound;
    }

    public void PowerupTaken(PowerupType _powerupType, int _powerupDuration, GameObject _customBullet, float _customRate)
    {
        StopAllCoroutines();
        StartCoroutine(PowerupTimeout(_powerupDuration));
        powerupType = _powerupType;
        if(_customBullet != null)
            currentBullet = _customBullet;
        if(_customRate != 0)
            customRate = _customRate;
        if(FindAudio(_powerupType) != null)
            currentSound = FindAudio(_powerupType);
    }

    #region ShotTypes
    private void Shoot()
    {
        GameObject liveBullet = Instantiate(currentBullet, baseShotOrigins[0]);
    }

    private void BigShot()
    {
        GameObject liveBullet = Instantiate(currentBullet, baseShotOrigins[0]);
    }

    private void TripleShot()
    {
        for(int i = 0; i <= 2; i++) 
        {
            GameObject liveBullet = Instantiate(currentBullet, baseShotOrigins[i]);
        }
    }

    private void OctaShot()
    {
        for(int i = 0; i < octaShotOrigins.Count; i++) 
        {
            GameObject liveBullet = Instantiate(currentBullet, octaShotOrigins[i]);
        }
    }
    #endregion

    [System.Serializable]
    private class PowerupAudio
    {
        [SerializeField] private PowerupType powerupType;
        [SerializeField] private AudioClip powerupAudio;

        public PowerupType Powerup { get { return powerupType; } }
        public AudioClip Audio { get { return powerupAudio; } }

        public PowerupAudio(PowerupType _powerupType, AudioClip _powerupAudio)
        {
            powerupType = _powerupType;
            powerupAudio = _powerupAudio;
        }
    }
}
