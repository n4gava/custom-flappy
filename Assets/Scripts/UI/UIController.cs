using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }

    public GameOverController gameOverController;
    public string menuScene;
    public RectTransform startMenu;

    private void Start()
    {
        Instance = this;
    }

    private void Update()
    {
        if (GameController.Instance.WaitingForStart && IsTouchingOnScreen())
            GameController.Instance.StartGame();
    }

    public void ShowStartMenu()
    {
        startMenu.gameObject.SetActive(true);
    }

    public void HideStartMenu()
    {
        startMenu.gameObject.SetActive(false);
    }

    public void ShowGameOverScreen()
    {
        gameOverController.ShowGameOverScreen();
    }

    public void HideGameOverScreen()
    {
        gameOverController.HideGameOverScreen();
    }

    public void OnClickStart()
    {
        HideGameOverScreen();
        GameController.Instance.Restart();
    }

    public void OnClickMenu()
    {
        SceneManager.LoadScene(menuScene);
    }

    public void OnClickPause()
    {
        GameController.Instance.Pause();
    }

    private bool IsTouchingOnScreen()
    {
        return Input.GetMouseButtonDown(0) || Input.touchCount > 0;
    }
}
