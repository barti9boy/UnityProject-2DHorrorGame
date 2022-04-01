using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System;

public class MultiStateAnimationManager : MonoBehaviour
{
    
    public AnimationClip itemPickUpAnimation;
    public event EventHandler OnItemPickup;
    private bool isPickUpAnimationPlaying;

    public event EventHandler OnAnimationFinished;
    public PlayerInput playerInput;
    public InputManager inputManager;
    private float timer;
    
    private void Awake()
    {
        timer = 0f;
        isPickUpAnimationPlaying = false;
        playerInput = this.gameObject.GetComponent<PlayerInput>();
        inputManager = this.gameObject.GetComponent<InputManager>();
    }
    private void Update()
    {
        if(isPickUpAnimationPlaying)
        {
            Debug.Log("hi");
            AnimationTimer(itemPickUpAnimation);
        }
    }

    public void PlayPickUpAnimation()
    {
        OnItemPickup?.Invoke(this, EventArgs.Empty);
        isPickUpAnimationPlaying = true;
        inputManager.movementInputEnabled = false;
        inputManager.interactionInputEnabled = false;
    }

    public void AnimationTimer(AnimationClip clip)
    {
        timer += Time.deltaTime;
        if(timer >= clip.length)
        {
            timer = 0;
            isPickUpAnimationPlaying = false;
            OnAnimationFinished?.Invoke(this, EventArgs.Empty);
            //add other animation bools here
            inputManager.movementInputEnabled = true;
            inputManager.interactionInputEnabled = true;
        }
    }
}
