using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : State
{

    private bool canLand = true;
    private bool canDoubleJump;
    private bool canAirShoot;

    public override void HandleInput(PlayerInput thisInput)
    {
        
        bool landed = IsGrounded(true );
        if (!canLand) { landed = false; }

        if (thisInput.HorizontalInput != 0.0f)
        {
            Move(thisInput.HorizontalInput);
            if (landed)
            {
                player.PlayerAnimator.SetBool("landed", true);
                player.ChangeState("run");
            }
        }
        else if (landed)
        {
            player.PlayerAnimator.SetBool("landed", true);
            player.ChangeState("idle");
        }

        if (!landed)
        {
            if (thisInput.JumpInput && canDoubleJump)
            {
                DoubleJump();
            } else if (thisInput.FireInput && canAirShoot)
            {
                this.canAirShoot = false;
                player.PlayerAnimator.SetTrigger("shoot");
                Shoot(transform.right * (int)playerDirection,
                    new Vector3(-1.0f * (int)playerDirection, 1.0f, 0.0f));
            }
        }
    }

    public override void Entry()
    {
        StartCoroutine(PreventLanding());
        canDoubleJump = true;
        canAirShoot = true;
        player.PlayerAnimator.SetBool("landed", false);
    }

    //Stops the player immediately exiting jump state after jumping
    IEnumerator PreventLanding()
    {
        canLand = false;
        yield return new WaitForSeconds(0.4f);
        canLand = true;
    }

    private void DoubleJump()
    {
        StartCoroutine(PreventLanding());
        canDoubleJump = false;
        Jump();
        Shoot(transform.up * -1.0f);
        player.PlayerAnimator.SetTrigger("jump");
    }
}
