using UnityEngine;
using System.Collections;

public class GameOverControllerOld : MonoBehaviour {

	public TextMesh score;
	public TextMesh bestScore;
	public Renderer[] medals;
	public GameObject content;
	public GameObject title;

	private Animator animatorTitle;
	private Animator animatorContent;


	// Use this for initialization
	void Start () {
		//animatorTitle = title.GetComponent<Animator>();
		animatorContent = content.GetComponent<Animator>();
		HideGameOver ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetGameOver(int scoreInGame)
	{

		if (scoreInGame > PlayerPrefs.GetInt ("Score"))
			PlayerPrefs.SetInt("Score", scoreInGame);

		score.text = scoreInGame.ToString ();
		bestScore.text = PlayerPrefs.GetInt ("Score").ToString();

		if (scoreInGame > 5 && scoreInGame <= 10)
			medals [0].enabled = true;
		else if (scoreInGame > 10 && scoreInGame <= 20)
			medals [1].enabled = true;
		else if (scoreInGame > 20 && scoreInGame <= 35)
			medals [2].enabled = true;
		else if (scoreInGame > 35)
			medals [3].enabled = true;

		content.SetActive (true);
		title.SetActive (true);

	}

	public void HideGameOver()
	{
		content.SetActive (false);
		title.SetActive (false);


		foreach (Renderer medal in medals) 
		{
			medal.enabled = false;	
		}
	}
}
