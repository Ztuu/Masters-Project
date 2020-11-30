using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pit : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().TakeDamage(99);
        }else if (other.CompareTag("Enemy"))
        {
            other.GetComponentInParent<Enemy>().TakeDamage(99);
        }
    }
}
