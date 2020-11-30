using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy
{

    protected int direction = -1; //Enemies move from right to left

    protected override void Act()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.GetChild(0).position + direction * new Vector3(0.41f, 0.0f, 0.0f),
            direction * transform.right,
            0.1f);

        if (hit && !(hit.collider.CompareTag("Player") || hit.collider.CompareTag("Projectile")))
        {
            direction *= -1;
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        transform.GetChild(0).position += direction * new Vector3(2.0f, 0.0f, 0.0f) * Time.deltaTime;
    }

    void Start()
    {
        initialPosition = transform.GetChild(0).position;
        damage = 1;
        SetupHurtBoxes();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        if (spriteRenderer.isVisible)
        {
            active = true;
        }
        if (active)
        {
            Act();
        }
    }

    public override void Reset()
    {
        base.Reset();
        direction = -1;
        spriteRenderer.flipX = false;
    }

}
