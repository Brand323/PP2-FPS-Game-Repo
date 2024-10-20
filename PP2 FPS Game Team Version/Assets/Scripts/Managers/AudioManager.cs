using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("----- Audio -----")]
    [SerializeField] AudioSource playerAudioSource;
    [SerializeField] public AudioSource backgroundAudioSource;

    [Header("----- Actions -----")]
    [SerializeField] public float sfxVolume;
    [SerializeField] public AudioClip[] jumpSounds;
    public bool isPlayingStepSound;
    [SerializeField] public AudioClip footStepSound;
    [SerializeField] public AudioClip[] hurtSounds;
    [SerializeField] public AudioClip menuButtonSound;
    [SerializeField] public AudioClip menuPopSound;

    [Header("----- Background -----")]
    [SerializeField] public AudioClip backgroundMusic;
    [SerializeField] public float backgroundMusicVolume;
    public bool backgroundMusicIsPlaying;


    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        backgroundAudioSource.clip = backgroundMusic;
        backgroundAudioSource.volume = backgroundMusicVolume;
        backgroundAudioSource.loop = true;
        if (SceneManager.GetActiveScene().name == "CombatSceneArctic")
        {
            backgroundAudioSource.Play();
            backgroundMusicIsPlaying = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        sfxVolume = UIManager.instance.sfxVolume.value;
        backgroundMusicVolume = UIManager.instance.musicVolume.value;
        backgroundAudioSource.volume = backgroundMusicVolume;
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
