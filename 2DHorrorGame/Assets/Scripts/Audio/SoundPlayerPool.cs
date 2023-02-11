using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayerPool : MonoBehaviour
{
    public static SoundPlayerPool Instance { get; private set; }

    [SerializeField] private GameObject soundPlayerPrefab;
    [SerializeField] private GameObject musicPlayerPrefab;
    private List<SoundPlayer> pooledSoundPlayers;
    private List<MusicPlayer> pooledMusicPlayers;

    private int amountOFPooledSoundPlayers;
    private int amountOFPooledMusicPlayers;


    private void Awake() 
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Debug.Log("Instantiated pool");
            Instance = this;
        }
    }

    private void Start()
    {
        pooledSoundPlayers = new List<SoundPlayer>();
        pooledMusicPlayers = new List<MusicPlayer>();
    }

    public void WarmUp(int amountOfPooledItems)
    {
        for(int i = 0; i < amountOfPooledItems; i++)
        {
            InstantiateSoundPlayer();
            InstantiateMusicPlayer();
        }
    }

    private GameObject InstantiateSoundPlayer()  
    {
        var soundPlayer = Instantiate(soundPlayerPrefab); 
        pooledSoundPlayers.Add(soundPlayer.GetComponent<SoundPlayer>());
        soundPlayer.gameObject.SetActive(false);
        amountOFPooledSoundPlayers++;
        return soundPlayer;
    }

    private void InstantiateMusicPlayer()
    {
        var musicPlayer = Instantiate(musicPlayerPrefab);
        pooledMusicPlayers.Add(musicPlayer.GetComponent<MusicPlayer>());
        musicPlayer.gameObject.SetActive(false);
        amountOFPooledMusicPlayers++;
    }

    public SoundPlayer RequestSoundPlayer() //think about creating another instance if none is available
    {
        for(int i = 0; i < amountOFPooledSoundPlayers; i++)
        {
            if (!pooledSoundPlayers[i].gameObject.activeInHierarchy)
            {
                return pooledSoundPlayers[i];
            }
        }
        GameObject playerGameObject = InstantiateSoundPlayer();
        SoundPlayer newPlayer = playerGameObject.GetComponent<SoundPlayer>();
        return newPlayer;
    }

    public MusicPlayer RequestMusicPlayer()
    {
        for (int i = 0; i < amountOFPooledMusicPlayers; i++)
        {
            if (!pooledMusicPlayers[i].gameObject.activeInHierarchy)
            {
                return pooledMusicPlayers[i];
            }
        }
        return null;
    }

    public void MutePlayers(bool shouldMuted)
    {
        foreach(SoundPlayer player in pooledSoundPlayers)
        {
            player.MutePlayer(shouldMuted);
        }
        //same for musicPlayers
    }

    public void ReturnPlayer(AudioPlayer audioPlayer)
    {
        audioPlayer.gameObject.SetActive(false);
    }
}
