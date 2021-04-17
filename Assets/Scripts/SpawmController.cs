using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SpawmController : MonoBehaviour {
	public static SpawmController Instance { get; private set; }

	public float maxHeight;
	public float minHeight;
	public float rateSpawn;
	private float currentRateSpawn;

	public GameObject tubePrefab;
	public int maxSpawnTubes;

	public List<GameObject> tubes;


	void Start () {
		Instance = this;
		currentRateSpawn = rateSpawn;

		for (int i=0; i < maxSpawnTubes; i++) 
		{
			var tube = Instantiate<GameObject>(tubePrefab);
			tube.SetActive(false);
			tubes.Add(tube);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!GameController.Instance.GameIsRunning)
			return;

		currentRateSpawn += Time.deltaTime;
		if (currentRateSpawn > rateSpawn) 
		{
			currentRateSpawn = 0;
			Spawn();
		}
	}

	public void Restart()
    {
        foreach (var tube in tubes)
        {
			tube.SetActive(false);
			tube.transform.position = this.transform.position;
		}
    }

	private void Spawn()
	{
		float randHeight = Random.Range(minHeight, maxHeight);

		var tempTube = tubes.FirstOrDefault(t => t.activeSelf == false);

		if (tempTube != null) 
		{
			tempTube.transform.position = new Vector3(transform.position.x, randHeight, transform.position.z);
			tempTube.SetActive(true);
		}
	}

	public void DisableColliders()
    {
        foreach (var tube in tubes)
        {
			var obstacheBehaviour = tube.GetComponent<ObstaclesBehaviour>();
			if (obstacheBehaviour != null)
				obstacheBehaviour.DisableColliders();
		}
    }

	public void EnableColliders()
	{
		foreach (var tube in tubes)
		{
			var obstacheBehaviour = tube.GetComponent<ObstaclesBehaviour>();
			if (obstacheBehaviour != null)
				obstacheBehaviour.EnableCollider();
		}
	}
}
