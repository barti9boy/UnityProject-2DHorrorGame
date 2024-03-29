using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Make scriptable object of sounds?
[CreateAssetMenu(fileName = "New Sound", menuName = "SoundData")]
public class Sound : ScriptableObject
{
    //enum
    public Clip clip;
    public AudioClip audioClip;
}

public enum Clip : byte
{
    //Ambient sounds 0-20
    lobbyMusic = 0,
    widnAmbient = 1,

    //PlayerSounds 21-50
    doorOpen = 21,
    doorClose = 22,
    hideoutOpen = 23,
    hideoutClose = 24,
    ventEnter = 25,
    ventExit = 26,
    pressurePlate = 27,
    doorCreek = 28,


    //MonsterSound 51-70
    monsterStep = 51,
    monsterChasing = 52,
    monsterConfused = 53,
    
    //ItemSounds 71-100
    ItemPickup = 71,
    UnlockingDoors = 72,
    ItemDrop = 73,
    NotePickUp = 74

}