using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


[System.Serializable]
public class Sound 
{
    public string name;

    //public Slider sliderAudio;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 1f;

    [Range(0f,3f)]
    public float pitch = 1f;

    public bool loop;

    public bool playOnAwake;


    [HideInInspector]
    public AudioSource source;

}
