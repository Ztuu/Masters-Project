using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public float x, y, z; //The player spawn position is set in inspector
    public bool final = false; //Is this the end of the level?

    public Vector3 Position { get; private set;} //Public get so that level knows where to send player

    public Level ParentLevel { private get; set; }

    void Start()
    {
        Position = new Vector3(x, y, z);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ParentLevel.SetCheckPoint(this, final);
            Destroy(gameObject);
        }
    }
}
