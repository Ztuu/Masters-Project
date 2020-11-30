using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringState : State
{
    bool finished = false;

    public override void HandleInput(PlayerInput thisInput)
    {
        if (finished)
        {
            player.ChangeState("idle");
        }
    }

    public override void Entry()
    {
        finished = false;
        player.PlayerAnimator.SetTrigger("shoot");
        StartCoroutine(ShootDelay());
    }

    private IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(0.5f);
        Shoot(transform.right * (int)playerDirection); //need to account for player direction
        yield return new WaitForSeconds(0.2f);
        finished = true;
    }
}
