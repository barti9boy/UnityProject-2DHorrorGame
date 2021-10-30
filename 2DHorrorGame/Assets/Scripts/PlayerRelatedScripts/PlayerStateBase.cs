using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerStateBase
{
    public Rigidbody2D rb;
    public PlayerInput playerInput;
    public InputManager inputManager;
    public Transform playerTransform;
    public GameObject flashlight;
    public GameObject playerGFX;
    public SpriteRenderer playerSpriteRenderer;
    public PlayerInventory playerInventory;

    public bool isFacingRight = true;
    public PlayerStateBase(GameObject playerObject)
    {

    }
    public abstract void EnterState(PlayerStateMachine player);
    public abstract void UpdateState(PlayerStateMachine player);
    public abstract void OnCollisionEnter(PlayerStateMachine player, Collision2D collision);
    public abstract void OnTriggerStay(PlayerStateMachine player, Collider2D collision);
}
