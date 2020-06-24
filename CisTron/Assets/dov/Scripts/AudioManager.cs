using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Toggle _toggleWhichChanges;
    public Sound[] sounds;
    int _volumeToggle = 1;
    public static AudioManager Instance { get; private set; }
   


    void Awake()
    {
       /* if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(Instance.gameObject);
            Toggle[] toggles = Resources.FindObjectsOfTypeAll<Toggle>();

            Debug.Log(toggles.Length);
            for (int i = 0; i < toggles.Length; i++)
            {
                if (toggles[i].name == "ToggleAudio")
                {
                    Instance._toggleWhichChanges = toggles[i];
                    break;
                  }
            }

            ChangeToggle();
        }*/

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            //s.source.volume = GameObject.Find("SliderAudio").GetComponent<Slider>().value;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.playOnAwake;
        }

        SetAudio();

        PlayOnAwake();
    }

    void PlayOnAwake()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].playOnAwake)
            {
                Play(sounds[i].name);
            }
        }
    }

    public void ChangeToggle()
    {
        if (_toggleWhichChanges.isOn)
        {
            _volumeToggle = 1;
            FindObjectOfType<AudioManager>().Play("SFX_Toggle01");
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("SFX_Toggle02");
            _volumeToggle = 0;
        }
        SetAudio();
    }

    public void SetAudio()
    {
        foreach (Sound s in sounds)
        {
            if (_volumeToggle == 0)
            {
                s.source.volume = _volumeToggle;
            }
            else
            {
                for (int i = 0; i < sounds.Length; i++)
                {
                    if (sounds[i].clip == s.source.clip)
                    {
                        s.source.volume = sounds[i].volume;
                    }
                }
            }
        }
    }
    public void Play(string name)
    {
        if(_volumeToggle == 1)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            if (s == null)
                return;
            s.source.Play();
        }
    }

    public void Stop(string name)
    {
        if (_volumeToggle == 1)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            if (s == null)
                return;
            s.source.Stop();
        }
    }

    public void PlayRandomPitch(string name,float pitchMin,float pitchMax)
    {
        if (_volumeToggle == 1)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
          
            if (s == null)
                return;

            s.source.pitch = UnityEngine.Random.Range(pitchMin, pitchMax);
            s.source.Play();
            
        }
    }

    public void PlaySoundClick()
    {
        FindObjectOfType<AudioManager>().Play("Click");
    }

    public void PlaySoundToggleUp()
    {
        FindObjectOfType<AudioManager>().Play("SFX_Toggle01");
    }

    public void PlaySoundToggleDown()
    {
        FindObjectOfType<AudioManager>().Play("SFX_Toggle02");
    }

    public void PlaySoundClickUp()
    {
        FindObjectOfType<AudioManager>().Play("SFX_Click01");
    }

    public void PlaySoundClickDown()
    {
        FindObjectOfType<AudioManager>().Play("SFX_Click02");
    }

    public void PlaySoundSlide()
    {
        FindObjectOfType<AudioManager>().Play("SFX_Slide");
    }
}
