using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonologueController : MonoBehaviour
{
    // Refer to monolog UI object
    public GameObject MonologObject;
    public Text contentMonolog;
    // Refer to class PlayerController
    public PlayerController Player;
    // Current stage
    private int CurrentStage;
    // Timer stage to hide monolog if not clicked
    public float timerToHide = 10.0f;
    private bool ChainingLog = false;

    // Start is called before the first frame update
    void Start()
    {
        CurrentStage = PlayerPrefs.GetInt("currentStage");
    }

    // Update is called once per frame
    void Update()
    {
        // Only affect stage 4
        if(CurrentStage == 3)
        {
            if(timerToHide > 0.0f)
            {
                // Count time
                timerToHide -= Time.deltaTime;
            }
            else
            {
                // When timer is 0, then hide monolog
                HideMonolog();
            }
        }
    }

    /**
     * Function to set chaining dialog
     * 
     * @param contents List dialog
     */
    public void SetChainingContent(List<string> contents)
    {
        // Get dialog position
        int index = PlayerPrefs.GetInt("currentDialog");
        // Trigger dialog
        ChainingLog = true;

        if(index < contents.Count)
        {
            // Set text dialog
            contentMonolog.GetComponent<Text>().text = contents[index];
            // Increment dialog position
            PlayerPrefs.SetInt("currentDialog", ++index);
        }
        else
        {
            // Ended chaining dialog
            ChainingLog = false;
        }
        // Show dialog
        ShowMonolog();
    }

    /**
     * Function to chaining chat around (static unfortunately)
     */
    public void ContinueChaining()
    {
        // Check chaining status
        if(ChainingLog == true)
        {
            // For stage 2
            if(CurrentStage == 2)
            {
                // List dialog
                List<string> contents = new List<string> {
                    "Nenek: \"Pagi banget dek. Mau ke mana?\"",
                    "Player: \"Ke museum, nek.\"",
                    "Nenek: \"Museum opo?\"",
                    "Player: \"Museum... Apa ya it-tu... Museum Proklamasi, nek\"",
                    "Nenek: \"Oh, maksudnya Museum Perumusan Naskah Proklamasi ya. Tempatnya unik loh.\nKamu bakal mendapatkan banyak hal menarik dari sana\"",
                    "Player: \"Gitu nek? Jadi gak sabar.\"",
                    "Player: \"Terimakasih nek!\"",
                };

                // Show dialog
                SetChainingContent(contents);

                if(PlayerPrefs.GetInt("currentDialog") == contents.Count)
                {
                    // Mark gathering information about the museum as done
                    Player.SetProperty("grandma", true);
                    // Hide dialog
                    HideMonolog();
                }
            }
        }
    }

    /**
     * Function to set content of monolog
     */
    public void SetContent(string text)
    {
        // Set this monologue to text content
        contentMonolog.GetComponent<Text>().text = text;
        // Debug log
        print(contentMonolog.GetComponent<Text>().text);
    }

    /**
     * Function to show monolog object
     */
    public void ShowMonolog()
    {
        // Restrict player movement
        Player.SetProperty("movement", false);
        // Show monolog object
        MonologObject.SetActive(true);
    }

    /**
     * Function to show what should we do in game
     */
    public void ShowMission() => MonologObject.SetActive(true);

    /**
     * Function to hide monolog object
     */
    public void HideMonolog()
    {
        if(ChainingLog == false)
        {
            // Check monolog object state first
            if(MonologObject.activeSelf == true)
            {
                if(CurrentStage == 2)
                {
                    // Make player stop playing phone
                    Player.SetProperty("phone_animation", false);
                }
                // Restrict player movement
                Player.SetProperty("movement", true);
                // Hide monolog object
                MonologObject.SetActive(false);
            }
        }
    }
}
