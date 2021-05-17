using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private Collider2D collider2D;
    private Animator animator;

    public Transform leftTransform;
    public Transform rightTransform;
    private Vector3 leftPos;
    private Vector3 rightPos;

    private float speed = 3f;
    private float jumpForce = 11f;
    private bool localLeft;

    public LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        leftPos = leftTransform.transform.position;
        rightPos = rightTransform.transform.position;

        Destroy(leftTransform.gameObject);
        Destroy(rightTransform.gameObject);

        rb2D = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();

        localLeft = true;
    }

    // Update is called once per frame
    void Update()
    {
        //moveHandler();
        switchAnimation();
    }

    void moveHandler()
    {
        if (localLeft)
        {
            if (collider2D.IsTouchingLayers(groundLayer))
            {
                rb2D.velocity = new Vector2(-speed, jumpForce);
                animator.SetBool("jump", true);
            }

            
            if(transform.position.x <= leftPos.x)
            {
                localLeft = false;
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        else
        {
            if (collider2D.IsTouchingLayers(groundLayer))
            {
                rb2D.velocity = new Vector2(speed, jumpForce);
                animator.SetBool("jump", true);
            }

            if (transform.position.x >= rightPos.x)
            {
                localLeft = true;
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    void switchAnimation()
    {
        if (animator.GetBool("jump"))
        {
            if(rb2D.velocity.y < 0.01f)
            {
                animator.SetBool("fall", true);
                animator.SetBool("jump", false);
            }
        }

        if(collider2D.IsTouchingLayers(groundLayer) && animator.GetBool("fall"))
        {
            animator.SetBool("fall", false);
        }
    }
}
