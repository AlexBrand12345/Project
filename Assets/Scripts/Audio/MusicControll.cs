using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicControll : MonoBehaviour
{
    [SerializeField]
    public AudioClip[] waves;
    [SerializeField]
    public AudioClip[] audioClips;
    [SerializeField]
    public AudioClip[] pause;
    [SerializeField]
    public AudioClip gameOver;
    [SerializeField]
    public AudioClip deathBackground;
    [SerializeField]
    public AudioMixerGroup musicMixer;
    //bool alreadyStopped;
    bool clipFound;
    bool isLoud = false;
    public float speed2change;
    public float volume;
    public AudioSource UIaudioSource;
    AudioSource audioSrc;
    AudioClip curClip;
    AudioClip prevClip;
    // Start is called before the first frame update


    public void ChangeMusicVolume(float volume)
    {
        musicMixer.audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-80, 0, volume));
    }
    
    void Awake()
    {
        StartPlaying();
        audioSrc = GetComponent<AudioSource>();
        volume = MainSave.save.musicVolume;
        //audioSrc.volume = volume;
        audioSrc.volume = 0f;
        //StartPlaying();
    }

    // Update is called once per frame
    void Update()
    {
        if (volume != MainSave.save.musicVolume)
        {
            volume = MainSave.save.musicVolume;
            audioSrc.volume = volume;
        }
    }

    void StartPlaying()
    {
        GetNewClip(audioSrc, audioClips);
        StartCoroutine(ChangeVolume(audioSrc, 2 * speed2change, audioClips));
    }
    //public void NewSong(float speed)
    //{
    //    StartCoroutine(ChangeVolume(speed), audioClips);
    //}
    //public void NewSong()
    //{
    //    StartCoroutine(ChangeVolume(speed2change));
    //}
    public void GameOver()
    {
        StartCoroutine(AfterDeath());
    }
    void GetNewClip(AudioSource source, AudioClip[] clips)
    {
        prevClip = source.clip;
        do
        {
            curClip = clips[Random.Range(0, clips.Length - 1)];
        } while (curClip == prevClip);
        source.clip = curClip;
        source.Play();

    }
    public void Pause(bool alreadyStopped)
    {
        if (!alreadyStopped)
        {
            audioSrc.Pause();
            audioSrc.volume = 0;
            //ChangeVolume(speed2change, pause);
            StartCoroutine(ChangeVolume(UIaudioSource, 2 * speed2change, pause));
            //audioSrc.clip = curClip;
        }
        else
        {
            UIaudioSource.Stop();
            audioSrc.UnPause();
            //audioSrc.Stop();
            //audioSrc.clip = prevClip;
            //audioSrc.volume = volume;
        }
        //audioSrc.Play();
    }
    public IEnumerator SwitchWave(float time2change)
    {
        StartCoroutine(ChangeVolume(audioSrc, speed2change, audioClips));
        yield return new WaitForSeconds(time2change - 3f);
        StartCoroutine(ChangeVolume(audioSrc, speed2change * 2, waves));
    }
    IEnumerator ChangeVolume(AudioSource source, float speed, AudioClip[] clips)
    {
        if (source.volume > volume)
        {
            audioSrc.volume = volume;
            isLoud = true;
            clipFound = false;
            yield return null;
        }
        else if (source.volume <= 0)
        {
            audioSrc.volume = 0;
            isLoud = false;
            if (MainSave.save.musicVolume != 0)
            {
                source.volume += speed;
                StartCoroutine(ChangeVolume(source, speed, clips));
            }
            if (!clipFound)
            {
                GetNewClip(source,clips);
                clipFound = true;
            }
            yield return null;
            //audioSrc.volume += speed;
            //ChangeVolume(speed);
        }
        else
        {
            if (!isLoud)
            {
                audioSrc.volume += speed;
            }
            else
            {
                audioSrc.volume -= speed;
            }
            yield return new WaitForSeconds(0.001f);
            ChangeVolume(source, speed, clips);
        }

    }

    IEnumerator AfterDeath()
    {
        audioSrc.Stop();
        audioSrc.clip = gameOver;
        audioSrc.Play();
        yield return new WaitForSeconds(audioSrc.clip.length);
        audioSrc.clip = deathBackground;
        audioSrc.Play();
    }
}
