using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }
    
    private void Update()
    {
        Vector3 point = cam.WorldToViewportPoint(player.position);
        Vector3 delta = new Vector3(player.position.x, 0, -10) - cam.ViewportToWorldPoint(new Vector2(0.5f, 0.5f));
        Vector3 destination = transform.position + delta;
        transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
    }
}
