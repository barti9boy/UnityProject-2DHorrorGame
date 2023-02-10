using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyMusicTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayAmbient(Clip.lobbyMusic);

    }

}
