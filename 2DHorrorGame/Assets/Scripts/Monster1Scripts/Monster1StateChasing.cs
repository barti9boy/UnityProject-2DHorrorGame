using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster1StateChasing : Monster1StateBase
{
    private float timer;
    private float playerTransformX;
    private bool playerhit;
    public Monster1StateChasing(GameObject monster1Object) : base(monster1Object)
    {
        rb = monster1Object.GetComponent<Rigidbody2D>();
        monster1Transform = monster1Object.GetComponent<Transform>();
        monster1GFX = monster1Object.transform.GetChild(0).gameObject;
        monster1SpriteRenderer = monster1GFX.GetComponent<SpriteRenderer>();
        rayStartTransform = monster1Object.transform.GetChild(2).transform;
    }
    public override void EnterState(Monster1StateMachine monster1, RaycastHit2D hitPlayer, Collider2D collision = null)
    {
        //Get player position and flip if needed
        //hitPlayer = monster1.previousState.hitPlayer;
        playerhit = false;
        playerTransformX = hitPlayer.transform.position.x;
        isFacingRight = monster1.previousState.isFacingRight;
        timer = 0;
        rb.velocity = new Vector2(0f, 0f);

        if (playerTransformX < monster1Transform.position.x) //Player is on the left 
        {
            if (isFacingRight)
            {
               // Flip();
            }
            movementDirection = -1;
        }
        if (playerTransformX > monster1Transform.position.x) //Player is on the right
        {
            if (!isFacingRight)
            {
              //  Flip();
            }
            movementDirection = 1;
        }
        //Play angry animation and wait for it to finish


    }
    public override void UpdateState(Monster1StateMachine monster1, RaycastHit2D hitPlayer, Collider2D collision = null)
    {

        if (!playerhit)
        {
            WaitBeforeChasing(monster1, hitPlayer, 2);

        }

        //Check if player is still in range, if not wait for a few seconds and come back to patrolling 
        if (hitPlayer.collider == null)
        {
            if (Mathf.Abs(monster1Transform.position.x - playerTransformX) < 1.5)
            {
                rb.velocity = new Vector2(0f, 0f);
                Debug.Log("LOST PLAYER");
                WaitUntilAnimated(monster1, hitPlayer, 5);
            }
        }

        else
        {
            //Check if position of the player is colse enough to play killing animation
            //playerTransformX = hitPlayer.transform.position.x;

            //if (Mathf.Abs(monster1Transform.position.x - playerTransformX) < 1)
            //{
            //    rb.velocity = new Vector2(0f, 0f);
            //    Debug.Log("KILLED PLAYER");
            //    WaitUntilAnimated(monster1, hitPlayer, 5);
            //}
            if (playerhit)
            {
                rb.velocity = new Vector2(0f, 0f);
            }
        }
    }
    public override void OnCollisionEnter(Monster1StateMachine monster1, Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            rb.velocity = new Vector2(0f, 0f);
            Debug.Log("KILLED PLAYER");
            Debug.Log(rb.velocity);
            playerhit = true;
            Debug.Log(collision.gameObject.GetComponent<PlayerStateMachine>().currentState); //U¯YÆ DO WYKRYWANIA CZY SIÊ CHOWA
        }
    }
    public override void OnTriggerStay(Monster1StateMachine monster1, Collider2D collision)
    {

    }
    public void Flip()
    {
        monster1Transform.Rotate(0.0f, 180.0f, 0.0f);
        isFacingRight = !isFacingRight;
    }
    public void WaitUntilAnimated(Monster1StateMachine monster1, RaycastHit2D hitPlayer, float timeOfAnimation)
    {
        rb.velocity = new Vector2(0f, 0f);
        timer += Time.deltaTime;
        if (timer >= timeOfAnimation)
        {
            monster1.previousState = this;
            monster1.SwitchState(monster1.idleState, hitPlayer);
        }
    }
    public void WaitBeforeChasing(Monster1StateMachine monster1, RaycastHit2D hitPlayer, float timeOfAnimation)
    {
        timer += Time.deltaTime;
        if (timer >= timeOfAnimation)
        {
            //Play chasing animation and start running
            rb.velocity = new Vector2(movementDirection * movementSpeed * 2, 0);
        }
    }
}
