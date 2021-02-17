using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EffectsControll : MonoBehaviour
{
    [SerializeField]
    public AudioMixerGroup effectsMixer;
    public AudioSource UIaudioSrc;
    AudioSource audioSrc;
    AudioSource source;
    public float volume;

    public void ChangeEffectsVolume(float volume)
    {
        effectsMixer.audioMixer.SetFloat("EffectsVolume", Mathf.Lerp(-80, 0, volume));
    }

    void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {       
        if (volume != MainSave.save.effectsVolume)
        {
            //volume = MainSave.save.effectsVolume;
            audioSrc.volume = volume;
            UIaudioSrc.volume = volume;
        }
    }
    public void PlayShortClip(int sourceInt, AudioClip clip)
    {
        if (sourceInt == 0) source = audioSrc;
        else source = UIaudioSrc;
        source.clip = clip;
        source.Play();
    }
    public void PlayShortClip(GameObject self)
    {
        self.GetComponent<AudioSource>().Play();
        //audioSrc.Play();
    }
    public void PlayOneShot(int sourceInt, AudioClip clip)
    {
        if (sourceInt == 0) source = audioSrc;
        else source = UIaudioSrc;
        source.PlayOneShot(clip);
    }
    public void PlayOneShot(GameObject self)
    {
        self.GetComponent<AudioSource>().PlayOneShot(self.GetComponent<AudioSource>().clip);
    }
}
