using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Class character controllers
    public CharacterController2D controller;
    // Animator
    public Animator animator;

    // Default speed player
    public float runSpeed = 40f;
    // Horizontal move state
    private float horizontalMove = 0f;
    // Player restrictions
    private bool CanMove;
    // Property that should fulfilled to player (fill everything here)
    private bool HaveWallet;
    private bool HaveSeenCalendar;
    private bool HaveSeenLaptop;

    private void Start()
    {
        // Define all restrictions here
        CanMove          = true;
        // Define all property requirements here
        HaveWallet       = false;
        HaveSeenCalendar = false;
        HaveSeenLaptop   = false;
    }

    // Update is called once per frame
    void Update()
    {
        // If movement is not restricted
        if (CanMove == true)
        {
            // Set horizontal move based on button clicked
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        }
        else
        {
            // Stop player from moving
            horizontalMove = 0.0f;
        }

        // Run the animation of walking
        animator.SetFloat("speed", Mathf.Abs(horizontalMove));
    }

    void FixedUpdate()
    {
        // Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, false);
    }

    /**
     * Getter property requirements
     * 
     * @param content Property content
     * @return bool
     */
    public bool GetProperty(string content)
    {
        if(content == "laptop")
        {
            return HaveSeenLaptop;
        }
        else if(content == "calendar")
        {
            return HaveSeenCalendar;
        }
        else if(content == "wallet")
        {
            return HaveWallet;
        }
        else if(content == "movement")
        {
            return CanMove;
        }
        else
        {
            return false;
        }
    }

    /**
     * Setter property requirements
     * 
     * @param content Property content
     * @param state State which to set
     */
    public void SetProperty(string content, bool state)
    {
        if (content == "laptop")
        {
            HaveSeenLaptop = state;
        }
        else if (content == "calendar")
        {
            HaveSeenCalendar = state;
        }
        else if (content == "wallet")
        {
            HaveWallet = state;
        }
        else if(content == "movement")
        {
            CanMove = state;
        }
    }
}
