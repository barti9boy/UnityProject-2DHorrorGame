using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster1StateIdle : Monster1StateBase
{
   //[SerializeField] private AnimationClip idleAnimation;
    private float timer;
    public event EventHandler OnEnterStateIdle;

    public Monster1StateIdle(GameObject monster1Object) : base(monster1Object)
    {
        rb = monster1Object.GetComponent<Rigidbody2D>();
        monster1Transform = monster1Object.GetComponent<Transform>();
        monster1GFX = monster1Object.transform.GetChild(0).gameObject;
        monster1SpriteRenderer = monster1GFX.GetComponent<SpriteRenderer>();
        
    }


    public override void EnterState(Monster1StateMachine monster1, RaycastHit2D hitPlayer, Collider2D collision = null)
    {
        OnEnterStateIdle?.Invoke(this, EventArgs.Empty);
        //hitPlayer = monster1.previousState.hitPlayer;
        isFacingRight = monster1.previousState.isFacingRight;
        timer = 0;

    }
    public override void UpdateState(Monster1StateMachine monster1, RaycastHit2D hitPlayer, Collider2D collision = null)
    {
        rb.velocity = new Vector2(0, 0);

        //if (isFacingRight)
        //{
        //    hitPlayer = Physics2D.Raycast(monster1Transform.position, monster1Transform.right , playerCheckDistance, 8);

        //}
        //else if (!isFacingRight)
        //{
        //    hitPlayer = Physics2D.Raycast(monster1Transform.position,-monster1Transform.right, playerCheckDistance, 8);
        //}
        //if (hitPlayer.collider != null)
        //{
        //    Debug.Log(isFacingRight);
        //    Debug.Log("spotted player idle");
        //}
        if (hitPlayer.collider != null)
        {
            Debug.Log("Spotted player idle");
            //Switch to chasing state
            monster1.previousState = this;
            monster1.SwitchState(monster1.chasingState, hitPlayer);

        }
        else
        {
            WaitUntilAnimated(monster1, hitPlayer);
        }



    }
    public override void OnCollisionEnter(Monster1StateMachine monster1, Collision2D collision)
    {

    }
    public override void OnTriggerStay(Monster1StateMachine monster1, Collider2D collision)
    {

    }
    public void WaitUntilAnimated(Monster1StateMachine monster1, RaycastHit2D hitPlayer)
    {
        timer += Time.deltaTime;
        if (timer >= 2)
        {
            monster1.previousState = this;
            monster1.SwitchState(monster1.patrollingState, hitPlayer);
        }
    }
}
