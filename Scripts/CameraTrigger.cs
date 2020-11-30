using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    //These positional limits to be set in the inspector when building the level
    public float xMax, xMin, yMax, yMin;

    public CameraController MainCamera { private get; set; } // This is passed to all camera triggers by level on startup

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            MainCamera.SetLimits(xMin, xMax, yMin, yMax);
        }
    }
}
