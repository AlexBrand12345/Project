using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsControll : MonoBehaviour
{
    AudioSource audioSrc;
    float volume;
    void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {       
        if (volume != MainSave.save.effectsVolume)
        {
            volume = MainSave.save.effectsVolume;
            audioSrc.volume = volume;
        }
    }
    public void PlayShortClip(AudioClip clip)
    {
        audioSrc.clip = clip;
        audioSrc.Play();
    }
    public void PlayOneShot(AudioClip clip)
    {
        audioSrc.PlayOneShot(clip);
    }
}
