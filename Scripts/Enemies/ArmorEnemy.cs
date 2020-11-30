using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorEnemy : Enemy
{
    protected int direction = -1; //Enemies move from right to left
    Transform body;

    protected override void Act()
    {
        RaycastHit2D hit = Physics2D.Raycast(body.position + direction * new Vector3(0.46f, 0.0f, 0.0f),
            direction * transform.right,
            0.1f);

        if (hit && !(hit.collider.CompareTag("Player") || hit.collider.CompareTag("Projectile")))
        {
            body.Rotate(new Vector2(0.0f, 180.0f));
            direction *= -1;
        }

        body.position += direction * new Vector3(2.0f, 0.0f, 0.0f) * Time.deltaTime;
    }

    void Start()
    {
        body = transform.GetChild(0);
        initialPosition = body.position;
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
        if(direction == 1)
        {
            body.Rotate(new Vector2(0.0f, 180.0f));
        }
        direction = -1;
    }

}
