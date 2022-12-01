using System;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //make it singleton
    public static AudioManager Instance { get; private set; }

    public static Action<Sound, Vector3> OnPlaySoundAtPosition;

    public static Action<Sound, Transform> OnPlaySoundAtParent;

    public static Action<Sound> OnPlayAmbient;

    public static Action<bool> OnToggleMute;

    private SoundPlayerPool pool;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Instance = this;

        }
        else
        {
            Destroy(this);
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

    private void PlaySoundAtPosition(Sound sound, Vector3 position)
    {
        SoundPlayer player = pool.RequestSoundPlayer();
        player.PlayOnPosition(sound, position);
    }

    private void PlaySoundAtParent(Sound sound, Transform parent)
    {
        SoundPlayer player = pool.RequestSoundPlayer();
        player.PlayOnParent(sound, parent);
    }

    private void PlayAmbient(Sound sound) 
    {

    }

    private void Mute(bool shouldBeMuted)
    {
        pool.MutePlayers(shouldBeMuted);
    }
}
