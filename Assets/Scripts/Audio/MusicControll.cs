using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicControll : MonoBehaviour
{
    [SerializeField]
    AudioClip[] pause;
    [SerializeField]
    AudioClip gameOver;
    [SerializeField]
    AudioClip deathBackground;
    //bool alreadyStopped;
    bool isLoud = false;
    public float speed2change;
    public float volume;
    AudioSource audioSrc;
    public AudioClip[] audioClips;
    AudioClip curClip;
    AudioClip prevClip;
    // Start is called before the first frame update
    void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
        volume = MainSave.save.musicVolume;
        //audioSrc.volume = volume;
        audioSrc.volume = 0f;
        StartPlaying();
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
    void GetNewClip(AudioClip[] clips)
    {
        prevClip = audioSrc.clip;
        do
        {
            curClip = clips[Random.Range(0, clips.Length - 1)];
        } while (curClip == prevClip);
        audioSrc.clip = curClip;
        audioSrc.Play();
        
    }
    void StartPlaying()
    {
        GetNewClip(audioClips);
        StartCoroutine(ChangeVolume(2 * speed2change));
    }
    public void NewSong(float speed)
    {
        StartCoroutine(ChangeVolume(speed));
    }
    public void NewSong()
    {
        StartCoroutine(ChangeVolume(speed2change));
    }
    public void GameOver()
    {
        StartCoroutine(AfterDeath());
    }
    public void Pause(bool alreadyStopped)
    {
        if (!alreadyStopped)
        {
            audioSrc.Pause();
            GetNewClip(pause);
            audioSrc.clip = curClip;
            audioSrc.volume = 0.001f;
            StartCoroutine(ChangeVolume(2 * speed2change));
        }
        else
        {
            audioSrc.Stop();
            audioSrc.clip = prevClip;
            audioSrc.volume = volume;
        }
        audioSrc.Play();
    }
    public void SwitchWave()
    {

    }
    IEnumerator ChangeVolume(float speed)
    {
        if (audioSrc.volume >= volume)
        {
            audioSrc.volume = volume;
            isLoud = true;
            yield return null;
        }
        else if(audioSrc.volume <= 0)
        {
            audioSrc.volume = 0;
            isLoud = false;
            if (MainSave.save.musicVolume != 0)
            {
                GetNewClip(audioClips);
                audioSrc.volume += speed;
                ChangeVolume(speed);
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
            ChangeVolume(speed);
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
