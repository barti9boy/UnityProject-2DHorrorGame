using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

static class PlayerActions 
{

    public static void Move(InputAction.CallbackContext context, Rigidbody2D rb) 
    {
        Debug.Log(context);
    }
}
