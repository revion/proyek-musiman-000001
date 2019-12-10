using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClamp : MonoBehaviour
{
    public Transform targetToFollow;

    public float minWall;
    public float maxWall;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.GetInt("currentStage") == 5)
        {
            // Stage 5, expand area to be explored when have ticket
            if(GameObject.Find("Player").GetComponent<PlayerController>().GetProperty("ticket") == true)
            {
                minWall = -56.62f;
                maxWall = 56.6f;
            }
        }

        transform.position = new Vector3(
            Mathf.Clamp(targetToFollow.position.x, minWall, maxWall),
            transform.position.y,
            transform.position.z
        );

    }
}
