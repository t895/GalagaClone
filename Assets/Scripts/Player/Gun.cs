using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    public Transform shotOrigin;
    public AudioClip shotSound;
    public float fireRate = 1f;
    
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
            Shoot();
    }

    private void Shoot()
    {
        if(player.isAlive && Time.time > nextFire)
        {
            audioPlayer.PlayOneShot(shotSound);
            nextFire = Time.time + fireRate;
            GameObject liveBullet = Instantiate(bullet, shotOrigin);
        }
    }

}
