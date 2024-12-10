using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSettings : MonoBehaviour
{

    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    //public const string MIXER_MUSIC = "MusicVolume";
    //public const string MIXER_SFX = "SFXVolume";

    //void Awake()
    //{
    //    musicSlider.onValueChanged.AddListener(SetMusicVolume);
    //    sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    //}


    void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat(AudioManager.MUSIC_KEY, 0.4f);
        sfxSlider.value = PlayerPrefs.GetFloat(AudioManager.SFX_KEY, 0.4f);

        SetMusicVolume(musicSlider.value);
        SetSFXVolume(sfxSlider.value);
    }


    public void SetMusicVolume(float value)
    {
        float trueVolume = Mathf.Log10(musicSlider.value) * 20;

        if (float.IsInfinity(trueVolume))
        {
            trueVolume = -80;
        }

        mixer.SetFloat(AudioManager.MUSIC_KEY, trueVolume);

        PlayerPrefs.SetFloat(AudioManager.MUSIC_KEY, musicSlider.value);
    }


    public void SetSFXVolume(float value)
    {
        float trueVolume = Mathf.Log10(sfxSlider.value) * 20;

        if (float.IsInfinity(trueVolume))
        {
            trueVolume = -80;
        }

        mixer.SetFloat(AudioManager.SFX_KEY, trueVolume);

        PlayerPrefs.SetFloat(AudioManager.SFX_KEY, sfxSlider.value);
    }
}
