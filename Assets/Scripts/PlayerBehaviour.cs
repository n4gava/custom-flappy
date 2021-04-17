using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerBehaviour : MonoBehaviour
{
    public static PlayerBehaviour Instance;

    private Vector3 _playerStartPosition;
    public Transform playerTransform;
    public Animator animator;
    public Rigidbody2D rigidbody2d;
    public float flyForce;
    public float upAngle = 30;
    public float downAngle = 330;


    public float downRotationVelocity;
    public float upRotationVelocity;
    public float maxHeight;
    public bool moveOnlyIfGameIsRunning = true;

    public bool IsFalling => !IsGoingUp;

    public bool IsGoingUp => rigidbody2d.velocity.y > 0;

    public bool IsFlying { get; private set; }

    void Start()
    {
        Instance = this;
        _playerStartPosition = playerTransform.position;
    }

    public void StartPlaying()
    {
        playerTransform.position = _playerStartPosition;
        playerTransform.rotation = Quaternion.identity;
        rigidbody2d.constraints = RigidbodyConstraints2D.None;
    }

    public void FreezePlayer()
    {
        rigidbody2d.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void UnfreezePlayer()
    {
        rigidbody2d.constraints = RigidbodyConstraints2D.None;
    }

    void Update()
    {
        if (moveOnlyIfGameIsRunning && GameController.Instance.WaitingForStart)
            this.transform.position = _playerStartPosition;

        if (moveOnlyIfGameIsRunning && !GameController.Instance.GameIsRunning)
            return;

        var touching = IsTouchingOnScreen();

        if (touching)
        {
            OnClick();
        }

        UpdateAngle();
        VerifyMaxHeight();
    }

    public void OnClick()
    {
        StartCoroutine(Fly());
    }

    private void VerifyMaxHeight()
    {
        var positionPlayer = transform.position;
        if (positionPlayer.y > maxHeight)
        {
            positionPlayer.y = maxHeight;
            transform.position = positionPlayer;
        }
    }

    private void UpdateAngle()
    {
        var angleDirection = IsGoingUp ? Vector3.forward : Vector3.back;
        var angleVelocity = IsGoingUp ? upRotationVelocity : downRotationVelocity;
        playerTransform.eulerAngles += angleDirection * angleVelocity * Time.deltaTime;


        var angleExceeded = playerTransform.eulerAngles.z < downAngle && playerTransform.eulerAngles.z > upAngle;

        if (angleExceeded)
            playerTransform.eulerAngles = Vector3.forward * (IsGoingUp ? upAngle : downAngle);
    }

    private IEnumerator Fly()
    {
        IsFlying = true;
        SoundController.Instance.PlaySound(SoundGame.Wing);
        animator.SetBool("callFly", true);
        rigidbody2d.velocity = Vector2.zero;
        rigidbody2d.AddForce(Vector2.up * flyForce);
        yield return new WaitForSeconds(.1f);
        animator.SetBool("callFly", false);
        IsFlying = false;
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (GameController.Instance.GameIsRunning)
        {
            SoundController.Instance.PlaySound(SoundGame.Hit);
            GameController.Instance.CallGameOver();
        }
    }

    private bool IsTouchingOnScreen()
    {
        var isTouching = Input.GetMouseButtonDown(0) || Input.touchCount > 0;

        if (isTouching && EventSystem.current.IsPointerOverGameObject())
            return false;

        return isTouching;
    }
}
