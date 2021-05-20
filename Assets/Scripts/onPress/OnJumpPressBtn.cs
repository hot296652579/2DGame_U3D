using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnJumpPressBtn : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    private PlayerControl playerControl;
    private bool isPress = false;

    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        playerControl = player.GetComponent<PlayerControl>();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        isPress = false;
        playerControl.jumpPree = false;
        CancelInvoke("OnPress");
        //Debug.Log("Up");
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        isPress = true;
        playerControl.jumpPree = true;
        InvokeRepeating("OnPress", 0, 0.1f);
        //Debug.Log("Down");
    }
    private void OnPress()
    {
        if (isPress)
            playerControl.jumpHoldPree = true;
    }
}
