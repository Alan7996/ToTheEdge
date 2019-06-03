using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    public Transform player;
    private float width;
    
    void Start()
    {
        BoxCollider2D backgroundCollider = GetComponent<BoxCollider2D>();
        width = backgroundCollider.size.x * 5;
    }
    
    void Update()
    {
        if (transform.position.x < player.position.x - width)
        {
            RepositionRight();
        }
        else if (transform.position.x > player.position.x + width)
        {
            RepositionLeft();
        }
    }

    private void RepositionRight()
    {
        Vector2 offset = new Vector2(width * 2f, 0);
        transform.position = (Vector2)transform.position + offset;
    }

    private void RepositionLeft()
    {
        Vector2 offset = new Vector2(-width * 2f, 0);
        transform.position = (Vector2)transform.position + offset;
    }
}
