using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using Photon.Pun;


public class HideoutScript : MonoBehaviour
{
    /*private Rigidbody2D rb;
    private PlayerInput playerInput;
    private InputManager inputManager;
    private Transform playerTransform;
    private GameObject flashlight;
    private GameObject playerGFX;
    private SpriteRenderer playerSpriteRenderer;
    private Collider2D collider;*/
    public bool finishedOpening = false;
    private Animator animator;
    [SerializeField] public Transform handle;
    [SerializeField] public AnimationClip hiding;

    private PhotonView photonView;
    private int animIDHiding;
    private int animIDLeaving;

    private bool isTaken;
    public bool IsTaken { get => isTaken; set => isTaken = value; }


    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        AssignAnimationIDs();
    }

    private void AssignAnimationIDs()
    {
        animIDHiding = Animator.StringToHash("TriggerHide");
        animIDLeaving = Animator.StringToHash("TriggerLeave");
    }

    public void PlayHidingAnim()
    {
        AudioManager.Instance.PlaySoundAtPosition(Clip.monsterChasing, transform.position);
        isTaken = true;
        animator.SetTrigger(animIDHiding);
        animator.ResetTrigger(animIDLeaving);
        photonView.RPC("RPC_PlayHidingAnim", RpcTarget.Others, photonView.ViewID);
        Debug.Log("PlayHidingAnim RPC sent");
    }

    public void PlayLeaveAnim()
    {
        isTaken = false;
        animator.SetTrigger(animIDLeaving);
        animator.ResetTrigger(animIDHiding);
        photonView.RPC("RPC_PlayLeaveAnim", RpcTarget.Others, photonView.ViewID);
        Debug.Log("PlayLeaveAnim RPC sent");
    }



    [PunRPC]
    private void RPC_PlayHidingAnim(int viewID)
    {
        if (photonView.ViewID == viewID)
        {
            isTaken = true;
            animator.SetTrigger(animIDHiding);
            animator.ResetTrigger(animIDLeaving);
            Debug.Log("PlayHidingAnim RPC recieved");
        }
    }

    [PunRPC]
    private void RPC_PlayLeaveAnim(int viewID)
    {
        if (photonView.ViewID == viewID)
        {
            isTaken = false;
            animator.SetTrigger(animIDLeaving);
            animator.ResetTrigger(animIDHiding);
            Debug.Log("PlayLeaveAnim RPC recieved");
        }
    }



    /* private void Awake()
     {
         playerObject.GetComponent<PlayerStateMachine>().tryingToHideState.OnEnterStateHiding += TryingToHideState_OnEnterStateHiding; ;

     }

     private void TryingToHideState_OnEnterStateHiding(object sender, EventArgs e)
     {
         WaitForHidingTime();
         playerObject.GetComponent<PlayerStateMachine>().tryingToHideState.isHiding = true;
         Debug.Log(playerObject.GetComponent<PlayerStateMachine>().tryingToHideState.isHiding);
     }

     private IEnumerator WaitForHidingTime()
     {
         yield return new WaitForSeconds(hiding.length);
         finishedOpening = true;
     } /*


    /*public bool isHiding = false;
    public bool isHidden = false;
    public bool isTryingToHide = false;
    public int velocityDirection;
    [SerializeField] private float movementSpeed = 5.0f;

    public event EventHandler OnEnterStateHiding;
    public event EventHandler OnLeaveStateHiding;

    void Awake()
    {
        rb = playerObject.GetComponent<Rigidbody2D>();
        playerInput = playerObject.GetComponent<PlayerInput>();
        inputManager = playerObject.GetComponent<InputManager>();
        playerTransform = playerObject.GetComponent<Transform>();
        flashlight = playerObject.transform.GetChild(1).gameObject;
        playerGFX = playerObject.transform.GetChild(0).gameObject;
        playerSpriteRenderer = playerGFX.GetComponent<SpriteRenderer>();
        playerStateMachine = playerObject.GetComponent<PlayerStateMachine>();
        animator = gameObject.GetComponent<Animator>();
    }*/

    /* void Update()
     {
       if (isTryingToHide)
        {
            MovePlayer();
        }
    }

    public void CheckHideout(PlayerStateMachine player)
    {
        inputManager.movementInputEnabled = false;
        inputManager.movementInputDirection = 0;
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);

        if (isHidden == false)
        {
            StartHiding(player);
        }
        else if (isHidden == true)
        {
            StartLeaving(player);
        }
    }

    public void StartHiding(PlayerStateMachine player)
    {

        if (handle.transform.position.x > playerTransform.position.x) //jesteœmy po lewej
        {
            if (!player.currentState.isFacingRight)
            {
                playerTransform.Rotate(0, 180, 0);
                player.currentState.isFacingRight = !player.currentState.isFacingRight;
                Debug.Log(player.currentState.isFacingRight);
                player.currentState.inputManager.isInteractionButtonClicked = false;
            }
            velocityDirection = 1;
        }
        else if (handle.transform.position.x < playerTransform.position.x) // jesteœmy po prawej
        {
            if (player.currentState.isFacingRight)
            {
                playerTransform.Rotate(0, 180, 0);
                player.currentState.isFacingRight = !player.currentState.isFacingRight;
                Debug.Log(player.currentState.isFacingRight);

                player.currentState.inputManager.isInteractionButtonClicked = false;
            }
            velocityDirection = -1;
        }
        isTryingToHide = true;
    }


    public void Hide()
    {
        playerSpriteRenderer.sortingOrder = -7;
        flashlight.GetComponent<SpriteRenderer>().sortingOrder = -7;
        flashlight.transform.Rotate(0.0f, 0.0f, -90.0f);
        flashlight.transform.position = playerTransform.position;
        playerStateMachine.SwitchState(playerStateMachine.hidingState);
        animator.SetBool("isHiding", false);
        animator.SetBool("isHidden", true);
        isHidden = true;

    }
    public void MovePlayer()
    {
        if (velocityDirection == 1 && playerTransform.position.x < handle.position.x)
        {
            rb.velocity = new Vector2(velocityDirection * movementSpeed, 0);
            if (Math.Abs(playerTransform.position.x - handle.transform.position.x) < 0.1)
            {
                animator.SetBool("isHiding", true);
                isTryingToHide = false;
                inputManager.interactionInputEnabled = true;
                OnEnterStateHiding?.Invoke(this, EventArgs.Empty);
                rb.velocity = new Vector2(0, 0);
                StartCoroutine(WaitForHidingTime());

            }
        }
        if (velocityDirection == -1 && playerTransform.position.x > handle.position.x)
        {
            rb.velocity = new Vector2(velocityDirection * movementSpeed, 0);
            if (Math.Abs(playerTransform.position.x - handle.transform.position.x) < 0.1)
            {
                animator.SetBool("isHiding", true);
                isTryingToHide = false;
                inputManager.interactionInputEnabled = true;
                OnEnterStateHiding?.Invoke(this, EventArgs.Empty);
                rb.velocity = new Vector2(0, 0);
                StartCoroutine(WaitForHidingTime());
            }
        }
    }




    private IEnumerator WaitForLeavingTime()
    {
        yield return new WaitForSeconds(hiding.length);
        Leave();
    }

    public void StartLeaving(PlayerStateMachine player)
    {
        animator.SetBool("isHidden", false);
        animator.SetBool("isLeaving", true);
        playerSpriteRenderer.sortingOrder = 1;
        flashlight.GetComponent<SpriteRenderer>().sortingOrder = 1;
        OnLeaveStateHiding?.Invoke(this, EventArgs.Empty);
        StartCoroutine(WaitForLeavingTime()); */



    /* inputManager.isInteractionButtonClicked = false;
     flashlight.transform.Rotate(0.0f, 0.0f, 90.0f);
     if (player.currentState.isFacingRight)
     {
         flashlight.transform.position = new Vector3(playerTransform.position.x + 0.2f, playerTransform.position.y, playerTransform.position.z);
     }
     else if (!player.currentState.isFacingRight)
     {
         flashlight.transform.position = new Vector3(playerTransform.position.x - 0.2f, playerTransform.position.y, playerTransform.position.z);
     }
     playerSpriteRenderer.sortingOrder = 1;
     flashlight.GetComponent<SpriteRenderer>().sortingOrder = 1;
     inputManager.movementInputEnabled = true;
     player.SwitchState(player.idleState);
     isHidden = false;
     animator.SetBool("isLeaving", false); 
}*/

    /* public void Leave()
     {
         inputManager.isInteractionButtonClicked = false;
         flashlight.transform.Rotate(0.0f, 0.0f, 90.0f);
         if (playerStateMachine.currentState.isFacingRight)
         {
             flashlight.transform.position = new Vector3(playerTransform.position.x + 0.2f, playerTransform.position.y, playerTransform.position.z);
         }
         else if (!playerStateMachine.currentState.isFacingRight)
         {
             flashlight.transform.position = new Vector3(playerTransform.position.x - 0.2f, playerTransform.position.y, playerTransform.position.z);
         }
         inputManager.movementInputEnabled = true;
         playerStateMachine.SwitchState(playerStateMachine.idleState);
         isHidden = false;
         animator.SetBool("isLeaving", false);
     }*/
}
