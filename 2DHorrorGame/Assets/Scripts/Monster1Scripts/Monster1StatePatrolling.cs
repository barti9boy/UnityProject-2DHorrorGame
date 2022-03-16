using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Monster1StatePatrolling : Monster1StateBase
{
    public Monster1WaypointsStorage waypointsStorage;
    public Transform[] waypoints;
    Vector2 previousWaypoint;
    Vector2 currentWaypoint;
    Vector2 raycastRange;
    private LayerMask playerLayer;

    int m_PreviousWaypointIndex = -1;
    int m_CurrentWaypointIndex;
    private bool spottedPlayer;

    int movementDirection;
    public event EventHandler OnEnterStatePatrolling;


    public Monster1StatePatrolling(GameObject monster1Object) : base(monster1Object)
    {
        rb = monster1Object.GetComponent<Rigidbody2D>();
        monster1Transform = monster1Object.GetComponent<Transform>();
        monster1GFX = monster1Object.transform.GetChild(0).gameObject;
        monster1SpriteRenderer = monster1GFX.GetComponent<SpriteRenderer>();
        waypointsStorage = monster1Object.GetComponent<Monster1WaypointsStorage>();
        waypoints = waypointsStorage.waypoints;
      
    }
    //public event EventHandler OnEnterStatePatrolling;

    public override void EnterState(Monster1StateMachine monster1, Collider2D collision = null)
    {
        playerLayer = LayerMask.NameToLayer("Player");
        spottedPlayer = false;
        OnEnterStatePatrolling?.Invoke(this, EventArgs.Empty);
        isFacingRight = monster1.previousState.isFacingRight;
        m_CurrentWaypointIndex = (m_PreviousWaypointIndex + 1) % waypoints.Length;
        currentWaypoint = waypoints[m_CurrentWaypointIndex].position;
        if (currentWaypoint.x < monster1Transform.position.x) //Waypoint is on the left 
        {
            if (isFacingRight)
            {
                Flip();
            }
            movementDirection = -1;
        }
        if (currentWaypoint.x > monster1Transform.position.x) //Waypoint is on the right
        {
            if (!isFacingRight)
            {
                Flip();
            }
            movementDirection = 1;
        }
    }
    public override void UpdateState(Monster1StateMachine monster1, Collider2D collision = null)
    {
        raycastRange = new Vector2(monster1Transform.position.x - playerCheckDistance, 0);
        rb.velocity = new Vector2(movementDirection * movementSpeed, 0);
        if(Mathf.Abs(monster1Transform.position.x - currentWaypoint.x) < 0.1)
        {
            m_PreviousWaypointIndex = m_CurrentWaypointIndex;
            monster1.previousState = this;
            monster1.SwitchState(monster1.idleState);
        }
        RaycastHit2D hitPlayer = Physics2D.Raycast(monster1Transform.position, Vector2.right * movementDirection, playerCheckDistance, LayerMask.NameToLayer("Player"));
        Debug.DrawRay(monster1Transform.position, Vector2.right * movementDirection * playerCheckDistance);
       // Debug.Log(LayerMask.LayerToName(8));

        if (hitPlayer.collider != null)
        {
            Debug.Log(isFacingRight);
            Debug.Log("spotted player patrolling");
            Debug.Log(hitPlayer.collider.tag) ;

        }

    }
    public override void OnCollisionEnter(Monster1StateMachine monster1, Collision2D collision)
    {

    }
    public override void OnTriggerStay(Monster1StateMachine monster1, Collider2D collision)
    {

    }
    
    public void Flip()
    {
        monster1Transform.Rotate(0.0f, 180.0f, 0.0f);
        isFacingRight = !isFacingRight;
    }
}
