using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 direction;
    private float speed = 10.0f;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
    }

    void Update()
    {
        if (!spriteRenderer.isVisible)
        {
            // Despawn projectiles that move off screen
            Destroy(gameObject);
        }
        transform.position += direction * Time.deltaTime * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Switch"))
        {
            collision.gameObject.GetComponent<Switch>().SwitchState();
        }else if (collision.collider.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponentInParent<Enemy>().TakeDamage(1);
        }

        if (!collision.collider.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
