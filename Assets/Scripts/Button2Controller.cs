using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button2Controller : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public PlayerController Player;
    public float MoveDirection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    /**
     * When hold button
     */
    public void OnPointerUp(PointerEventData eventData)
    {
        Player.SetAxis(0f);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Player.GetProperty("movement") == false)
        {
            Player.SetAxis(0f);
        }
        else
        {
            Player.SetAxis(MoveDirection);
        }
    }
}
