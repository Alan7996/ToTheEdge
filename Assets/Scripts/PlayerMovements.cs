using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    public Vector3 horizForce = new Vector3(500f, 0, 0);
    public Vector3 jumpForce = new Vector3(0, 500f, 0);
    private int jumpCount = 0;
    private bool didJump = false;

    public Sprite idleSprite;
    public Sprite jumpUpSprite;
    public Sprite jumpDownSprite;
    
    private Rigidbody2D playerRigidbody;
    private Animator playerAnimator;
    private SpriteRenderer playerRenderer;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerRenderer = GetComponent<SpriteRenderer>();
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            playerRigidbody.AddForce(horizForce);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            playerRigidbody.AddForce(-horizForce);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        playerAnimator.SetFloat("HorizMove", playerRigidbody.velocity.x);
        //playerAnimator.speed = playerRigidbody.velocity.magnitude;
        if (didJump)
        {
            playerAnimator.enabled = false;
            if (playerRigidbody.velocity.y > 0) playerRenderer.sprite = jumpUpSprite;
            else playerRenderer.sprite = jumpDownSprite;
        }
    }

    private void Jump()
    {
        didJump = true;
        if (jumpCount < 2)
        {
            jumpCount++;
            playerRigidbody.AddForce(jumpForce);
        }
    }
}
