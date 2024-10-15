using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("----- Audio -----")]
    [SerializeField] AudioSource playerAudioSource;
    [SerializeField] public AudioSource backgtoundAudioSource;

    [Header("----- Actions -----")]
    [SerializeField] public AudioClip[] jumpSounds;
    [SerializeField] public float jumpSoundsVolume;
    public bool isPlayingStepSound;
    [SerializeField] public AudioClip footStepSound;
    [SerializeField] public float footStepVolume;
    [SerializeField] public AudioClip[] hurtSounds;
    [SerializeField] public float hurtSoundsVolume;
    [SerializeField] public AudioClip[] menuButtonSound;
    [SerializeField] public float menuButtonVolume;

    [Header("----- Background -----")]
    [SerializeField] public AudioClip backgroundMusic;
    [SerializeField] public float backgroundMusicVolume;
    public bool backgroundMusicIsPlaying;


    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        backgtoundAudioSource.clip = backgroundMusic;
        backgtoundAudioSource.volume = backgroundMusicVolume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playSound(AudioClip[] clips, float volume)
    {
        playerAudioSource.PlayOneShot(clips[Random.Range(0, clips.Length)], volume);
    }

    public void playSound(AudioClip clip, float volume)
    {
        playerAudioSource.PlayOneShot(clip, volume);
    }

}
