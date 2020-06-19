using System;
using UnityEngine;
using UnityEngine.UI;

public class MusiqueManager : MonoBehaviour
{
    public Toggle _toggleWhichChanges;
    int _volumeToggle = 1;
    public Sound[] musics;
    public static MusiqueManager Instance { get; private set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
            Toggle[] toggles = Resources.FindObjectsOfTypeAll<Toggle>();

            Debug.Log(toggles.Length);
            for (int i = 0; i < toggles.Length; i++)
            {
                if (toggles[i].name == "ToggleMusic")
                {
                    Instance._toggleWhichChanges = toggles[i];
                    Debug.Log("qmlkdfjqmlsdkf");
                    break;
                }
            }

            ChangeToggle();
            //Instance = this;
            //DontDestroyOnLoad(this);
        }
    }
    void Start()
    {
        foreach (Sound s in Instance.musics)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            //s.source.volume = GameObject.Find("SliderAudio").GetComponent<Slider>().value;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.playOnAwake;
            s.source.volume = s.volume;
        }
        SetVolume();
        PlayOnAwake();
    }
    void Update()
    {
     
    }

    void PlayOnAwake()
    {
        for (int i = 0; i < Instance.musics.Length; i++)
        {
            if (Instance.musics[i].playOnAwake)
            {
               Instance.Play(musics[i].name);
            }
        }
    }

    public void Play(string name)
    {
        if(Instance._volumeToggle == 1)
        {
            Sound s = Array.Find(Instance.musics, sound => sound.name == name);
            if (s == null)
                return;
            s.source.Play();
        }
    }

    public void Stop(string name)
    {
       
            Sound s = Array.Find(Instance.musics, sound => sound.name == name);
            if (s == null)
                return;
            s.source.Stop();
        
    }


    public void ChangeToggle()
    {
        if (Instance._toggleWhichChanges.isOn)
        {
            Instance._volumeToggle = 1;
            FindObjectOfType<AudioManager>().Play("SFX_Toggle01");
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("SFX_Toggle01");
            Instance._volumeToggle = 0;
        }
        Instance.SetVolume();
    }

    public void SetVolume()
    {
        foreach (Sound s in Instance.musics)
        {
            if(Instance._volumeToggle==0)
            s.source.volume = Instance._volumeToggle;
            else
                s.source.volume = s.volume;
        }
    }
}