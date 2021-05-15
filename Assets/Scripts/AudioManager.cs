using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    static AudioManager current;

    [Header("环境音效")]
    public AudioClip absClip;
    public AudioClip bgMusicClip;

    [Header("人物音效")]
    public AudioClip[] crouchClip;
    public AudioClip[] playStepClip;
    public AudioClip jumpFxClip;
    public AudioClip jumpPlayClip;

    public AudioClip deathPlayClip;
    public AudioClip deathVoiceClip;
    public AudioClip propBackClip;

    public AudioClip orbFXClip;
    public AudioClip orbVoiceClip;

    
     AudioSource absAudioSource;
     AudioSource bgAudioSource;
     AudioSource fxAudioSource;
     AudioSource playAudioSource;
     AudioSource voiceSource;

    void Awake()
    {
        if (current != null)
        {
            Destroy(gameObject);
            return;
        }
        current = this;

        DontDestroyOnLoad(gameObject);

        fxAudioSource = gameObject.AddComponent<AudioSource>();
        playAudioSource = gameObject.AddComponent<AudioSource>();
        voiceSource = gameObject.AddComponent<AudioSource>();
        absAudioSource = gameObject.AddComponent<AudioSource>();
        bgAudioSource = gameObject.AddComponent<AudioSource>();
        
        startLevelBGAudio();
    }

    private void startLevelBGAudio()
    {
        current.absAudioSource.clip = current.absClip;
        current.absAudioSource.loop = true;
        current.absAudioSource.Play();

        current.bgAudioSource.clip = current.bgMusicClip;
        current.bgAudioSource.loop = true;
        current.bgAudioSource.Play();
    }

    public static void playStepAudio()
    {
        int index = Random.Range(0, current.playStepClip.Length);
        current.playAudioSource.clip = current.playStepClip[index];
        current.playAudioSource.Play();
    }

    public static void playCroushAudio()
    {
        int index = Random.Range(0, current.crouchClip.Length);
        current.playAudioSource.clip = current.crouchClip[index];
        current.playAudioSource.Play();
    }

    public static void playJumpAudio()
    {
        current.voiceSource.clip = current.jumpFxClip;
        current.voiceSource.Play();

        current.playAudioSource.clip = current.jumpPlayClip;
        current.playAudioSource.Play();
    }

    public static void playDeathAudio()
    {
        current.voiceSource.clip = current.deathVoiceClip;
        current.voiceSource.Play();

        current.playAudioSource.clip = current.deathPlayClip;
        current.playAudioSource.Play();

        current.fxAudioSource.clip = current.propBackClip;
        current.fxAudioSource.Play();
    }

    public static void playOrbAudio()
    {
        current.fxAudioSource.clip = current.orbFXClip;
        current.fxAudioSource.Play();

        current.playAudioSource.clip = current.orbVoiceClip;
        current.playAudioSource.Play();
    }
}
