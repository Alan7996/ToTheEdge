using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 jumpForce = new Vector2(0, 350);
    private int jumpCount = 0;
    private bool didJump = false;
    private bool hurt = false;
    private bool atDoor = false;

    private int cherryCount = 0;
    private int gemCount = 0;

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
        // Reset motion and return if game over or stage cleared
        if (GameManager.instance.isGameover)
        {
            playerRigidbody.gravityScale = 0;
            playerRigidbody.velocity = Vector2.zero;
            playerAnimator.SetBool("NotDead", false);
            return;
        }
        else if (GameManager.instance.isStageclear)
        {
            playerRigidbody.gravityScale = 0;
            playerRigidbody.velocity = Vector2.zero;
            return;
        }

        // During hurt animation, no inputs allowed
        if (hurt) return;

        // Space to jump and UpArrow to enter doors
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        } else if (atDoor && Input.GetKeyDown(KeyCode.UpArrow))
        {
            PlayerPrefs.SetInt("Cherry", cherryCount);
            PlayerPrefs.SetInt("Gem", gemCount);
            GameManager.instance.LoadNextLevel();
        }

        // Left Right smooth movements
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

        // Alter animation speed based on movement speed
        float absVelX = Mathf.Abs(playerRigidbody.velocity.x);
        playerAnimator.SetFloat("HorizMove", absVelX);
        playerAnimator.speed = (absVelX != 0 && absVelX != 5) ? 0.5f : 1;

        // During jump motion, disabled animation and just use sprite renderer
        if (didJump)
        {
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
        playerAnimator.enabled = false;
    }

    // Toggles "hurt" boolean
    private void ToggleHurt()
    {
        hurt = !hurt;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" && collision.collider.GetType().IsEquivalentTo(typeof(BoxCollider2D)))
        {
            didJump = false;
            jumpCount = 0;
            playerAnimator.enabled = true;
        }
        else if (collision.gameObject.tag == "Spike" && !hurt)
        {
            hurt = true;
            playerAnimator.enabled = true;
            playerAnimator.SetTrigger("Hurt");
            Invoke("ToggleHurt", 1);
            GameManager.instance.LoseHealth();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Cherry")
        {
            cherryCount++;
            GameManager.instance.AddScore(100);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Gem")
        {
            gemCount++;
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
