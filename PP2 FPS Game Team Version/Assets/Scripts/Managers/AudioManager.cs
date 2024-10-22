using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private float fadeSpeed = 3f;
    public bool fadeEnded = false;

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
    [SerializeField] public AudioClip buySound;
    [SerializeField] public AudioClip mapTriggerSound;

    [Header("----- Background -----")]
    [SerializeField] public AudioClip backgroundMusic;
    [SerializeField] public float backgroundMusicVolume;
    public bool backgroundMusicIsPlaying;
    [SerializeField] public AudioClip mapBackgroundMusic;
    public bool mapBackgroundMusicIsPlaying;


    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        backgroundAudioSource.volume = backgroundMusicVolume;
        backgroundAudioSource.loop = true;
        if (SceneManager.GetActiveScene().name == "CombatSceneArctic")
        {
            backgroundAudioSource.clip = backgroundMusic;
            backgroundAudioSource.Play();
            backgroundMusicIsPlaying = true;
            fadeIn();
        }
        if (SceneManager.GetActiveScene().name == "Map Scene")
        {
            backgroundAudioSource.clip = mapBackgroundMusic;
            backgroundAudioSource.Play();
            mapBackgroundMusicIsPlaying = true;
            fadeIn();
        }
    }

    // Update is called once per frame
    void Update()
    {
        sfxVolume = UIManager.instance.sfxVolume.value;
        if (fadeEnded)
        {
            backgroundMusicVolume = UIManager.instance.musicVolume.value;
        }
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
    public void fadeIn()
    {
        backgroundMusicVolume = 0f;
        StartCoroutine(musicFadeIn(0.3f));//objective is max volume
    }

    public void fadeOut()
    {
        backgroundMusicVolume = 0.3f;
        StartCoroutine(musicFadeOut(0f));//objective is no volume
    }

    IEnumerator musicFadeIn(float target)
    {
        while (backgroundMusicVolume < target - 0.0001)
        {
            backgroundMusicVolume = Mathf.Lerp(backgroundMusicVolume, target, fadeSpeed * Time.deltaTime);
            yield return null;
        }
        fadeEnded = true;
        yield break;
    }

    IEnumerator musicFadeOut(float target)
    {
        while (backgroundMusicVolume > target + 0.0001)
        {
            backgroundMusicVolume = Mathf.Lerp(backgroundMusicVolume, target, fadeSpeed * Time.deltaTime);
            yield return null;
        }
        fadeEnded = true;
        yield break;
    }
}
