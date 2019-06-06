using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OscillatingObjectController : MonoBehaviour
{
    private Vector2 startPos;
    public Vector2 targetPos;
    public float speed = 1;
    private bool startToTargetBool = true;
    
    void Start()
    {
        startPos = transform.position;
    }
    
    void Update()
    {
        float step = speed * Time.deltaTime;

        if ((Vector2)transform.position == targetPos) startToTargetBool = false;
        else if ((Vector2)transform.position == startPos) startToTargetBool = true;

        if (startToTargetBool) transform.position = Vector2.MoveTowards(transform.position, targetPos, step);
        else transform.position = Vector2.MoveTowards(transform.position, startPos, step);
    }
}
