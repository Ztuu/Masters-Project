using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private GameController gameController;
    public GameObject uiElements;

    public void Show()
    {
        uiElements.SetActive(true);
    }

    public void Hide()
    {
        uiElements.SetActive(false);
    }
    
    public void SetGameController(GameController gc)
    {
        this.gameController = gc;
    }

    public void StartButtonClicked(bool intuitive)
    {
        gameController.StartGame(intuitive);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
