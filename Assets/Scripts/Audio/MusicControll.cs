using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class MusicControll : MonoBehaviour
//{
//    [SerializeField]
//    public AudioClip[] waves;
//    [SerializeField]
//    public AudioClip[] audioClips;
//    [SerializeField]
//    public AudioClip[] pause;
//    [SerializeField]
//    public AudioClip gameOver;
//    [SerializeField]
//    public AudioClip deathBackground;
//    //bool alreadyStopped;
//    bool isLoud = false;
//    public float speed2change;
//    public float volume;
//    AudioSource audioSrc;
//    AudioClip curClip;
//    AudioClip prevClip;
//    // Start is called before the first frame update
//    void Awake()
//    {
//        audioSrc = GetComponent<AudioSource>();
//        volume = MainSave.save.musicVolume;
//        //audioSrc.volume = volume;
//        audioSrc.volume = 0f;
//        //StartPlaying();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (volume != MainSave.save.musicVolume)
//        {
//            volume = MainSave.save.musicVolume;
//            audioSrc.volume = volume;
//        }
//    }
    
//    void StartPlaying()
//    {
//        GetNewClip(audioClips);
//        StartCoroutine(ChangeVolume(2 * speed2change, audioClips));
//    }
//    //public void NewSong(float speed)
//    //{
//    //    StartCoroutine(ChangeVolume(speed), audioClips);
//    //}
//    //public void NewSong()
//    //{
//    //    StartCoroutine(ChangeVolume(speed2change));
//    //}
//    public void GameOver()
//    {
//        StartCoroutine(AfterDeath());
//    }
//    void GetNewClip(AudioClip[] clips)
//    {
//        prevClip = audioSrc.clip;
//        do
//        {
//            curClip = clips[Random.Range(0, clips.Length - 1)];
//        } while (curClip == prevClip);
//        audioSrc.clip = curClip;
//        audioSrc.Play();

//    }
//    public void Pause(bool alreadyStopped)
//    {
//        if (!alreadyStopped)
//        {
//            audioSrc.Pause();
//            audioSrc.volume = 0;
//            //ChangeVolume(speed2change, pause);
//            StartCoroutine(ChangeVolume(2 * speed2change, pause));
//            audioSrc.clip = curClip;
//        }
//        else
//        {
//            audioSrc.Stop();
//            audioSrc.clip = prevClip;
//            audioSrc.volume = volume;
//        }
//        audioSrc.Play();
//    }
//    public IEnumerator SwitchWave(float time2change)
//    {
//        StartCoroutine(ChangeVolume(speed2change, audioClips));
//        yield return new WaitForSeconds(time2change - 3f);
//        StartCoroutine(ChangeVolume(speed2change * 2, waves));
//    }
//    IEnumerator ChangeVolume(float speed, AudioClip[] clips)
//    {
//        if (audioSrc.volume > volume)
//        {
//            audioSrc.volume = volume;
//            isLoud = true;
//            yield return null;
//        }
//        else if(audioSrc.volume <= 0)
//        {
//            audioSrc.volume = 0;
//            isLoud = false;
//            if (MainSave.save.musicVolume != 0)
//            {
//                audioSrc.volume += speed;
//                ChangeVolume(speed, clips);
//            }
//            GetNewClip(clips);
//            yield return null;
//            //audioSrc.volume += speed;
//            //ChangeVolume(speed);
//        }
//        else 
//        {
//            if (!isLoud)
//            {
//                audioSrc.volume += speed;
//            }
//            else
//            {
//                audioSrc.volume -= speed;
//            }
//            yield return new WaitForSeconds(0.001f);
//            ChangeVolume(speed, clips); 
//        }
        
//    }
   
//    IEnumerator AfterDeath()
//    {
//        audioSrc.Stop();
//        audioSrc.clip = gameOver;
//        audioSrc.Play();
//        yield return new WaitForSeconds(audioSrc.clip.length);
//        audioSrc.clip = deathBackground;
//        audioSrc.Play();
//    }
//}
