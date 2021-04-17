using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenuController : MonoBehaviour
{
    public string gameScene;

    public void OnClickStart()
    {
        SceneManager.LoadScene(gameScene);
    }
}
