﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class MusicController : Singleton<MusicController>
{
    public enum SoundEffects
    {
        MainMenu_DrunkenSailor,
        BGM,
        Hit,
        Fire,
        Jump,
        Click, 
        Hover,
        Ground_1,
        Ground_2,
        Ground_3,
        Ground_4,
        Victory,
        Jump_1,
        Jump_2,
        Jump_3,
        Jump_4,
    }
    
    public float BGMSilenceFactor = 2f;
    
    private Dictionary<SoundEffects, AudioClip> soundsEffects;
    private const string FileExt = "";
    private readonly float backgroundVolume = 0.8f;
    private float effectsVolume = .8f;
    public AudioSource BGMaudioSource;
    private AudioSource SFXAudioSource;
    

    // Start is called before the first frame update
    protected override void Awake()
    {
        BGMaudioSource = gameObject.AddComponent<AudioSource>();
        SFXAudioSource = gameObject.AddComponent<AudioSource>();
        
        BGMaudioSource.loop = true;
        BGMaudioSource.volume = backgroundVolume / BGMSilenceFactor;
        SFXAudioSource.volume = effectsVolume;
        
        soundsEffects = new Dictionary<SoundEffects, AudioClip>();
        LoadSoundClips();
        BGMaudioSource.clip = soundsEffects[SoundEffects.MainMenu_DrunkenSailor];
        BGMaudioSource.Play();
        
        base.Awake();
    }

    private void LoadSoundClips()
    {
        foreach (SoundEffects sound in Enum.GetValues(typeof(SoundEffects)))
        {
            string soundClipName = Enum.GetName(typeof(SoundEffects), sound);
            var audioClip = Resources.Load<AudioClip>(soundClipName + FileExt);
            soundsEffects[sound] = audioClip;
        }
    }
    
    public void PlaySound(SoundEffects soundEffects, float volume = 1)
    {
        var soundToPlay = soundsEffects[soundEffects];
        SFXAudioSource.PlayOneShot(soundToPlay, volume);
    }

    public void PlaySound(string soundName, float volume = 1)
    {
        if (Enum.TryParse(soundName, true, out SoundEffects sound))
        {
            PlaySound(sound, volume);
        }
        else
        {
            Debug.LogWarning("The sound " + soundName + " was not found!");
        }

    }

    public void SetSFXVolume(Slider slider)
    {
        SFXAudioSource.volume = slider.value;
    }

    public void SetBGMVolume(Slider slider)
    {
        BGMaudioSource.volume = slider.value / BGMSilenceFactor;
    }

    public void PlayGameBGM()
    {
        var toPlay = soundsEffects[SoundEffects.BGM];
        if (BGMaudioSource.clip == toPlay) return;
        BGMaudioSource.clip = toPlay;
        BGMaudioSource.Play();
    }

    public void PlayMenuBGM()
    {
        var toPlay = soundsEffects[SoundEffects.MainMenu_DrunkenSailor];
        if (BGMaudioSource.clip == toPlay) return;
        BGMaudioSource.clip = toPlay;
        BGMaudioSource.Play();
    }

    public float GetSFXVolume()
    {
        return SFXAudioSource.volume;
    }

    public float GetBGMVolume()
    {
        return BGMaudioSource.volume * BGMSilenceFactor;
    }
    
}