using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    //Assigned in editor
    public CheckPoint playerStart;

    //References to level components
    CameraTrigger[] cameraTriggers;
    Switchable[] switchables;
    Enemy[] enemies;
    CheckPoint[] checkPoints;
    Resettable[] resettables;

    private CheckPoint latestCheckPoint;
    private StatTracker statTracker;

    //References outside level scope
    CameraController mainCamera;
    Player player;

    void Start()
    {
        cameraTriggers = GetComponentsInChildren<CameraTrigger>();
        switchables = GetComponentsInChildren<Switchable>();
        enemies = GetComponentsInChildren<Enemy>();
        checkPoints = GetComponentsInChildren<CheckPoint>();
        resettables = GetComponentsInChildren<Resettable>();

        foreach(CheckPoint cp in checkPoints)
        {
            cp.ParentLevel = this;
        }
    }

    public void SetupLevel(CameraController camera, Player player)
    {
        this.mainCamera = camera;
        //Pass the camera reference to each trigger
        foreach (CameraTrigger trigger in cameraTriggers)
        {
            trigger.MainCamera = mainCamera;
        }

        latestCheckPoint = playerStart;
        this.player = player;
        this.player.transform.position = latestCheckPoint.Position;
        this.player.Death += new Player.PlayerEventHandler(ResetLevel);
        this.statTracker = new StatTracker();
        this.statTracker.StartRun();
    }

    void Update()
    {
        if (statTracker != null)
        {
            statTracker.CurrentTimer += Time.deltaTime;
        }
    }

    public void SetCheckPoint(CheckPoint cp, bool final = false)
    {
        this.latestCheckPoint = cp;

        if (final)
        {
            this.statTracker.GameOver();
        }else
        {
            this.statTracker.NewCheckPoint();
        }

    }

    public void ResetLevel()
    {
        this.player.transform.position = latestCheckPoint.Position;
        this.player.AttachedRigidBody.velocity = Vector3.zero;
        this.statTracker.CurrentDeaths++;
        foreach(Resettable resettable in resettables)
        {
            resettable.Reset();
        }
    }
}
