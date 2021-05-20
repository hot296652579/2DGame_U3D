using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnCrouchPressBtn : MonoBehaviour
{
    private PlayerControl playerControl;
    public Button button;
    private bool isCrouch;

    private void Start()
    {
        isCrouch = false;
        GameObject player = GameObject.FindWithTag("Player");
        playerControl = player.GetComponent<PlayerControl>();
        button.onClick.AddListener(() =>
        {
            isCrouch = !isCrouch;
            if (isCrouch)
                playerControl.crouch();
            else
                playerControl.standUp();
        });
    }

}
