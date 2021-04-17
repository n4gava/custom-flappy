using UnityEngine;
using System.Collections;


public class GameControllerOld : MonoBehaviour {

	public Transform player;

	private Vector3 startPositionPlayer;
	private GameState currentState = GameState.START;

	public TextMesh numberScore;
	public TextMesh shadowScore;

	public float timeToRestart;
	private float currentTimeToRestart;
	private GameOverControllerOld gameOverController;
    private StartMenuController startMenuController;

    private int score = 0;

	// Use this for initialization
	void Start () 
	{
		startPositionPlayer = player.position;
		gameOverController = FindObjectOfType (typeof(GameOverControllerOld)) as GameOverControllerOld;
        startMenuController = FindObjectOfType(typeof(StartMenuController)) as StartMenuController;
    }
	
	// Update is called once per frame
	void Update () 
	{
		switch (currentState) {
		case GameState.START:
			{
				player.position = startPositionPlayer;
				currentState = GameState.WAITGAME;
				score = 0;
			}
			break;
		case GameState.WAITGAME:
			{
				player.position = startPositionPlayer;
			}
			break;
		case GameState.INGAME:
			{
				numberScore.text = score.ToString();
				shadowScore.text = score.ToString();
			}
			break;
		case GameState.RANKING:
			{
				//player.position = startPositionPlayer;
				numberScore.GetComponent<Renderer>().enabled = false;
				shadowScore.GetComponent<Renderer>().enabled = false;
			}
	    break;
		case GameState.GAMEOVER:
			{
			    currentTimeToRestart += Time.deltaTime;
				
				if (currentTimeToRestart > timeToRestart)
				{
					currentTimeToRestart = 0;
					currentState = GameState.RANKING;
					numberScore.GetComponent<Renderer>().enabled = false;	
					shadowScore.GetComponent<Renderer>().enabled = false;
					numberScore.text = score.ToString();
					shadowScore.text = score.ToString();
					gameOverController.SetGameOver(score);
				}
			}
			break;
		}

	}

	public void StartGame()
	{
		currentState = GameState.INGAME;
		score = 0;
		numberScore.GetComponent<Renderer>().enabled = true;
		shadowScore.GetComponent<Renderer>().enabled = true;
		gameOverController.HideGameOver ();
        gameOverController.HideGameOver();
        startMenuController.HideStartMenu();
    }

	public GameState GetCurrentState()
	{
		return currentState;
	}

	public void CallGameOver()
	{
		if (currentState != GameState.GAMEOVER)
			SoundController.Instance.PlaySound(SoundGame.Hit);

		currentState = GameState.GAMEOVER;
	}

	public void Restart()
	{
		player.position = startPositionPlayer;
		player.GetComponent<PlayerBehaviourOld> ().ResetRotation ();

		ObstaclesBehaviour[] pipes = FindObjectsOfType(typeof(ObstaclesBehaviour)) as ObstaclesBehaviour[];

		foreach (ObstaclesBehaviour o in pipes) 
		{
			o.gameObject.SetActive(false);	
		}
	}

	public void AddScore()
	{
		score++;
		SoundController.Instance.PlaySound(SoundGame.Point);
	}
}
