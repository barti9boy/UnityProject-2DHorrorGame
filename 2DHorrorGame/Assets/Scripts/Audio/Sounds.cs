using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Make scriptable object of sounds?
public struct Sound
{
    //enum
    public Clip clip;
    public AudioClip audioClip;
}

public enum Clip : byte
{
    //Ambient sounds 0-20
    horrorAmbient = 0,
    widnAmbient = 1,

    //PlayerSounds 21-50
    playerStep = 21,
    
    //MonsterSound 51-70
    monsterStep = 51,
    monsterChasing = 52,
    monsterConfused = 53,
    
    //ItemSounds 71-100
    KeyPickUp = 71,

}