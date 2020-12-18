using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource _musicSource;
    public AudioSource _soundEffectSource;

    public AudioClip[] musicClips;
    public AudioClip[] soundEffectClips;

    public void PlayMusic (int index)
    {
        if (!GameConstants.isSoundEnabled)
            return;

        if (!_musicSource.isPlaying)
        {
            _musicSource.clip = musicClips[index];
            _musicSource.Play ();
        }
        else
        {
            Debug.LogError ("Music is currently playing. Please stop the music to play another.");
        }
    }

    public void StopMusic ()
    {
        _musicSource.Stop ();
    }

    public void PlaySoundEffectOneShot (int index)
    {
        if (!GameConstants.isSoundEnabled)
            return;

        _soundEffectSource.PlayOneShot (soundEffectClips[index]);
    }
}