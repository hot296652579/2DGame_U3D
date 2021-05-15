using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAniControl : MonoBehaviour
{
    private Animator animator;
    private PlayerControl playerControl;
    private Rigidbody2D rg2D;

    int speedId;
    int isCrouchingId;
    int isHangingId;
    int isOnGroundId;
    int verticalVelocityId;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerControl = GetComponentInParent<PlayerControl>();
        rg2D = GetComponentInParent<Rigidbody2D>();

        speedId = Animator.StringToHash("speed");
        isOnGroundId = Animator.StringToHash("isOnGround");
        isCrouchingId = Animator.StringToHash("isCrouching");
        isHangingId = Animator.StringToHash("isHanging");
        verticalVelocityId = Animator.StringToHash("verticalVelocity");
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat(speedId, Mathf.Abs(playerControl.xVolcity));
        animator.SetBool(isOnGroundId, playerControl.isOnGround);
        animator.SetBool(isCrouchingId, playerControl.isCrouch);
        animator.SetBool(isHangingId, playerControl.isHanging);

        animator.SetFloat(verticalVelocityId, rg2D.velocity.y);
    }

    public void playStepAudio()
    {
        AudioManager.playStepAudio();
    }

    public void crouchAudio()
    {
        AudioManager.playCroushAudio();
    }
}
