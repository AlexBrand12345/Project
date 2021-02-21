using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Slider effectsSlider;
    public Slider musicSlider;
    [SerializeField]
    MusicControll music;
    [SerializeField]
    EffectsControll effects;
    [SerializeField]
    Escbuttons esc;

    private void Start()
    {
        music = GameObject.FindWithTag("MusicControll").GetComponent<MusicControll>();
        effects = GameObject.FindWithTag("EffectsControll").GetComponent<EffectsControll>();
        esc = GameObject.FindWithTag("EscController").GetComponent<Escbuttons>();
        effectsSlider.value = MainSave.save.effectsVolume;
        musicSlider.value = MainSave.save.musicVolume;
    }
    public void ChangeMusicVolume(Slider slider)
    {
        music.ChangeMusicVolume(slider.value);
        MainSave.save.musicVolume = musicSlider.value;      
    }
    public void ChangeEffectsVolume(Slider slider)
    {
        effects.ChangeEffectsVolume(slider.value);
        MainSave.save.effectsVolume = effectsSlider.value;
    }
    public void Destroy()
    {
        esc.Settings();
        SaveLoad.Save();
    }
}
