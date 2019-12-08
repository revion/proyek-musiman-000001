using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    // Min max wall
    public float minRangeWalk;
    public float maxRangeWalk;
    // Player restrictions
    private bool CanMove;
    private bool CheckPhone;
    // Property that should fulfilled to player (fill everything here)
    // Stage 1
    private bool HaveWallet;
    private bool HaveSeenCalendar;
    private bool HaveSeenLaptop;
    // Stage 2
    private bool HaveCheckPhone;
    private bool TalkToGrandma;

    private void Awake()
    {
        // At start of game, save session of played stage
        if (SceneManager.GetActiveScene().name == "Game-1")
        {
            PlayerPrefs.SetInt("currentStage", 1);
        }
        else if (SceneManager.GetActiveScene().name == "Game-2")
        {
            PlayerPrefs.SetInt("currentStage", 2);
        }
        else if (SceneManager.GetActiveScene().name == "Game-4")
        {
            PlayerPrefs.SetInt("currentStage", 4);
        }
        else
        {
            PlayerPrefs.SetInt("currentStage", 0);
        }
    }

    private void Start()
    {
        // Define all restrictions here
        if(PlayerPrefs.GetInt("currentStage") == 1)
        {
            CanMove      = true;
            // Define all property requirements here
            // Stage 1
            HaveWallet = false;
            HaveSeenCalendar = false;
            HaveSeenLaptop = false;
        }
        else if(PlayerPrefs.GetInt("currentStage") == 2)
        {
            CanMove      = false;
            // Define all property requirements here
            // Stage 2
            HaveCheckPhone = false;
            TalkToGrandma = false;
        }
        else if(PlayerPrefs.GetInt("currentStage") == 4)
        {
            CanMove = true;
        }
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
        // Run the animation of checking phone
        animator.SetBool("checking_phone", CheckPhone);

        transform.position = new Vector2(
            Mathf.Clamp(transform.position.x, minRangeWalk, maxRangeWalk),
            transform.position.y
        );
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
        else if (content == "phone")
        {
            return HaveCheckPhone;
        }
        else if (content == "phone_animation")
        {
            return CheckPhone;
        }
        else if(content == "grandma")
        {
            return TalkToGrandma;
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
        else if(content == "phone")
        {
            HaveCheckPhone = state;
        }
        else if (content == "phone_animation")
        {
            CheckPhone = state;
        }
        else if(content == "grandma")
        {
            TalkToGrandma = state;
        }
    }
}
