using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : State
{
    public override void HandleInput(PlayerInput thisInput)
    {
        //TODO: May need to add dead zone to movement
        if (thisInput.HorizontalInput != 0.0f)
        {
            Move(thisInput.HorizontalInput);
        }else
        {
            //change to idle state
            player.ChangeState("idle");
        }

        if (!IsGrounded())
        {
            player.PlayerAnimator.SetTrigger("jump");
            player.ChangeState("jump");
        }
        else if (thisInput.JumpInput)
        {
            Jump();
            player.ChangeState("jump");
        }
        else if (thisInput.FireInput)
        {
            player.ChangeState("shoot");
        }
    }

    public override void Entry()
    {
        player.PlayerAnimator.ResetTrigger("shoot");

    }
}
