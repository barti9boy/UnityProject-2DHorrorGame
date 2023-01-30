using System;
using UnityEngine.Audio;
using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    public Sound[] soundsArray;

    public static AudioManager Instance { get; private set; }

    public static Action<Clip, Vector3> OnPlaySoundAtPosition;

    public static Action<Clip, Transform> OnPlaySoundAtParent;

    public static Action<Clip> OnPlayAmbient;

    public static Action<bool> OnToggleMute;

    private SoundPlayerPool pool;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        OnPlaySoundAtPosition += PlaySoundAtPosition;
        OnPlaySoundAtParent += PlaySoundAtParent;
        OnPlayAmbient += PlayAmbient;
        OnToggleMute += Mute;
    }
    void Start()
    {
        pool = SoundPlayerPool.Instance;
        pool.WarmUp(3);
    }

    private void OnDestroy()
    {
        OnPlaySoundAtPosition -= PlaySoundAtPosition;
        OnPlaySoundAtParent -= PlaySoundAtParent;
        OnPlayAmbient -= PlayAmbient;
        OnToggleMute -= Mute;
    }

    public void PlaySoundAtPosition(Clip soundName, Vector3 position)
    {
        SoundPlayer player = pool.RequestSoundPlayer();
        player.gameObject.SetActive(true);
        Sound sound = FindSound(soundName);
        player.PlayOnPosition(sound, position);
    }

    public void PlaySoundAtParent(Clip soundName, Transform parent)
    {
        SoundPlayer player = pool.RequestSoundPlayer();
        Sound sound = FindSound(soundName);
        player.PlayOnParent(sound, parent);
    }

    public void PlayAmbient(Clip soundName) 
    {

    }

    public void Mute(bool shouldBeMuted)
    {
        pool.MutePlayers(shouldBeMuted);
    }

    public Sound FindSound(Clip clipName)
    {
        for(int i = 0; i < soundsArray.Length; i++)
        {
            if (soundsArray[i].clip == clipName)
            {
                return soundsArray[i];
            }

        }
        return null;
    }

    
}
