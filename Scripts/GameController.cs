using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject playerPreFab;

    //References
    public MainMenu menu; //Currently assigned in inspector
    public CameraController mainCamera;
    private Player activePlayer;
    public GameObject controlLevel, intuitiveLevel;
    private Level activeLevel;
    private StatLogger logger;


    void Start()
    {
        //enable start menu
        menu.SetGameController(this);
        menu.Show();
        logger = StatLogger.GetInstance();
    }

    public void StartGame(bool intuitive)
    {
        menu.Hide();
        GameObject tempPlayer = Instantiate(playerPreFab, Vector3.zero, Quaternion.identity);
        activePlayer = tempPlayer.GetComponent<Player>();

        logger.SetFileName(System.DateTime.Now.ToShortDateString().Replace('/', '_'));
        logger.WriteToFile("Starting new run");

        if (intuitive)
        {
            intuitiveLevel.SetActive(true);
            controlLevel.SetActive(false);
            activeLevel = intuitiveLevel.GetComponent<Level>();
            activeLevel.SetupLevel(mainCamera, activePlayer);
            logger.WriteToFile("Level : Intuitive");
        }
        else
        {
            intuitiveLevel.SetActive(false);
            controlLevel.SetActive(true);
            activeLevel = controlLevel.GetComponent<Level>();
            activeLevel.SetupLevel(mainCamera, activePlayer);
            logger.WriteToFile("Level : Control");
        }

        mainCamera.SetPlayer(activePlayer.transform);
        mainCamera.Follow = true;
    }
}
