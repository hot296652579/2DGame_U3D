using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rg2D;

    public float speed = 5f;
    public float croushSpeedDiv = 2f;

    public float xVolcity;

    [Header("状态")]
    public bool isCrouch;
    public bool isJump;
    public bool isOnGround;
    public bool isHeadBlocked;
    public bool isHanging;

    private BoxCollider2D box2D;
    private Vector2 standUpCollSize;
    private Vector2 standupCollOffset;
    private Vector2 crouchCollSize;
    private Vector2 crouchCollOffset;

    public bool crouchHoldPree;
    public bool crouchPree;
    public bool jumpPree;
    public bool jumpHoldPree;
 

    private float jumpForce = 6.5f;
    private float jumpHoldForce = 1.5f;
    private float jumpCurrentTime;
    private float jumpDirTime = 0.1f;
    private float crouchJumpBoost = 5.8f;
    private float hangingJumpForce = 18f;

    private float footOffset = 0.4f;
    private float groundDistance = 0.2f;
    private float headDistance = 0.1f;

    private float playHeight;
    private float grabDir = 0.4f;
    private float eyeHeight = 1.5f;
    private float reachOffset = 0.7f;

    public LayerMask groundLayer;
    private int enemyLayerId;

    public Animator animator;
    public GameObject deathEnemyPre;

    public GameObject firePoint;

    private Joystick joystick;

    void Start()
    {
        joystick = GameObject.FindWithTag("Joystick").GetComponent<Joystick>();
        rg2D = GetComponent<Rigidbody2D>();
        box2D = GetComponent<BoxCollider2D>();

        playHeight = box2D.size.y;

        standUpCollSize = box2D.size;
        standupCollOffset = box2D.offset;
        crouchCollSize = new Vector2(box2D.size.x, box2D.size.y / 2f);
        crouchCollOffset = new Vector2(box2D.offset.x, box2D.offset.y / 2f);

        enemyLayerId = LayerMask.NameToLayer("Enemy");
    }

    private void Update()
    {
        //crouchHoldPree = Input.GetButton("Crouch");
        //crouchPree = Input.GetButtonDown("Crouch");
        //jumpPree = Input.GetButtonDown("Jump");
        //jumpHoldPree = Input.GetButton("Jump");
    }

    private void FixedUpdate()
    {
        PhysicsCheck();
        moveHandler();
        turnHandler();
        jumpHandler();
    }

    void PhysicsCheck()
    {
        RaycastHit2D leftFootCheck = RaycastHandler(new Vector2(-footOffset, 0f), Vector2.down, groundDistance, groundLayer);
        RaycastHit2D rightFootCheck = RaycastHandler(new Vector2(footOffset, 0f), Vector2.down, groundDistance, groundLayer);
        RaycastHit2D headCheck = RaycastHandler(new Vector2(0f,box2D.size.y), Vector2.up, headDistance, groundLayer);

        if (leftFootCheck || rightFootCheck)
            isOnGround = true;
        else
            isOnGround = false;

        if (headCheck)
            isHeadBlocked = true;
        else
            isHeadBlocked = false;

        float playDir = transform.localScale.x;
        Vector2 rayDir = new Vector2(playDir, 0f);

        RaycastHit2D headTopCheck = RaycastHandler(new Vector2(footOffset * playDir, playHeight), rayDir, grabDir, groundLayer);
        RaycastHit2D wallCheck = RaycastHandler(new Vector2(footOffset * playDir, eyeHeight), rayDir, grabDir, groundLayer);
        RaycastHit2D ledgeCheck = RaycastHandler(new Vector2(reachOffset * playDir, playHeight), Vector2.down, grabDir, groundLayer);

        if(wallCheck && ledgeCheck && !headTopCheck && !isOnGround && rg2D.velocity.y < 0f)
        {
            Vector3 pos = transform.position;
            pos.x += (wallCheck.distance - 0.05f) * playDir;
            pos.y -= ledgeCheck.distance;
            transform.position = pos;

            rg2D.bodyType = RigidbodyType2D.Static;
            isHanging = true;
        }
    }

    private void moveHandler()
    {
        if (isHanging)
            return;
        //xVolcity = Input.GetAxis("Horizontal");
        xVolcity = joystick.Horizontal;

        if (isOnGround)
        {
            if (crouchHoldPree && !isCrouch)
                crouch();
            else if (crouchHoldPree && isCrouch && !isHeadBlocked)
                standUp();
        }
        else
        {
            if(isJump)
                standUp();
        }


        if (isCrouch)
            xVolcity /= croushSpeedDiv;

        rg2D.velocity = new Vector2(xVolcity * speed, rg2D.velocity.y);
    }

    private void turnHandler()
    {
        if (isHanging)
            return;

        if (xVolcity < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            firePoint.transform.localScale = new Vector3(-1, 1, 1);
            //transform.Rotate(0f, 180f, 0f);
        }
           
        if (xVolcity > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            firePoint.transform.localScale = new Vector3(1, 1, 1);
            //transform.Rotate(0f, 0f, 0f);
        }   
    }

    public void standUp()
    {
        isCrouch = false;
        box2D.size = standUpCollSize;
        box2D.offset = standupCollOffset;
    }

    public void crouch()
    {
        isCrouch = true;
        box2D.size = crouchCollSize;
        box2D.offset = crouchCollOffset;
    }

    public void jumpHandler()
    {
        if (isHanging)
        {
            if (jumpPree)
            {
                rg2D.bodyType = RigidbodyType2D.Dynamic;
                rg2D.AddForce(new Vector2(0f, hangingJumpForce), ForceMode2D.Impulse);
                isHanging = false;
            }

            if (crouchPree)
            {
                rg2D.bodyType = RigidbodyType2D.Dynamic;
                isHanging = false;
            }
        }

        if(isOnGround && jumpPree && !isJump && !isHeadBlocked)
        {
            isOnGround = false;
            isJump = true;

            if(isCrouch)
            {
                standUp();
                rg2D.AddForce(new Vector2(0f, crouchJumpBoost), ForceMode2D.Impulse);
            }

            jumpCurrentTime = Time.time + jumpDirTime;
            rg2D.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);

            AudioManager.playJumpAudio();

        }
        else if (isJump)
        {
            if (jumpHoldPree)
                rg2D.AddForce(new Vector2(0f, jumpHoldForce), ForceMode2D.Impulse);

            if (jumpCurrentTime < Time.time)
                isJump = false;
        }
    }

    RaycastHit2D RaycastHandler(Vector2 offset,Vector2 dir,float length,LayerMask layer)
    {
        Vector2 pos = transform.position;
        RaycastHit2D hint = Physics2D.Raycast(pos + offset, dir, length, layer);

        Color color;
        if (hint)
            color = Color.red;
        else
            color = Color.green;
        Debug.DrawRay(pos + offset, dir, color, length);

        return hint;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == enemyLayerId)
        {
            if (rg2D.velocity.y < 0.1f && !isOnGround)
            {
                Destroy(collision.gameObject);
                Instantiate(deathEnemyPre, transform.position, transform.rotation);
                AudioManager.playDeathAudio();
            } 
        }
    }
}
