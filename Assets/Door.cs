using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    int playId;
    int openId;
    Animator animator;

    private void Start()
    {
        playId = LayerMask.NameToLayer("Player");
        openId = Animator.StringToHash("Open");
        animator = GetComponentInParent<Animator>();

        DoorEventSystem.Instance.onDoorOpen += OpenDoor;
    }

    private void OpenDoor()
    {
        animator.SetBool(openId, true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == playId)
        {
            DoorEventSystem.Instance.onOpenTheDoor();
        }
    }

}
