using System;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    private GameState _currentState = GameState.WAITGAME;

    public Transform player;
    public Text textScore;

    public int Score { get; private set; } = 0;

    public int BestScore => PlayerPrefs.GetInt("Score");

    public GameState CurrentState => _currentState;

    public bool GameIsRunning => _currentState == GameState.INGAME;

    public bool WaitingForStart => _currentState == GameState.WAITGAME;

    void Start()
    {
        Instance = this;
    }

    public void Restart()
    {
        _currentState = GameState.WAITGAME;
        SpawmController.Instance.EnableColliders();
        SpawmController.Instance.Restart();
        Score = 0;
        textScore.text = Score.ToString();
        PlayerBehaviour.Instance.StartPlaying();
        UIController.Instance.ShowStartMenu();
    }

    public void StartGame()
    {
        _currentState = GameState.INGAME;
        UIController.Instance.HideStartMenu();
        PlayerBehaviour.Instance.OnClick();
    }

    public void AddScore()
    {
        Score++;
        SoundController.Instance.PlaySound(SoundGame.Point);
        textScore.text = Score.ToString();
    }

    private void SetBestScore(int bestScore)
    {
        PlayerPrefs.SetInt("Score", bestScore);
    }

    public void CallGameOver()
    {
        _currentState = GameState.GAMEOVER;
        SpawmController.Instance.DisableColliders();
        UIController.Instance.ShowGameOverScreen();
        if (Score > BestScore)
            SetBestScore(Score);
    }

    public void Pause()
    {
        _currentState = GameState.PAUSE;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        _currentState = GameState.INGAME;
        Time.timeScale = 1;
    }
}
