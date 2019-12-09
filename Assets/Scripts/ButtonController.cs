using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public PlayerController Player;
    public GameObject NextStageButton;
    public MonologueController MonologClass;
    private int CurrentStage;

    // Start is called before the first frame update
    void Start()
    {
        // Do not pass value when at menu
        if(SceneManager.GetActiveScene().name != "Menu")
        {
            CurrentStage = PlayerPrefs.GetInt("currentStage");
        }

        PlayerPrefs.SetInt("currentDialog", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name != "Loading")
        {
            if (CurrentStage == 1)
            {
                // When on Game Scene, check for player requirements
                // Needed: Check calendar, check laptop, then grab wallet (in any orders)
                if (Player.GetProperty("calendar") == true && Player.GetProperty("laptop") == true && Player.GetProperty("wallet") == true)
                {
                    // If requirements are fulfilled, then pop up the button to next stage
                    NextStageButton.SetActive(true);
                }
            }
            else if (CurrentStage == 2)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                    RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                    if (hit.collider != null)
                    {
                        if (hit.collider.gameObject.name == "CheckPhoneButton")
                        {
                            CheckPhone();
                            MonologClass.SetContent("Memeriksa HP");
                            MonologClass.ShowMonolog();
                        }
                        else if (hit.collider.gameObject.name == "GetTalk")
                        {
                            List<string> contents = new List<string> {
                                "Nenek: \"Pagi banget dek. Mau ke mana?\"",
                                "Player: \"Ke museum, nek.\"",
                                "Nenek: \"Museum opo?\"",
                                "Player: \"Museum... Apa ya it-tu... Museum Proklamasi, nek\"",
                                "Nenek: \"Oh, maksudnya Museum Perumusan Naskah Proklamasi ya. Tempatnya unik loh. Kamu bakal mendapatkan banyak hal menarik dari sana\"",
                                "Player: \"Gitu nek? Jadi gak sabar.\"",
                                "Player: \"Terimakasih nek!\"",
                            };

                            MonologClass.SetChainingContent(contents);
                            hit.collider.gameObject.SetActive(false);
                        }
                        else if (hit.collider.gameObject.name == "NextStage")
                        {
                            NextStage();
                        }
                    }
                }

                // When on Game Scene, check for player requirements
                // Needed: Check your phone and talk to the grandma
                if (Player.GetProperty("phone") == true && Player.GetProperty("grandma") == true)
                {
                    // If requirements are fulfilled, then pop up the button to next stage
                    NextStageButton.SetActive(true);
                }
            }
            else if (CurrentStage == 3)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                    RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                    if (hit.collider != null)
                    {
                        if (hit.collider.gameObject.name == "NextStage")
                        {
                            NextStage();
                        }
                    }
                }
            }
            else if(CurrentStage == 4)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                    RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                    if (hit.collider != null)
                    {
                        if (hit.collider.gameObject.name == "Interact2" || hit.collider.gameObject.name == "Interact1")
                        {
                            // Hide button
                            hit.collider.gameObject.SetActive(false);
                            // Have paper
                            int havePaper = PlayerPrefs.GetInt("havePaper");
                            PlayerPrefs.SetInt("havePaper", ++havePaper);
                            Debug.Log(havePaper);
                            if (havePaper == 2)
                            {
                                MonologClass.SetContent("Kertas sudah diambil, kamu bisa masuk ke museum sekarang!");
                                MonologClass.ShowMonolog();
                                NextStageButton.SetActive(true);
                            }
                        }
                        else if(hit.collider.gameObject.name == "NextStage")
                        {
                            NextStage();
                        }
                    }
                }
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
        GameObject.Find("StaticObjects/Wallet").SetActive(false);
        // Hide wallet interaction button
        GameObject.Find("Canvas/wallet").SetActive(false);
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
        // Hide button
        GameObject.Find("Player/CheckPhoneButton").SetActive(false);
    }

    /**
     * Function to handle next stage button (vary on each scene)
     */
    public void NextStage()
    {
        if (CurrentStage == 1)
        {
            // Check requirements if fulfilled
            // Check order: calendar, laptop, then wallet
            if (Player.GetProperty("calendar") == true && Player.GetProperty("laptop") == true && Player.GetProperty("wallet") == true)
            {
                print("Next stage");
                // Go to train scene
                SceneManager.LoadScene("Game-" + (++CurrentStage).ToString());
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
        else if (CurrentStage == 2)
        {
            // Check requirements if fulfilled
            // Check order: phone and grandma
            if (Player.GetProperty("phone") == true && Player.GetProperty("grandma") == true)
            {
                print("Next stage");
                // Go to stage 3
                SceneManager.LoadScene("Game-" + (++CurrentStage).ToString());
            }
            else if (Player.GetProperty("phone") == false)
            {
                print("Check your phone first! You have no information");
            }
            else if (Player.GetProperty("grandma") == false)
            {
                print("Ask someone to get more information");
            }
        }
        else if(CurrentStage == 3)
        {
            // Go to stage 4
            SceneManager.LoadScene("Game-" + (++CurrentStage).ToString());
        }
        else if(CurrentStage == 4)
        {
            // Check how many shattered paper have been recovered
            if(PlayerPrefs.GetInt("havePaper") == 2)
            {
                // Go to stage 5
                SceneManager.LoadScene("Game-" + (++CurrentStage).ToString());
            }
        }
    }
}
