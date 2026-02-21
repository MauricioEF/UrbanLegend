using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource sfxAudioSource, musicAudioSource;
    public float sfxVolume;
    public static AudioManager Instance
    {
        get; private set;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void ChangeSfxVolume(float volume)
    {
        sfxVolume = volume;
    }

    public void ChangeMusicVolume(float volume)
    {
        musicAudioSource.volume = volume;
    }

    public void StopMusic()
    {
        musicAudioSource.Stop();
    }
    public void StopSfx()
    {
        sfxAudioSource.Stop();
    }

    public void StopMusicWithFading(float fadeTime)
    {
        StartCoroutine(FadeVolume(fadeTime));
    }

    IEnumerator FadeVolume(float fadeTime)
    {
        float startVolume = musicAudioSource.volume;

        while (musicAudioSource.volume > 0)
        {
            musicAudioSource.volume -= startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }

        musicAudioSource.Stop();
        musicAudioSource.volume = startVolume;
    }
    public void ResumeMusic()
    {
        musicAudioSource.Play();
    }
    public void PlaySoundEffect(AudioClip audioClip)
    {
        sfxAudioSource.PlayOneShot(audioClip, sfxVolume);
    }
    public void PlayTemporaryLoopedSoundEffect(AudioClip audioClip, float loopedTime)
    {
        StartCoroutine(LoopSfx(audioClip, loopedTime));
    }

    IEnumerator LoopSfx(AudioClip audioClip, float loopedTime)
    {
        sfxAudioSource.loop = true;
        sfxAudioSource.resource = audioClip;
        sfxAudioSource.Play();
        yield return new WaitForSeconds(loopedTime);
        sfxAudioSource.Stop();
        sfxAudioSource.loop = false;
    }

    IEnumerator LoopIntervalSfx(AudioClip audioClip, float interval)
    {
        sfxAudioSource.resource = audioClip;
        while (true)
        {
            sfxAudioSource.Play();
            yield return new WaitForSeconds(interval);
        }
    }
}

