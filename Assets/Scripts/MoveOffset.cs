using UnityEngine;

public class MoveOffset : MonoBehaviour
{
	private Material currentMaterial;
	public float speed;
	public bool moveOnlyIfGameIsRunning = true;
	private float offset;

	void Start()
	{
		currentMaterial = GetComponent<Renderer>().material;
	}

	void Update()
	{
		if (moveOnlyIfGameIsRunning && !GameController.Instance.GameIsRunning && !GameController.Instance.WaitingForStart)
			return;

		offset += Time.deltaTime;
		currentMaterial.SetTextureOffset("_MainTex", new Vector2(offset * speed, 0));
	}
}
