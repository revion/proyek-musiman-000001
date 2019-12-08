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
        transform.position = new Vector3(
            Mathf.Clamp(targetToFollow.position.x, minWall, maxWall),
            transform.position.y,
            transform.position.z
        );
    }
}
