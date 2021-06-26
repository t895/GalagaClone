using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    public GameObject defaultBullet;
    public float fireRate = 1f;
    public PowerupType powerupType;
    [SerializeField] private AudioClip defaultShotSound;
    [SerializeField] private AudioClip disablePowerupSound;
    [SerializeField] private List<PowerupAudio> powerupAudio;
    private AudioSource audioPlayer;
    
    [SerializeField] private List<Transform> baseShotOrigins;
    [SerializeField] private List<Transform> octaShotOrigins;
    private float nextFire = 0f;
    private PlayerManager player;
    private PlayerController controller;
    
    private GameObject currentBullet;
    private float customRate = 0;
    private AudioClip currentSound;

    private void Awake()
    {
        PlayerVariables.playerGun = this;   
    }

    private void Start()
    {
        player = GetComponent<PlayerManager>();
        controller = GetComponent<PlayerController>();
        audioPlayer = GetComponent<AudioSource>();
        currentSound = defaultShotSound;
        currentBullet = defaultBullet;

        PlayerVariables.playerControls.InGame.ClearPowerup.performed += cxt => RemovePowerupManual();
    }

    private void Update()
    {
        float shotInput = PlayerVariables.playerControls.InGame.Shoot.ReadValue<float>();

        if(!GameState.paused && player.isAlive)
        {
            if(shotInput == 1)
                AttemptShot();
        }
    }

    private void AttemptShot()
    {
        if(Time.time > nextFire)
            {
                if(customRate == 0)
                {
                    nextFire = Time.time + fireRate;
                    PickShot();
                }
                else
                {
                    nextFire = Time.time + customRate;
                    PickShot();
                }
            }
    }

    private void RemovePowerupManual()
    {
        if(powerupType != PowerupType.none && !GameState.paused && player.isAlive)
                DisablePowerup();
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
            if(powerupAudio[i].powerup == _powerupType && powerupAudio[i].audio != null)
                return powerupAudio[i].audio;
        }
        return defaultShotSound;
    }

    private IEnumerator PowerupTimeout(float _time)
    {
        yield return new WaitForSeconds(_time);
        DisablePowerup();
    }

    public void PowerupTaken(PowerupType _powerupType, int _powerupDuration, GameObject _customBullet, float _customRate)
    {
        StopAllCoroutines();
        StartCoroutine(PowerupTimeout(_powerupDuration));
        powerupType = _powerupType;

        if(_customBullet != null)
            currentBullet = _customBullet;
        else
            currentBullet = defaultBullet;

        if(_customRate != 0)
            customRate = _customRate;
        else
            customRate = 0;

        currentSound = FindAudio(powerupType);
    }

    private void DisablePowerup()
    {
        powerupType = PowerupType.none;
        currentBullet = defaultBullet;
        customRate = 0;
        currentSound = defaultShotSound;
        audioPlayer.PlayOneShot(disablePowerupSound);
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
        [SerializeField] private string name;
        public PowerupType powerup;
        public AudioClip audio;

        //public PowerupType Powerup { get { return powerupType; } }
        //public AudioClip Audio { get { return powerupAudio; } }
    }
}
