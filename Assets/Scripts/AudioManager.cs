using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
   
    public static AudioManager instance;


    [SerializeField] AudioMixer mixer;
    [SerializeField] AudioSource musicSource;
    [SerializeField] List<AudioClip> musicClips = new List<AudioClip>();



    public const string MUSIC_KEY = "musicVolume";
    public const string SFX_KEY = "sfxVolume";

    void Awake()
    {
        LoadVolume();

        if (instance == null)
        {
            RandomMusic();  

            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {

            Destroy(gameObject);

        }
    }

    public void RandomMusic()
    {

        AudioClip clip = musicClips[Random.Range(0, musicClips.Count)];
        musicSource.clip = clip;
        musicSource.Play();
    }

    void LoadVolume() //Volune saved in VolumeSettings.cs
    {
        float musicVolume = PlayerPrefs.GetFloat(MUSIC_KEY, 0.4f);
        float sfxVolume = PlayerPrefs.GetFloat(SFX_KEY, 0.4f);
        print(musicVolume);
        mixer.SetFloat(AudioManager.MUSIC_KEY, Mathf.Log10(musicVolume)* 20);
        mixer.SetFloat(AudioManager.SFX_KEY, Mathf.Log10(sfxVolume)* 20);

    }


}
