using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
using Entities;
using UnityEngine;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

public class PlayerMovement : AgileBeing
{
    // this script goes in the player object
    //--------------------------------------
    
    // gets the character controller component from the player
    [SerializeField] private CharacterController controller;

    // sets the movement speed, gravity and jump height
    [SerializeField] private float speed = 12f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 3f;

    // getting and setting stuff for the ground detection

    // creates a velocity Vector3 to be used with gravity
    private Vector3 velocity;

    public bool stepping;
    private float waitTime = 0.5f;
    
    private bool canFallSoundPlay;

    protected override void Update()
    {
        base.Update();
        
        // ground detection stuff
        if (isGrounded && velocity.y < 0) { velocity.y = -2f; }
        
        // getting inputs
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // setting movement according to inputs
        var move = transform.right * x + transform.forward * z;

        // "Move" method from character controller using the movement Vector3 and multiplying by the speed
        controller.Move(move * (speed * Time.deltaTime));

        // jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            //FindObjectOfType<MovementSound>().PlayJumpSound();
        }

        //if (!isGrounded && velocity.y < -5)
        //{
        //    canFallSoundPlay = true;
        //}

        //if (isGrounded && canFallSoundPlay)
        //{
        //    FindObjectOfType<MovementSound>().PlayFallSound();
        //    Invoke("FallSoundReset", waitTime);
        //}
        
        //making gravity work
        velocity.y += gravity * Time.deltaTime;

        //applying velocity (gravity) the player
        controller.Move(velocity * Time.deltaTime);
        
        // step sounds
        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.1 && !stepping && isGrounded || Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1 && !stepping&& isGrounded)
        {
            stepping = true;
            FindObjectOfType<MovementSound>().PlayStepSound();
            Invoke("ChangeStep", waitTime);
        }
    }

    private void ChangeStep()
    {
        stepping = false;
    }

    private void FallSoundReset()
    {
        canFallSoundPlay = false;
    }
}