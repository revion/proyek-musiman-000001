using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public PlayerController Player;
    public GameObject NextStageButton;
    private int CurrentStage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("currentStage") == 1)
        {
            // When on Game Scene, check for player requirements
            // Needed: Check calendar, check laptop, then grab wallet (in any orders)
            if (Player.GetProperty("calendar") == true && Player.GetProperty("laptop") == true && Player.GetProperty("wallet") == true)
            {
                // If requirements are fulfilled, then pop up the button to next stage
                NextStageButton.SetActive(true);
            }
        }
    }
    
    /**
     * Function to Start Game
     */
    public void StartGame()
    {
        // Only active at scene main menu
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            // Go to loading scene
            SceneManager.LoadScene("Loading");
        }
    }

    /**
     * Function to fulfill calendar check requirement
     */
    public void SeenCalendar() => Player.SetProperty("calendar", true);

    /**
     * Function to fulfill laptop check requirement
     */
    public void SeenLaptop() => Player.SetProperty("laptop", true);

    /**
     * Function to fulfill grab wallet requirement
     */
    public void GrabWallet()
    {
        Player.SetProperty("wallet", true);
        // Hide wallet object
        GameObject.Find("Dompet").SetActive(false);
    }

    /**
     * Function to fulfill phone requirement
     */
    public void CheckPhone()
    {
        // Make player play phone
        Player.SetProperty("phone_animation", true);
        // Change state phone
        Player.SetProperty("phone", true);
    }
    
    /**
     * Function to handle next stage button (vary on each scene)
     */
    public void NextStage()
    {
        if (PlayerPrefs.GetInt("currentStage") == 1)
        {
            // Check requirements if fulfilled
            // Check order: calendar, laptop, then wallet
            if (Player.GetProperty("calendar") == true && Player.GetProperty("laptop") == true && Player.GetProperty("wallet") == true)
            {
                print("Next stage");
                // Go to train scene
                SceneManager.LoadScene("Game-2");
            }
            else if (Player.GetProperty("calendar") == false)
            {
                print("Check your calendar first!");
            }
            else if (Player.GetProperty("laptop") == false)
            {
                print("Check your laptop first!");
            }
            else if (Player.GetProperty("wallet") == false)
            {
                print("Grab your wallet!");
            }
        }
        else if (PlayerPrefs.GetInt("currentStage") == 2)
        {
            print("Stage 2");
        }
    }
}
