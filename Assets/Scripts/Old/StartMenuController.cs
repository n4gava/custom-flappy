using UnityEngine;
using System.Collections;

public class StartMenuController : MonoBehaviour {

    public GameObject content;

    public void HideStartMenu()
    {
        content.SetActive(false);
    }
}
