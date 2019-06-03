using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    public string horizontalAxisStr = "Horizontal";
    public string jumpStr = "Jump";

    public float moveHoriz { get; private set; }
    public bool jump { get; private set; }
    
    void Update()
    {
        if (GameManager.instance != null && GameManager.instance.isGameover)
        {
            moveHoriz = 0;
            jump = false;
            return;
        }

        moveHoriz = Input.GetAxis(horizontalAxisStr);
        jump = Input.GetButton(jumpStr);
    }
}
