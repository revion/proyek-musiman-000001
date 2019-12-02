using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonologueController : MonoBehaviour
{
    // Refer to monolog UI object
    public GameObject MonologObject;
    // Refer to class PlayerController
    public PlayerController Player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /**
     * Function to set content of monolog
     */
    public void SetContent(string text)
    {
        // Set this monologue to text content
        MonologObject.GetComponent<Text>().text = text;
        // Debug log
        print(MonologObject.GetComponent<Text>().text);
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
     * Function to hide monolog object
     */
    public void HideMonolog()
    {
        // Check monolog object state first
        if(MonologObject.activeSelf == true)
        {
            if(PlayerPrefs.GetInt("currentStage") == 2)
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
