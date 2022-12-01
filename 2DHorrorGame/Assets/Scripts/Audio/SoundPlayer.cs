using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : AudioPlayer
{
    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    private void Update()
    {
        //Check if clip is still playing, if not OnClipEnd
        if (!source.isPlaying)
        {
            OnClipEnd();
        }
    }
    public void PlayOnPosition(Sound sound, Vector3 position)//, SoundPlayerPool pool)
    {
        transform.position = position;
        source.PlayOneShot(sound.audioClip);
    }

    public void PlayOnParent(Sound sound, Transform parent)//, SoundPlayerPool pool)
    {
        transform.parent = parent;
        transform.localPosition = Vector3.zero;
        source.PlayOneShot(sound.audioClip);
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
