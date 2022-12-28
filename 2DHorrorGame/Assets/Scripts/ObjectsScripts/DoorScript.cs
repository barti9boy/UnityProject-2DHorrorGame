using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DoorScript : MonoBehaviour, IInteractible
{
    public static Action<int> OnDoorUnlocked;
    public bool isLocked;
    public bool isOpened;
    public bool isChangingRoom = false;
    public int velocityDirection;
    public AnimationClip doorOpeningAnimation;
    public GameObject interactionHighlight;

    private readonly string lockedText = "hold \"E\" to unlock";
    private readonly string unlockedText = "press \"E\" to enter";

    private InteractionHighlight highlight;
    public float interactionTime;
    public float unlockTimeRequired;
    public int itemIdToUnlock;
    //[SerializeField] private Transform rightPoint;
    //[SerializeField] private Transform leftPoint;
    private Transform unlockingCanvasImageTransform;
    private Transform unlockingCanvasBackgroundTransform;

    private Collider2D doorCollider;
    private string doorTag;

    private Animator animator;
    private int animOpenDoor;
    private int animCloseDoor;

    private PhotonView photonView;


    private void Awake()
    {
        highlight = GetComponentInChildren<InteractionHighlight>();
        photonView = GetComponent<PhotonView>();
        animator = GetComponent<Animator>();
        doorCollider = gameObject.transform.GetChild(0).GetComponent<Collider2D>();
        if (!isLocked) isOpened = false;
        unlockingCanvasImageTransform = gameObject.transform.GetChild(3).GetChild(0);
        unlockingCanvasBackgroundTransform = gameObject.transform.GetChild(3).GetChild(1);
    }
    private void Start()
    {
        if (isLocked)
            highlight.InteractionText.text = lockedText;
        else
            highlight.InteractionText.text = unlockedText;

        AssignAnimationIDs();
    }

    private void AssignAnimationIDs()
    {
        animOpenDoor = Animator.StringToHash("TriggerOpenDoor");
        animCloseDoor = Animator.StringToHash("TriggerCloseDoor");
    }
    public void PlayOpenDoorAnim()
    {
        animator.SetTrigger(animOpenDoor);
        animator.ResetTrigger(animCloseDoor);
        photonView.RPC("RPC_PlayOpenDoorAnim", RpcTarget.Others, photonView.ViewID);
        Debug.Log("PlayOpenDoorAnim RPC sent");
    }
    public void PlayCloseDoorAnim()
    {
        animator.SetTrigger(animCloseDoor);
        animator.ResetTrigger(animOpenDoor);
        photonView.RPC("RPC_PlayOpenDoorAnim", RpcTarget.Others, photonView.ViewID);
        Debug.Log("PlayOpenDoorAnim RPC sent");
    }

    [PunRPC]
    private void RPC_PlayOpenDoorAnim(int viewID)
    {
        if (photonView.ViewID == viewID)
        {
            animator.SetTrigger(animOpenDoor);
            animator.ResetTrigger(animCloseDoor);
            Debug.Log("PlayOpenDoorAnim RPC recieved");
        }
    }
    [PunRPC]
    private void RPC_PlayCloseDoorAnim(int viewID)
    {
        if (photonView.ViewID == viewID)
        {
            animator.SetTrigger(animCloseDoor);
            animator.ResetTrigger(animOpenDoor);
            Debug.Log("PlayCloseDoorAnim RPC recieved");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            interactionTime = 0;
        }
    }
    public void DoorUnlock(IPickableObject[] items, bool isInteractionButtonHeld)
    {
        if (isInteractionButtonHeld)
        {
            int slotPosition = 0; ;
            foreach (IPickableObject item in items) //numer itemu w items
            {
                if (item != null && item.ItemID == itemIdToUnlock)
                {
                    interactionTime += Time.deltaTime;
                    if (interactionTime >= unlockTimeRequired)
                    {
                        highlight.InteractionText.text = unlockedText;
                        isLocked = false;
                        OnDoorUnlocked?.Invoke(slotPosition);
                    }  
                }
                slotPosition++;
            }
        }
        else
        {
            interactionTime = 0;
        }
    }
    public void ChangeInteractionCanvasTransform(float playerX, float doorX)
    {
        if(doorTag  == "vDoor")
        {
            unlockingCanvasImageTransform.localPosition = new Vector3(0, 0, 0);
            unlockingCanvasBackgroundTransform.localPosition = new Vector3(0, 0, 0);
        }
        else if(doorTag == "Doors")
        { 
            if (playerX > doorX)
            {
                unlockingCanvasImageTransform.localPosition = new Vector3(0.75f, 0, 0);
                unlockingCanvasBackgroundTransform.localPosition = new Vector3(0.75f, 0, 0);
            }
            else
            {
                unlockingCanvasImageTransform.localPosition = new Vector3(-0.75f, 0, 0);
                unlockingCanvasBackgroundTransform.localPosition = new Vector3(-0.75f, 0, 0);
            }
        }
    }

    public void EnableInteractionHighlight()
    {
        interactionHighlight.SetActive(true);
    }

    public void DisableInteractionHighlight()
    {
        interactionHighlight.SetActive(false);
    }
}
