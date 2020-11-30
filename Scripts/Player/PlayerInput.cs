using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput
{
    public float HorizontalInput { get; set; }
    public bool JumpInput { get; set; }
    public bool FireInput { get; set; }

    public PlayerInput()
    {
        HorizontalInput = 0.0f;
        JumpInput = false;
        FireInput = false;
    }
}
