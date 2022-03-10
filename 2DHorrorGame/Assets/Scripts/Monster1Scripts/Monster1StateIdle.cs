using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster1StateIdle : Monster1StateBase
{
    public Monster1StateIdle(GameObject monster1Object) : base(monster1Object)
    {
        rb = monster1Object.GetComponent<Rigidbody2D>();
        monster1Transform = monster1Object.GetComponent<Transform>();
        monster1GFX = monster1Object.transform.GetChild(0).gameObject;
        monster1SpriteRenderer = monster1GFX.GetComponent<SpriteRenderer>();
        
    }

    public event EventHandler OnEnterStateIdle;

    public override void EnterState(Monster1StateMachine monster1, Collider2D collision = null)
    {
        OnEnterStateIdle?.Invoke(this, EventArgs.Empty);
        isFacingRight = monster1.previousState.isFacingRight;
    }
    public override void UpdateState(Monster1StateMachine monster1, Collider2D collision = null)
    {
        rb.velocity = new Vector2(0, 0);
    }
    public override void OnCollisionEnter(Monster1StateMachine monster1, Collision2D collision)
    {

    }
    public override void OnTriggerStay(Monster1StateMachine monster1, Collider2D collision)
    {

    }
}
