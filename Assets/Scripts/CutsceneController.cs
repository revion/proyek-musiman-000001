using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneController : MonoBehaviour
{
    public PlayerController Player;
    public Sprite[] ListCutScene;
    public GameObject Image;
    public GameObject[] ShowButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetImage(string content="")
    {
        if (content == "pertemuan")
        {
            Image.GetComponentsInChildren<Image>()[1].sprite = ListCutScene[0];
            Player.SetProperty("scene-meeting", true);
            Player.SetProperty("movement", false);
            ShowCutscene();
            ShowButton[0].SetActive(true);
        }
        else if (content == "perumusan")
        {
            Image.GetComponentsInChildren<Image>()[1].sprite = ListCutScene[1];
            Player.SetProperty("scene-formulation", true);
            Player.SetProperty("movement", false);
            ShowCutscene();
            ShowButton[1].SetActive(true);
        }
        else if (content == "pengesahan")
        {
            Image.GetComponentsInChildren<Image>()[1].sprite = ListCutScene[2];
            Player.SetProperty("scene-validate", true);
            Player.SetProperty("movement", false);
            ShowCutscene();
            ShowButton[2].SetActive(true);
        }
        else if (content == "pengetikan")
        {
            Image.GetComponentsInChildren<Image>()[1].sprite = ListCutScene[3];
            Player.SetProperty("scene-typing", true);
            Player.SetProperty("movement", false);
            ShowCutscene();
            ShowButton[3].SetActive(true);
        }
        else if (content == "pembacaan")
        {
            if (PlayerPrefs.GetInt("havePaper") == 4)
            {
                Debug.Log("OK, load the last cutscene");
                Player.SetProperty("scene-reading", true);
                //Player.SetProperty("movement", false);
            }
            else
            {
                Debug.Log("You need to collect all shattered papers");
            }
        }
    }

    public void HideCutscene()
    {
        Image.SetActive(false);
        Player.SetProperty("movement", true);
    }

    private void ShowCutscene()
    {
        Image.SetActive(true);
    }
}
