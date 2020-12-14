using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class MusicController : Singleton<MusicController>
{
    public enum SoundEffects
    {
        MainMenu_DrunkenSailor,
        BGM,
        Hit,
        Fire,
        Jump,
        ChestOpen,
        Click, 
        Hover,
        Ground_1,
        Ground_2,
        Ground_3,
        Ground_4,
    }

    private Dictionary<SoundEffects, AudioClip> _soundsEffects;
    private const string FileExt = "";
    private float _backgroundVolume = 0.8f;
    private float _effectsVolume = .8f;
    public AudioSource _audioSource;

    // Start is called before the first frame update
    protected override void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.name = "Background Music";
        _audioSource.loop = true;
        _audioSource.volume = _backgroundVolume;
        
        _soundsEffects = new Dictionary<SoundEffects, AudioClip>();
        LoadSoundClips();
        _audioSource.clip = _soundsEffects[SoundEffects.MainMenu_DrunkenSailor];
        _audioSource.Play();
        
        base.Awake();
    }

    private void LoadSoundClips()
    {
        foreach (SoundEffects sound in Enum.GetValues(typeof(SoundEffects)))
        {
            string soundClipName = Enum.GetName(typeof(SoundEffects), sound);
            var audioClip = Resources.Load<AudioClip>(soundClipName + FileExt);
            _soundsEffects[sound] = audioClip;
        }
    }
    
    public void PlaySound(SoundEffects soundEffects, float volume = 1)
    {
        var soundToPlay = _soundsEffects[soundEffects];
        _audioSource.PlayOneShot(soundToPlay, volume);
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
    
    public void PlaySound(SoundEffects soundEffects, Vector3 position)
    {
        var soundToPlay = _soundsEffects[soundEffects];
        AudioSource.PlayClipAtPoint(soundToPlay, position, _effectsVolume);
    }

    public void ChangeSoundEffectsVolume(Slider slider)
    {
        _effectsVolume = slider.value;
    }

    public void PlayGameBGM()
    {
        var toPlay = _soundsEffects[SoundEffects.BGM];
        if (_audioSource.clip == toPlay) return;
        _audioSource.clip = toPlay;
        _audioSource.Play();
    }

    public void PlayMenuBGM()
    {
        var toPlay = _soundsEffects[SoundEffects.MainMenu_DrunkenSailor];
        if (_audioSource.clip == toPlay) return;
        _audioSource.clip = toPlay;
        _audioSource.Play();
    }
    
}