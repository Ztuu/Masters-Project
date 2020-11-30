using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatTracker
{
    public int CurrentDeaths { get; set; }
    public float CurrentTimer { get; set; }
    private int totalDeaths;
    private float totalTime;
    private int sectionNum = 0;

    public StatTracker()
    {
        CurrentDeaths = 0;
        CurrentTimer = 0.0f;
    }
    public void StartRun()
    {
        StatLogger.GetInstance().WriteToFile("Starting new run");
        sectionNum = 0;
    }

    public void NewCheckPoint()
    {
        if(sectionNum != 0){
        LogDetails();
        totalDeaths += CurrentDeaths;
        totalTime += CurrentTimer;
        CurrentDeaths = 0;
        CurrentTimer = 0.0f;
        }

        sectionNum++;
    }

    public void GameOver()
    {
        NewCheckPoint();
        string output = string.Format("Run Completed\nTotal Time required {0}\nTotal Deaths {1}\n\n", totalTime, totalDeaths);
        StatLogger.GetInstance().WriteToFile(output);
    }

    void LogDetails()
    {
        string output = string.Format("Section {2} Complete\nTime required {0}\nDeaths {1}\n\n", CurrentTimer, CurrentDeaths, sectionNum);
        StatLogger.GetInstance().WriteToFile(output);
    }
}
