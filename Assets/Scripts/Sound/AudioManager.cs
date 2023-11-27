using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public SoundList music;
    public SoundList sfxSounds;
    public AudioSource musicSource;
    public AudioSource[] sfxSources;

    public class SoundInstance { public Sound sound; public Vector3 location;  }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void PlaySFX(string name, Vector2 position, float volume = 1f)
    {
        Sound s = sfxSounds.Sounds.Find(x => x.name == name);      
        if (s == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            //sfxSources[0].clip = s.clip;
            //sfxSources[0].volume = volume;
            sfxSources[0].transform.position = position;
            sfxSources[0].PlayOneShot(s.clip, volume);
            //AudioSource (s.clip, position, volume);
        }
    }

    public void PlaySFX(AudioClip clip, Vector2 position, float volume = 1f)
    {
        if (!sfxSources[1].isPlaying)
        {
            sfxSources[1].transform.position = position;
            sfxSources[1].PlayOneShot(clip, volume);
        }
    }

    public void PlayMusic(string name, float volume = .5f)
    {
        Sound s = music.Sounds.Find(x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.volume = volume;
            musicSource.Play();
        }
    }

    public void PauseResume(bool pause)
    {
        if (pause)
        {
            musicSource.Pause();
        }
        else
        {
            musicSource.Play();
        }
    }

    public void PlayMusicForScene(string sceneName)
    {
        // Select the music track based on the scene name
        switch (sceneName)
        {
            case "MainMenu":
                PlayMusic("Menu", .3f);
                break;
            case "Game":
                PlayMusic("Game", .3f);
                break;
                // Add more cases as needed for each scene
        }
    }
}
