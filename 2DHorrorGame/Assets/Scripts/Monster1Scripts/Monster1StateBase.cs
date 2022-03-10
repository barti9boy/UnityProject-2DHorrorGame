using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster1StateBase 
{
    public Rigidbody2D rb;
    public Transform monster1Transform;
    public GameObject monster1GFX;
    public SpriteRenderer monster1SpriteRenderer;
    public Collider2D collider;

    public bool isFacingRight = true;
    public float movementSpeed = 5f;


    public Monster1StateBase(GameObject monster1Object)
    {

    }
    public abstract void EnterState(Monster1StateMachine monster1, Collider2D collision = null);
    public abstract void UpdateState(Monster1StateMachine monster1, Collider2D collision = null);
    public abstract void OnCollisionEnter(Monster1StateMachine monster1, Collision2D collision);
    public abstract void OnTriggerStay(Monster1StateMachine monster1, Collider2D collision);
}

