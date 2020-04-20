using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

[System.Serializable]
public class Sound
{
    public string name;
    public bool enabled = true;
    [Space]
    public AudioClip[] clips = new AudioClip[1];
    [Space]
    public bool playIfItsNotPlaying = false;
    [Space]
    public AudioMixerGroup group;
    [Space]
    public bool randomPitch = false;
    public Vector2 pitchValues = new Vector2(0.3f, 1.5f);
    [Space]

    [HideInInspector]
    public bool multipleClips = false;

    public float pitch = 1f;

    [HideInInspector]
    public AudioSource source;
}

public class AudioManager : MonoBehaviour
{

    public AudioMixer mixer;
    public Sound[] sounds;
    public static bool isAudioOn = true;

    //Çok basit bir singleton
    public static AudioManager main;

    public void Awake()
    {
        main = this;

        foreach (Sound s in sounds)//Bütün audio source'ları eklemesi
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.outputAudioMixerGroup = s.group;

            s.source.playOnAwake = false;

            if (s.clips.Length == 1)
                s.source.clip = s.clips[0];
            else if (s.clips.Length > 1)
            {
                s.multipleClips = true;
            }
            else
            {
                Debug.LogError("There is no clip!!!");
            }

            s.source.pitch = s.pitch;
        }

        //PLAYER PREFS

        if (!PlayerPrefs.HasKey("audio"))
        {
            PlayerPrefs.SetInt("audio", 1);
        }

        isAudioOn = PlayerPrefs.GetInt("audio") == 1;

        print("Is audio on: " + isAudioOn);


    }

    void Start()
    {
        SetAudio(isAudioOn);
    }

    public void SetAudio(bool on)
    {
        isAudioOn = on;

        if (on)
        {
            mixer.SetFloat("MasterVolume", 0);
            PlayerPrefs.SetInt("audio", 1);
        }
        else
        {
            mixer.SetFloat("MasterVolume", -80);
            PlayerPrefs.SetInt("audio", 0);
        }
    }

    //Çalması
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null || !s.enabled)
            return;

        if (s.playIfItsNotPlaying && s.source.isPlaying)
            return;

        if (s.multipleClips)
            s.source.clip = s.clips[Random.Range(0, s.clips.Length)];

        if (s.randomPitch)
            s.source.pitch = Random.Range(s.pitchValues.x, s.pitchValues.y);

        s.source.Play();
    }

    //Durdurması
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
            return;

        s.source.Stop();
    }

    float pitch = 1;

    void Update()
    {
        pitch = Mathf.Lerp(0.1f, 1, Time.timeScale);

        mixer.SetFloat("MasterPitch", pitch);

    }

    public void SetMusic(float volume, bool fast)
    {
        mixer.DOKill();

        if (fast)
            mixer.SetFloat("MusicVol", volume);
        else
            mixer.DOSetFloat("MusicVol", volume, 1);
    }
}
