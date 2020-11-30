using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour, Resettable
{
    //Assigned in editor
    public GameObject targetObj;
    public Sprite onSprite, offSprite;

    private Switchable target;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;

    void Start()
    {
        target = targetObj.GetComponent<Switchable>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    public void SwitchState()
    {
        audioSource.Play();
        target.Switch();
        UpdateSprite();
    }

    public void SwitchToTargetState(bool targetState)
    {
        target.Switch(targetState);
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        if (target.GetCurrentState())
        {
            spriteRenderer.sprite = onSprite;
        }else
        {
            spriteRenderer.sprite = offSprite;
        }
    }

    public void Reset()
    {
        //UpdateSprite();
        spriteRenderer.sprite = offSprite;
    }
}
