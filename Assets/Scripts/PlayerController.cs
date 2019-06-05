using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 jumpForce = new Vector2(0, 350);
    private int jumpCount = 0;
    private bool didJump = false;
    private bool atDoor = false;

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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        } else if (atDoor && Input.GetKeyDown(KeyCode.UpArrow))
        {
            GameManager.instance.LoadNextLevel();
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.localScale = new Vector2(5, 5);
            playerRigidbody.velocity = new Vector2(6, playerRigidbody.velocity.y);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.localScale = new Vector2(-5, 5);
            playerRigidbody.velocity = new Vector2(-6, playerRigidbody.velocity.y);

        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            playerRigidbody.velocity = new Vector2(1, playerRigidbody.velocity.y);
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            playerRigidbody.velocity = new Vector2(-1, playerRigidbody.velocity.y);

        }

        float absVelX = Mathf.Abs(playerRigidbody.velocity.x);
        playerAnimator.SetFloat("HorizMove", absVelX);
        playerAnimator.speed = (absVelX != 0 && absVelX != 5) ? 0.5f : 1;
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
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0);
            playerRigidbody.AddForce(jumpForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            didJump = false;
            jumpCount = 0;
            playerAnimator.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Cherry")
        {
            GameManager.instance.AddScore(100);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Gem")
        {
            GameManager.instance.AddScore(1000);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Door")
        {
            atDoor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Door")
        {
            atDoor = false;
        }
    }
}
