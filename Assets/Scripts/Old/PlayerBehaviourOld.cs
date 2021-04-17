using UnityEngine;
using System.Collections;

public class PlayerBehaviourOld : MonoBehaviour {

	public Transform mesh;
	public float forceFly;
	private Animator animatorPlayer;
	public float maxHeight;

	private float currentTimeToAnime;
	private bool inAnime = false;
	private GameControllerOld gameController;
	// Use this for initialization
	void Start () {
		animatorPlayer = mesh.GetComponent<Animator> ();
		gameController = FindObjectOfType (typeof(GameControllerOld)) as GameControllerOld;
	}

    void TapFlappy()
    {
        inAnime = true;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1) * forceFly);
        SoundController.Instance.PlaySound(SoundGame.Wing);
    }

    // Update is called once per frame
    void Update () {

		if (Input.GetMouseButtonDown(0))
		{
            /*
			if (gameController.GetCurrentState() == GameStates.WAITGAME)
			{
				Restart();
			}
            */

		    if (gameController.GetCurrentState() == GameState.INGAME)
			{
                TapFlappy();

			}
		}

		Vector3 positionPlayer = transform.position;

		if (positionPlayer.y > maxHeight) {
			positionPlayer.y = maxHeight;
			transform.position = positionPlayer;
		}


		if (gameController.GetCurrentState() != GameState.INGAME &&
		    gameController.GetCurrentState() != GameState.GAMEOVER)
		{
			GetComponent<Rigidbody2D>().gravityScale = 0;
			return;
		}
		else
		{
			GetComponent<Rigidbody2D>().gravityScale = 1;
		}

		if (inAnime) 
		{
			currentTimeToAnime += Time.deltaTime;
			if (currentTimeToAnime > 0.4f) 
			{
				currentTimeToAnime = 0;
				inAnime = false;
			}
		}

		animatorPlayer.SetBool("callFly", inAnime);

		if (gameController.GetCurrentState () == GameState.INGAME) {
			if (GetComponent<Rigidbody2D>().velocity.y < 0) {
					mesh.eulerAngles -= new Vector3 (0, 0, 2f);
					if (mesh.eulerAngles.z < 330 && mesh.eulerAngles.z > 30)
							mesh.eulerAngles = new Vector3 (0, 0, 330);
			} else if (GetComponent<Rigidbody2D>().velocity.y > 0) {
					mesh.eulerAngles += new Vector3 (0, 0, 2f);

					if (mesh.eulerAngles.z > 30)
						mesh.eulerAngles = new Vector3 (0, 0, 30);
			}
		} else if (gameController.GetCurrentState () == GameState.GAMEOVER) {
			mesh.eulerAngles -= new Vector3 (0, 0, 5f);
			if (mesh.eulerAngles.z < 270 && mesh.eulerAngles.z > 90)
				mesh.eulerAngles = new Vector3 (0, 0, 270);
		}
	}

	private void OnCollisionEnter2D(Collision2D coll) 
	{
		gameController.CallGameOver ();
	}

	private void OnTriggerEnter2D(Collider2D coll) 
	{
		gameController.CallGameOver ();
	}

	public void ResetRotation()
	{
		mesh.eulerAngles = new Vector3 (0, 0, 0);
	}

	public void Restart()
	{
		if (gameController.GetCurrentState() != GameState.GAMEOVER)
		{
			gameController.Restart();
			gameController.StartGame();
            TapFlappy();
        }
	}

}
