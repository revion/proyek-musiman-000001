using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingController : MonoBehaviour
{
    public float timer = 10.0f;
    private Text loadingText;
    // Start is called before the first frame update
    void Start()
    {
        loadingText = GameObject.Find("LoadingText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0.0f)
        {
            SceneManager.LoadScene("Game");
        }
    }

    private void FixedUpdate()
    {
        string tempTextLoading = loadingText.text;
        if (tempTextLoading.LastIndexOf('.') > 9)
        {
            loadingText.text = "Loading";
        }
        else
        {
            loadingText.text += ".";
        }
    }
}
