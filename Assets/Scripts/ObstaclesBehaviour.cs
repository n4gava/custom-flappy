using UnityEngine;
using System.Collections;

public class ObstaclesBehaviour : MonoBehaviour 
{
	private bool _passed;
	public float speed;
	public float screenLimit;
	public BoxCollider2D[] boxColliders;
	

	void OnEnable()
	{
		_passed = false;
	}

	// Update is called once per frame
	void Update () {

		if (!GameController.Instance.GameIsRunning)
			return;

		transform.position += new Vector3 (speed, 0, 0) * Time.deltaTime;

		if (transform.position.x < screenLimit) {
			gameObject.SetActive(false);
		}

		if (transform.position.x < GameController.Instance.player.position.x && !_passed ) 
		{
			_passed = true;
			GameController.Instance.AddScore();
		}
	}

	public void DisableColliders()
    {
		foreach (var boxCollider in boxColliders)
		{
			boxCollider.enabled = false;
		}
	}

	public void EnableCollider()
    {
        foreach (var boxCollider in boxColliders)
        {
			boxCollider.enabled = true;
		}
    }
}
