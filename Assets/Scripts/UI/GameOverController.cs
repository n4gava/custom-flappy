using UnityEngine;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class GameOverController : MonoBehaviour
{
    private Vector3 _gameOverTextPosition;
    private Vector3 _gameOverWindowPosition;

    public Text score;
    public Text bestScore;  
    public GameObject gameOverText;
    public GameObject gameOverWindow;
    public GameObject[] buttons;
    public Vector3 textStartPosition;
    public Vector3 windowStartPosition;
    public Image medalImage;
    public Sprite[] medals;
    public int[] medalPoints;


    private void Start()
    {
        _gameOverTextPosition = gameOverText.transform.localPosition;
        _gameOverWindowPosition = gameOverWindow.transform.localPosition;
        HideGameOverScreen();
    }

    public void ShowGameOverScreen()
    {
        score.text = "0";
        bestScore.text = GameController.Instance.BestScore.ToString();
        StartAnimation();
    }

    public void HideGameOverScreen()
    {
        gameOverText.SetActive(false);
        gameOverWindow.SetActive(false);
        foreach (var button in buttons)
        {
            button.SetActive(false);
        }
    }

    private void StartAnimation()
    {
        gameOverText.transform.localPosition = textStartPosition;
        gameOverWindow.transform.localPosition = windowStartPosition;
        StartCoroutine(AnimationSequence());
    }

    private IEnumerator<WaitForSeconds> AnimationSequence()
    {
        gameOverText.SetActive(true);
        gameOverWindow.SetActive(true);
        SetCoin();
        var oldBestScore = GameController.Instance.BestScore;

        gameOverText.transform.DOLocalMove(_gameOverTextPosition, 1f).SetEase(Ease.OutBounce);
        yield return new WaitForSeconds(1.5f);
        SoundController.Instance.PlaySound(SoundGame.Menu);
        gameOverWindow.transform.DOLocalMove(_gameOverWindowPosition, 1.5f).SetEase(Ease.OutQuint);

        yield return new WaitForSeconds(1.3f);

        for (int i = 1; i <= GameController.Instance.Score; i++)
        {
            score.text = i.ToString();
            if (i > oldBestScore)
                bestScore.text = score.text;

            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(0.5f);
        foreach (var button in buttons)
        {
            button.SetActive(true);
        }
    }

    private void SetCoin()
    {
        for (int i = medalPoints.Length - 1; i >=0; i--)
        {
            if (GameController.Instance.Score > medalPoints[i])
            {
                medalImage.sprite = medals[i];
                return;
            }
        }
    }
}
