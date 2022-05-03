using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSwitchVents : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera playerCamera1;
    [SerializeField] CinemachineVirtualCamera playerCamera2;
    [SerializeField] float middleOfTheTriggger;
    [SerializeField] GameObject monster;
    private Monster1StateMachine monsterScript;


    private void Awake()
    {
        middleOfTheTriggger = GetComponent<Transform>().position.y;
        if (monster != null)
        {
            monsterScript = monster.GetComponent<Monster1StateMachine>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.transform.position.y > middleOfTheTriggger)
            {
                playerCamera1.Priority = 0;
                playerCamera2.Priority = 1;
            }
            if (collision.transform.position.y < middleOfTheTriggger)
            {
                playerCamera1.Priority = 1;
                playerCamera2.Priority = 0;
            }
            if (monster != null)
            {
                if (monster.activeSelf == false)
                {
                    monster.SetActive(true);
                }
                monsterScript.isLookingForAPlayer = true;
            }
        }
    }

}