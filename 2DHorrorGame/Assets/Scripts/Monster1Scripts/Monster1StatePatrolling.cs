using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster1StatePatrolling : Monster1StateBase
{
    public Transform[] waypoints;

    Vector2 previousWaypoint;
    Vector2 currentWaypoint;

    int m_PreviousWaypointIndex;
    int m_CurrentWaypointIndex = 0;

    int movementDirection;

    public Monster1StatePatrolling(GameObject monster1Object) : base(monster1Object)
    {
        rb = monster1Object.GetComponent<Rigidbody2D>();
        monster1Transform = monster1Object.GetComponent<Transform>();
        monster1GFX = monster1Object.transform.GetChild(0).gameObject;
        monster1SpriteRenderer = monster1GFX.GetComponent<SpriteRenderer>();

    }
    //public event EventHandler OnEnterStatePatrolling;

    public override void EnterState(Monster1StateMachine monster1, Collider2D collision = null)
    {
        //OnEnterStatePatrolling?.Invoke(this, EventArgs.Empty);
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
        if (currentWaypoint.x < monster1Transform.position.x) //Waypoint is on the right
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
        rb.velocity = new Vector2(movementDirection * movementSpeed, 0);
        if(Mathf.Abs(monster1Transform.position.x - currentWaypoint.x) < 0.1)
        {
            m_PreviousWaypointIndex = m_CurrentWaypointIndex;
            monster1.SwitchState(monster1.idleState);
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
