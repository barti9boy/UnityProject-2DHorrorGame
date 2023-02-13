using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : AudioPlayer
{
    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    private void Update()
    {
        //Check if clip is still playing, if not OnClipEnd
        //if (!source.isPlaying)
        //{
        //    OnClipEnd();
        //}
    }
    public void PlayGlobalMusic(Sound sound)//, SoundPlayerPool pool)
    {
        source.clip = sound.audioClip;
        source.Play();
    }

    public void MutePlayer(bool shouldBeMuted)
    {
        source.mute = shouldBeMuted;
    }

    private void OnClipEnd()
    {
        SoundPlayerPool.Instance.ReturnPlayer(this);
    }
}
