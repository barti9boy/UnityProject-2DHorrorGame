using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour
{
    public Rigidbody2D rb;
    [SerializeField] private float movmentSpeed;

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }
    public Vector2 Movement(InputAction.CallbackContext context) 
    {
        if(context.performed)
        {
            Debug.Log("" + context.ReadValue<float>());
        }
        return new Vector2(movmentSpeed * context.ReadValue<float>(), 0);
        
    }
}
