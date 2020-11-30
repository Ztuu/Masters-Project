using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, Switchable
{
    public GameObject doorObject;
    private bool currentState; //True means switch has been triggered
    private bool initialState = false;

    void Start()
    {
        Reset();
    }

    public void Switch()
    {
        currentState = !currentState;
        UpdateState();
    }

    public void Switch(bool targetState)
    {
        currentState = targetState;
        UpdateState();
    }

    public bool GetCurrentState()
    {
        return currentState;
    }

    private void UpdateState()
    {
        doorObject.SetActive(!currentState); //True means switch triggered which means door disabled
    }

    public void Reset()
    {
        currentState = initialState;
        UpdateState();
    }
}
