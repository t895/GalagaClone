using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour, IPooledObject
{
    [SerializeField] private AudioClip hitClip;
    private AudioSource audioSource;
    private ParticleSystem particles;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        particles = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if(hitClip != null)
            if(!(particles.isPlaying && audioSource.isPlaying))
                gameObject.SetActive(false);
    }

    public void OnObjectSpawn()
    {
        if(hitClip != null)
            audioSource.PlayOneShot(hitClip);
        particles.Play();
    }
}
