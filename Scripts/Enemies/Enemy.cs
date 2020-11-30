using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, Resettable
{
    protected Vector3 initialPosition;
    protected int maxHealth, health;
    protected int damage=1;
    protected bool active = false;
    protected SpriteRenderer spriteRenderer;


    //Should be called every frame
    protected abstract void Act();

    //Should be called in start method
    protected void SetInitialPosition()
    {
        initialPosition = transform.position;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            if (spriteRenderer.isVisible)
            {
                GameObject tempProjectile = (GameObject)Instantiate(Resources.Load("Death"), transform.GetChild(0).position, Quaternion.identity);
            }
            transform.GetChild(0).gameObject.SetActive(false);
            active = false;

        }
    }

    public virtual void Reset()
    {
        //Bring back to life and return to initial position
        transform.GetChild(0).position = initialPosition;
        health = maxHealth;
        transform.GetChild(0).gameObject.SetActive(true);
        StartCoroutine(StayInactive());
    }

    IEnumerator StayInactive()
    {
        float timer = 0.0f;
        while(timer < 0.5f)
        {
            active = false;
            timer += Time.deltaTime;
            yield return null;
        }
    }

    protected void SetupHurtBoxes()
    {
        foreach(HurtBox hurtBox in GetComponentsInChildren<HurtBox>())
        {
            hurtBox.SetDamage(damage);
        }
    }
}
