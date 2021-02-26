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
    bool changeStarted;
    bool isLoud = false;
    public float speed2change;
    public float volume;
    public AudioSource UIaudioSource;
    AudioSource audioSrc;
    AudioClip curClip;
    AudioClip prevClip;
    Coroutine ChangeVol;
    // Start is called before the first frame update


    public void ChangeMusicVolume(float volume)
    {
        musicMixer.audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-80, 0, volume));
    }
    
    void Awake()
    {      
        audioSrc = GetComponent<AudioSource>();
        volume = 1f;
        audioSrc.volume = 0.01f;
        UIaudioSource.volume = 0.01f;
        StartPlaying();
        //volume = MainSave.save.musicVolume;      
        //audioSrc.volume = volume;       
        //StartPlaying();
    }

    // Update is called once per frame
    void Update()
    {
        //if (volume != MainSave.save.musicVolume)
        //{
            //volume = MainSave.save.musicVolume;
            //audioSrc.volume = volume;
        //}
    }

    void StartPlaying()
    {
        //GetNewClip(audioSrc, audioClips);
        ChangeVolume(audioSrc, 2 * speed2change, audioClips);
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
        //audioSrc.volume = volume;
        //source.Stop();
        //do
        //{
            curClip = clips[Random.Range(0, clips.Length - 1)];
        //} //while (curClip == prevClip);
        source.clip = curClip;
        Debug.Log(curClip);
        source.Play();
        clipFound = true;

    }
    public void Pause(bool isStopped)
    {
        if (isStopped)
        {
            audioSrc.Pause();
            if (ChangeVol != null)
            StopCoroutine(ChangeVol);
            //audioSrc.volume = 0f;
            //ChangeVolumeVoid(UIaudioSource, speed2change, pause);
            ChangeVolume(UIaudioSource, 7 * speed2change, pause);
            //Time.timeScale = 0.07f;
            //audioSrc.clip = curClip;
        }
        else
        {
            if (ChangeVol != null)
            StopCoroutine(ChangeVol);
            //UIaudioSource.volume = 0f;
            UIaudioSource.Stop();
            audioSrc.UnPause();
            //audioSrc.Stop();
            //audioSrc.clip = prevClip;
            //audioSrc.volume = volume;
        }
        //audioSrc.Play();
    }
    //void ChangeVolumeVoid(AudioSource source, float speed, AudioClip[] clips)
    //{
    //    if(ChangeVol != null)
    //    StopCoroutine(ChangeVol);
    //    ChangeVol = StartCoroutine(ChangeVolume(source, speed, clips));
    //}
    public IEnumerator SwitchWave(float time2change)
    {
        //ChangeVolumeVoid(audioSrc, speed2change, audioClips);
        ChangeVolume(audioSrc, speed2change, audioClips);
        yield return new WaitForSeconds(time2change);
        ChangeVolume(audioSrc, speed2change * 2, waves);
    }
    //IEnumerator ChangeVolume(AudioSource source, float speed, AudioClip[] clips)
    //{
    //    if (source.volume <= 0) //конец уменьшения громкости, смена композиции
    //    {
    //        Debug.Log("volume=0");
    //        source.volume = 0;
    //        clipFound = false;
    //        isLoud = false;
    //        Debug.Log(MainSave.save.musicVolume);            
    //        if (!clipFound)
    //        {
    //            Debug.Log("LookingFornewClip");
    //            GetNewClip(source, clips);
    //            //clipFound = true;
    //        }
    //        if (MainSave.save.musicVolume != 0)
    //        {
    //            Debug.Log(source.volume);
    //            source.volume += speed;
    //            yield return null;
    //            ChangeVolumeVoid(source, speed, clips);
    //        }
    //        yield return null;
    //        //audioSrc.volume += speed;
    //        //ChangeVolume(speed);
    //    }
    //    else
    //    {
    //        if (!isLoud)
    //        {
    //            source.volume += speed;
    //            if (source.volume >= volume) //конец увеличения громкости
    //            {
    //                Debug.Log("volume=max");
    //                source.volume = volume;
    //                isLoud = true;
    //                clipFound = false;
    //                yield return null;
    //            }
    //        }
    //        else
    //        {
    //            source.volume -= speed;
    //        }
    //        yield return new WaitForSeconds(0.1f);
    //        ChangeVolumeVoid(source, speed, clips);
    //    }

    //}
    void ChangeVolume(AudioSource source, float speed, AudioClip[] clips)
    {
        if(ChangeVol != null)
        StopCoroutine(ChangeVol);
        StartToChangeMusic();
        //if(!changeStarted) 
        ChangeVol = StartCoroutine(ChangeSourceVolume(source, speed, clips));
    }
    void StartToChangeMusic()
    {
        isLoud = true;
        clipFound = false;
        //changeStarted = true;
    }
    void LookForNewClip(AudioSource source, AudioClip[] clips)
    {
        isLoud = false;
        Debug.Log("LookingFoClip");
        source.volume = 0f;
        GetNewClip(source, clips);      
    }
    void EndToChangeMusic(AudioSource source, AudioClip[] clips)
    {
        source.volume = volume;
        isLoud = true;
        if (UIaudioSource.isPlaying) Time.timeScale = 0.03f;
        //changeStarted = false;
    }
    IEnumerator ChangeSourceVolume(AudioSource source, float speed, AudioClip[] clips)
    {
        if (!isLoud)
        {
            if (source.volume >= volume)
            {
                EndToChangeMusic(source, clips);
                Debug.Log("End");
                yield break;
            }
            else
            source.volume += speed;
        }
        else
        {
            if(source.volume <= 0)
            {
                isLoud = false;
                LookForNewClip(source, clips);
            }
            else
            source.volume -= speed;
        }
        yield return new WaitForSeconds(0.1f);
        //ChangeVolume(source, speed, clips);
        StartCoroutine(ChangeSourceVolume(source, speed, clips));
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
