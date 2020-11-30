using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float xMin = 0, xMax = 0, yMin = 0, yMax = 0;
    private float zPos;
    public bool Follow {get;set;}
    private Transform playerTransform;
    private Vector3 velocity;
    private float smoothTime = 0.3F;

    void Start()
    {
        zPos = transform.position.z;
        Follow = false;
    }

    void Update()
    {
        if (Follow)
        {
            float xPos = Mathf.Clamp(playerTransform.position.x, xMin, xMax);
            float yPos = Mathf.Clamp(playerTransform.position.y, yMin, yMax);
            Vector3 targetPosition = new Vector3(xPos, yPos, zPos);
            // Camera follows lazily using smoothdamp
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }

    public void SetLimits(float xMin, float xMax, float yMin, float yMax)
    {
        this.xMin = xMin;
        this.xMax = xMax;
        this.yMin = yMin;
        this.yMax = yMax;
    }

    public void SetPlayer(Transform playerTransform)
    {
        this.playerTransform = playerTransform;
    }
}
