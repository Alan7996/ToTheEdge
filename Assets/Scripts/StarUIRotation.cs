using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarUIRotation : MonoBehaviour
{
    private Vector2 rotateHoriz = new Vector2(0, 360);

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotateHoriz * Time.deltaTime);
    }
}
