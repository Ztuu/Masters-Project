using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public override void HandleInput(PlayerInput thisInput)
    {
        //TODO: May need to add dead zone to movement
        if (thisInput.HorizontalInput != 0.0f)
        {
            Move(thisInput.HorizontalInput);
            player.ChangeState("run");
        }

        if (!IsGrounded())
        {
            player.ChangeState("jump");
        }
        else if (thisInput.JumpInput)
        {
            Jump();
            player.ChangeState("jump");
        }else if (thisInput.FireInput)
        {
            player.ChangeState("shoot");
        }
    } 
    
    public override void Entry()
    {
        player.PlayerAnimator.SetBool("running", false);
        player.PlayerAnimator.ResetTrigger("shoot");
    }   
}
