using UnityEngine;
using System.Collections;

public class MoveOffsetOld : MonoBehaviour {

	private Material currentMaterial;
	public float speed;
	private float offset;

	private GameControllerOld gameController;
	// Use this for initialization
	void Start () {
		gameController = FindObjectOfType (typeof(GameControllerOld)) as GameControllerOld;
		currentMaterial = GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
		if (gameController.GetCurrentState() != GameState.INGAME)
			return;

		offset += 0.001f;
		currentMaterial.SetTextureOffset("_MainTex", new Vector2(offset * speed, 0));
	}
}
