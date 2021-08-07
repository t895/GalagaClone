using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] private List<Transform> baseShotOrigins;
    [SerializeField] private List<Transform> octaShotOrigins;
    private float nextFire = 0f;

    public PlayerBulletObject defaultBullet;
    public PlayerBulletObject currentBullet;

    private AudioSource audioPlayer;
    [SerializeField] private AudioClip disablePowerupSound;

    private void Awake()
    {
        PlayerVariables.playerGun = this;   
    }

    private void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        currentBullet = defaultBullet;

        PlayerVariables.playerControls.InGame.ClearPowerup.performed += cxt => DisablePowerup();
    }

    private void Update()
    {
        float shotInput = PlayerVariables.playerControls.InGame.Shoot.ReadValue<float>();

        if(!GameState.paused && PlayerVariables.playerManager.isAlive)
            if(shotInput == 1)
                AttemptShot();
    }

    private void AttemptShot()
    {
        if(Time.time > nextFire)
        {
            if(currentBullet.fireRate == defaultBullet.fireRate)
                nextFire = Time.time + defaultBullet.fireRate;
            else
                nextFire = Time.time + currentBullet.fireRate;

            PickShot();
        }
    }

    private void PickShot()
    {
        if (currentBullet.powerupType == PowerupType.tripleShot)
            TripleShot();
        else if (currentBullet.powerupType == PowerupType.octaShot)
            OctaShot();
        else
            Shoot();

        audioPlayer.PlayOneShot(currentBullet.audio);
    }

    private IEnumerator PowerupTimeout(float _time)
    {
        yield return new WaitForSeconds(_time);
        DisablePowerup();
    }

    public void PowerupTaken(PlayerBulletObject _customBullet)
    {
        currentBullet = _customBullet;
        StopAllCoroutines();
        StartCoroutine(PowerupTimeout(currentBullet.powerupDuration));
    }

    private void DisablePowerup()
    {
        if(currentBullet.powerupType != PowerupType.none && !GameState.paused && PlayerVariables.playerManager.isAlive)
        {
            currentBullet = defaultBullet;
            audioPlayer.PlayOneShot(disablePowerupSound);
        }
    }

    //TODO: Find a way to avoid this
    #region ShotTypes
    private void Shoot()
    {
        ObjectPooler.Instance
            .SpawnFromPool(currentBullet.pooledObject, baseShotOrigins[0].position, baseShotOrigins[0].rotation)
            .GetComponent<Bullet>().Initialize(currentBullet);
    }

    private void TripleShot()
    {
        for(int i = 0; i <= 2; i++) 
        {
            ObjectPooler.Instance
                .SpawnFromPool(currentBullet.pooledObject, baseShotOrigins[i].position, baseShotOrigins[i].rotation)
                .GetComponent<Bullet>().Initialize(currentBullet);
        }
    }

    private void OctaShot()
    {
        for(int i = 0; i < octaShotOrigins.Count; i++) 
        {
            ObjectPooler.Instance
                .SpawnFromPool(currentBullet.pooledObject, octaShotOrigins[i].position, octaShotOrigins[i].rotation)
                .GetComponent<Bullet>().Initialize(currentBullet);
        }
    }
    #endregion
}
