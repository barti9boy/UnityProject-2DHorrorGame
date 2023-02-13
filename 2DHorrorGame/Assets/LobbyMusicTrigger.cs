using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyMusicTrigger : MonoBehaviour
{
    // Start is called before the first frame update

    bool m_IsPlaying = false;

    void Update()
    {
        if(AudioManager.Instance != null && !m_IsPlaying)
        {
            AudioManager.Instance.PlayAmbient(Clip.lobbyMusic);
            m_IsPlaying = true;
        }
    }

}
