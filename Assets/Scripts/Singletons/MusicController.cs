using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class MusicController : Singleton<MusicController>
{
    public enum SoundEffects
    {
        Hit,
        Fire,
        Jump,
    }

    private Dictionary<SoundEffects, AudioClip> _soundsEffects;
    private const string FileExt = "";
    private float _backgroundVolume = 0.05f;
    private float _effectsVolume = 1f;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.name = "Background Music";
        _audioSource.loop = true;
        _audioSource.volume = _backgroundVolume;
        _soundsEffects = new Dictionary<SoundEffects, AudioClip>();
        LoadSoundClips();
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

    // Update is called once per frame

    public void PlaySound(SoundEffects soundEffects)
    {
        var soundToPlay = _soundsEffects[soundEffects];
        _audioSource.PlayOneShot(soundToPlay, _effectsVolume);
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
}