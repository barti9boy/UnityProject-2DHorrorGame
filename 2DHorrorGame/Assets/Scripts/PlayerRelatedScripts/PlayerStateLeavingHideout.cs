using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerActions;
using System;
using Photon.Pun;

public class PlayerStateLeavingHideout : PlayerStateBase
{
    Collider2D hideoutCollider;
    PlayerStateMachine playerStateMachine;

    //hideout variables
    private float hideoutEntrence;
    private HideoutScript hideout;


    //hiding phases variables
    private bool isHidden;
    private float timer;
    private string hideoutTag;


    //player GFX events
    public event EventHandler OnLeaveStateHiding;
    public event EventHandler OnTurnOffFurnitureTag;


    //Photon
    private PhotonView photonView;

    public PlayerStateLeavingHideout(GameObject playerObject) : base(playerObject)
    {
        rb = playerObject.GetComponent<Rigidbody2D>();
        playerInput = playerObject.GetComponent<PlayerInput>();
        inputManager = playerObject.GetComponent<InputManager>();
        playerTransform = playerObject.GetComponent<Transform>();
        flashlight = playerObject.transform.GetChild(1).gameObject;
        playerGFX = playerObject.transform.GetChild(0).gameObject;
        playerSpriteRenderer = playerGFX.GetComponent<SpriteRenderer>();
        playerInventory = playerObject.GetComponent<PlayerInventory>();
    }
    public override void EnterState(PlayerStateMachine player, Collider2D collision = null)
    {
        playerStateMachine = player;
        photonView = playerStateMachine.gameObject.GetComponent<PhotonView>();
        hideoutCollider = collision;
        hideoutTag = collision.tag;
        timer = 0;

        hideout = collision.GetComponent<HideoutScript>();

        playerSpriteRenderer.sortingOrder = 1;
        flashlight.GetComponent<SpriteRenderer>().sortingOrder = 1;
        OnLeaveStateHiding?.Invoke(this, EventArgs.Empty);

        if(player.photonView.IsMine)
            hideout.PlayLeaveAnim();
        
        CoroutineHandler.Instance.StartCoroutine(CoroutineHandler.Instance.WaitUntilAnimated(hideout.hiding.length, () => Leave(player)));
    }

    public override void UpdateState(PlayerStateMachine player, Collider2D collision = null)
    {
 //       WaitUntilAnimated(player);
    }

    //public void WaitUntilAnimated(PlayerStateMachine player)
    //{
    //    timer += Time.deltaTime;

    //    if (timer >= hidingAnimation.length)
    //    {
    //        if(photonView.IsMine)
    //        {
    //            photonView.RPC("RPC_Leave", RpcTarget.Others, photonView.ViewID);
    //            Debug.Log("Leave RPC sent");
    //        }
    //        Leave(player);
    //    }
    //}
    //[PunRPC]
    //private void RPC_Leave(int viewId)
    //{
    //    if (viewId == photonView.ViewID)
    //    {
    //        Debug.Log("Leave RPC recieved");
    //        Leave(playerStateMachine);
    //    }
    //}
    
    private void Leave(PlayerStateMachine player)
    {
        Debug.Log("Player left hideout");
        inputManager.isInteractionButtonClicked = false;
        flashlight.transform.Rotate(0.0f, 0.0f, 90.0f);
        if (player.isFacingRight)
        {
            flashlight.transform.position = new Vector3(playerTransform.position.x + 0.375f, playerTransform.position.y - 0.5f, playerTransform.position.z);
        }
        else if (!player.isFacingRight)
        {
            flashlight.transform.position = new Vector3(playerTransform.position.x - 0.375f, playerTransform.position.y - 0.5f, playerTransform.position.z);
        }
        OnTurnOffFurnitureTag?.Invoke(this, EventArgs.Empty);
        inputManager.movementInputEnabled = true;
        player.previousState = States.leavingHideout;
        player.SwitchState(States.idle);
        isHidden = false;
    }

    public override void OnCollisionEnter(PlayerStateMachine player, Collision2D collision)
    {
        if (collision.collider.tag == "Monster")
        {
            inputManager.movementInputEnabled = true;
            player.SwitchState(States.dead);
        }
    }
    public override void OnTriggerStay(PlayerStateMachine player, Collider2D collision)
    {

    }
}