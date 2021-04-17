using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIA : MonoBehaviour
{
    public PlayerBehaviour playerBehaviour;
    public float yClickPosition;



    void Update()
    {
        if (!playerBehaviour.IsFlying && this.transform.localPosition.y < yClickPosition)
        {
            playerBehaviour.OnClick();
        }
    }
}
