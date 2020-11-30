using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    public GameObject projectile;

    protected static Player player;
    protected Vector3 playerSizeOffset = new Vector3(0.0f, -0.51f, 0.0f);
    protected Vector3 horizontalRayOffset = new Vector3(0.25f, 0.0f, 0.0f);
    private float speed = 5.0f;

    protected enum Direction { LEFT=-1, RIGHT=1, };
    protected static Direction playerDirection = Direction.RIGHT;

    public abstract void HandleInput(PlayerInput thisInput);
    public abstract void Entry();

    public static void AssignPlayer(Player activePlayer)
    {
        player = activePlayer;
    }

    protected void Move(float moveInput)
    {
        Vector3 moveVec = new Vector3(moveInput, 0.0f, 0.0f) * Time.deltaTime * speed;
        transform.position += moveVec;

        //Ignore if move input is 0. Don't want to change the player's direction if they don't input anything
        if (moveInput > 0.0f)
        {
            playerDirection = Direction.RIGHT;
            player.PlayerSprite.flipX = false;
        }else if (moveInput < 0.0f)
        {
            playerDirection = Direction.LEFT;
            player.PlayerSprite.flipX = true;
        }
        player.PlayerAnimator.SetBool("running", true);
    }

    protected void Jump()
    {
        player.AttachedRigidBody.velocity = Vector3.zero;
        player.AttachedRigidBody.AddForce(Vector2.up * 10.0f, ForceMode2D.Impulse);
        player.PlayerAnimator.SetTrigger("jump");
        player.PlayerAudio.PlayOneShot(player.jumpSound);

    }

    protected void Shoot(Vector3 direction)
    {
        //Instantiate the projectile
        GameObject tempProjectile = (GameObject)Instantiate(Resources.Load("Projectile"),
            transform.position + direction * 0.3f,
             Quaternion.identity);
        //Set the direction of the projectile
        tempProjectile.GetComponent<Projectile>().SetDirection(direction);
        player.PlayerAudio.PlayOneShot(player.shootSound);
    }

    protected void Shoot(Vector3 direction, Vector3 knockback)
    {
        GameObject tempProjectile = (GameObject)Instantiate(Resources.Load("Projectile"),
            transform.position + direction * 0.55f,
             Quaternion.identity);
        tempProjectile.GetComponent<Projectile>().SetDirection(direction);
        //Add knockback
        player.AttachedRigidBody.velocity = Vector3.zero;
        player.AttachedRigidBody.AddForce(knockback * 8.0f, ForceMode2D.Impulse);
        player.PlayerAudio.PlayOneShot(player.shootSound);
    }

    protected bool IsGrounded(bool airborne = false)
    {
        if (airborne)
        {
            //When player is jumping dont send out side rays to prevent walljumping
            RaycastHit2D hit1 = Physics2D.Raycast(transform.position + playerSizeOffset,
             -0.05f * transform.up, 1.0f);
            return hit1;
        }
        else
        {
            //Send out 3 rays to detect if player on ground
            //One ray in middle and one at either edge. Allows player to jump when half off a platform
            RaycastHit2D hit1 = Physics2D.Raycast(transform.position + playerSizeOffset,
                -0.05f * transform.up, 1.0f);
            RaycastHit2D hit2 = Physics2D.Raycast(transform.position + playerSizeOffset + horizontalRayOffset,
                -0.05f * transform.up, 1.0f);
            RaycastHit2D hit3 = Physics2D.Raycast(transform.position + playerSizeOffset - horizontalRayOffset,
                -0.05f * transform.up, 1.0f);
            return hit1 || hit2 || hit3;
        }

    }
}
