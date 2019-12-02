using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    GameObject playerObject;
    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision);

        if(collision.gameObject.name == playerObject.name)
        {
            Debug.Log("You are colliding");
        }
    }
}
