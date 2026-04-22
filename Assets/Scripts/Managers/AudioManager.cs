using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource musicSource;
    public AudioSource sfxSource;

    // Clips de audio
    public AudioClip collectBanana;
    public AudioClip collectApple;
    public AudioClip collectGoldenBall;
    public AudioClip jump;
    public AudioClip boostedJump;
    public AudioClip punch;
    public AudioClip fallWater;

    
    void Awake()
    {
        Instance = this;
    }

    public void PlayCollectBanana()
    {
        PlaySFX(collectBanana);
    }
    public void PlayCollectApple()
    {
        PlaySFX(collectApple);
    }
    public void PlayCollectGoldenBall()
    {
        PlaySFX(collectGoldenBall);
    }
    public void PlayJump()
    {
        PlaySFX(jump);
    }
    public void PlayBoostedJump()
    {
        PlaySFX(boostedJump);
    }
    public void PlayPunch()
    {
        PlaySFX(punch);
    }

    public void PlayFallWater()
    {
        PlaySFX(fallWater);
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
            sfxSource.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        if (clip == null) return;
        musicSource.clip = clip;
        musicSource.loop = loop;
        musicSource.Play();
    }
}
