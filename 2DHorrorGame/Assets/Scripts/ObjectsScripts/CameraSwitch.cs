using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera playerCamera1;
    [SerializeField] CinemachineVirtualCamera playerCamera2;
    [SerializeField] float middleOfTheTriggger;

    private void Awake()
    {
        middleOfTheTriggger = GetComponent<Transform>().position.x;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.transform.position.x < middleOfTheTriggger)
            {
                playerCamera1.Priority = 0;
                playerCamera2.Priority = 1;
            }
            if (collision.transform.position.x > middleOfTheTriggger)
            {
                playerCamera1.Priority = 1;
                playerCamera2.Priority = 0;
            }
        }
    }

}
