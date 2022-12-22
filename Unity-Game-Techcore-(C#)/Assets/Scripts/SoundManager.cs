using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip terrainDestructionSound;
    public AudioClip missionCompletedSound;
    public AudioClip gameOverSound;
    public AudioClip youWinSound;
    public AudioClip pickupWeaponSound;
    public AudioClip dropWeaponSound;
    public AudioClip shootSound;
    public AudioClip hitSound;
    public AudioClip deathSound;

    public float terrainDestructionSoundVolume = 1;
    public float missionCompletedSoundVolume = 1;
    public float gameOverSoundVolume = 1;
    public float youWinSoundVolume = 1;
    public float pickupWeaponSoundVolume = 1;
    public float dropWeaponSoundVolume = 1;
    public float shootSoundVolume = 1;
    public float hitSoundVolume = 1;
    public float deathSoundVolume = 1;


    private AudioSource soundPlayer;

    // Start is called before the first frame update
    void Start()
    {
        soundPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playPickupWeaponSound()
    {
        soundPlayer.PlayOneShot(pickupWeaponSound, pickupWeaponSoundVolume);
    }

    public void playDropWeaponSound()
    {
        soundPlayer.PlayOneShot(dropWeaponSound, dropWeaponSoundVolume);
    }

    public void playShootSound()
    {
        soundPlayer.PlayOneShot(shootSound, shootSoundVolume);
    }

    public void playHitSound()
    {
        soundPlayer.PlayOneShot(hitSound, hitSoundVolume);
    }

    public void playDeathSound()
    {
        soundPlayer.PlayOneShot(deathSound, deathSoundVolume);
    }

    public void playTerrainDestructionSound()
    {
        soundPlayer.PlayOneShot(terrainDestructionSound, terrainDestructionSoundVolume);
    }

    public void playGameOverSound()
    {
        soundPlayer.PlayOneShot(gameOverSound, gameOverSoundVolume);
        StartCoroutine(ParallelSoundClip());
    }

    IEnumerator ParallelSoundClip()
    {
        yield return new WaitForSeconds(4);
        soundPlayer.PlayOneShot(youWinSound, youWinSoundVolume);
    }
}
