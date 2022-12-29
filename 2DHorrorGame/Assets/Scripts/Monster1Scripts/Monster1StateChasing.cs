using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster1StateChasing : Monster1StateBase
{
    private float timer1, timer2, timer3;
    private float playerTransformX;
    private bool playerCollision;
    private bool doorHit;
    private bool waited;
    public event EventHandler OnEnterStateChasing;
    public event EventHandler OnExitStateChasing;
    public Monster1StateChasing(GameObject monster1Object) : base(monster1Object)
    {
        rb = monster1Object.GetComponent<Rigidbody2D>();
        monster1Transform = monster1Object.GetComponent<Transform>();
        monster1GFX = monster1Object.transform.GetChild(0).gameObject;
        monster1SpriteRenderer = monster1GFX.GetComponent<SpriteRenderer>();
        rayStartTransform = monster1Object.transform.GetChild(2).transform;
    }
    public override void EnterState(Monster1StateMachine monster1, Collider2D collision = null)
    {
        //Get player position and flip if needed
        //hitPlayer = monster1.previousState.hitPlayer;
        waited = false;
        playerCollision = false;
        doorHit = false;
        playerTransformX = monster1.hitPlayer.transform.position.x;
        isFacingRight = monster1.previousState.isFacingRight;
        timer1 = 0;
        timer2 = 0;
        timer3 = 0;
        rb.velocity = new Vector2(0f, 0f);
        OnExitStateChasing?.Invoke(this, EventArgs.Empty);

        if (playerTransformX < monster1Transform.position.x) //Player is on the left 
        {
            if (isFacingRight)
            {
                Flip();
            }
            movementDirection = -1;
        }
        if (playerTransformX > monster1Transform.position.x) //Player is on the right
        {
            if (!isFacingRight)
            {
                Flip();
            }
            movementDirection = 1;
        }
        //Play angry animation and wait for it to finish


    }
    public override void UpdateState(Monster1StateMachine monster1, Collider2D collision = null)
    {
        if (!waited)
        {
            WaitBeforeChasing(monster1, monster1.hitPlayer, 2);
        }


        //Check if player is still in range, if not wait for a few seconds and come back to patrolling 
        if (!monster1.hitPlayer && waited)
        {
            //rb.velocity = new Vector2(movementDirection * movementSpeed * 1.8f, 0);
            WaitBeforeStopping(monster1, monster1.hitPlayer, 1.5f);
            WaitUntilAnimated(monster1, monster1.hitPlayer, 4.5f);

        }

    }
    public override void OnCollisionEnter(Monster1StateMachine monster1, Collision2D collision)
    {
        if (collision.collider.tag == "Doors")
        {
            rb.velocity = new Vector2(0f, 0f);
        }
        else if (collision.collider.tag == "Player")
        {
            rb.velocity = new Vector2(0f, 0f);
 
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
        timer1 += Time.deltaTime;
        if (timer1 >= timeOfAnimation)
        {
            monster1.previousState = this;
            monster1.SwitchState(MonsterStates.idle);
            timer1 = 0;
        }
    }
    public void WaitBeforeChasing(Monster1StateMachine monster1, RaycastHit2D hitPlayer, float timeOfAnimation)
    {
        timer2 += Time.deltaTime;
        if (timer2 >= timeOfAnimation)
        {
            //Play chasing animation and start running
            OnEnterStateChasing?.Invoke(this, EventArgs.Empty);
            rb.velocity = new Vector2(movementDirection * movementSpeed * 1.8f, 0);
            waited = true;
            timer2 = 0;
        }
    }
    public void WaitBeforeStopping(Monster1StateMachine monster1, RaycastHit2D hitPlayer, float timeOfAnimation)
    {
        timer3 += Time.deltaTime;
        if (timer3 >= timeOfAnimation)
        {
            //Play chasing animation and start running
            rb.velocity = new Vector2(0.0f, 0.0f);
            OnExitStateChasing?.Invoke(this, EventArgs.Empty);
            timer3 = 0;
        }
    }
}
