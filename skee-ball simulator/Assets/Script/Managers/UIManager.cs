using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager _instance;

    [Header("Charging Bar")]
    public Image chargingBar;
    public Gradient chargingBarGradient;
    private float chargingBarMaxHeight;

    [Header("Score Displayer")]
    private int scoreStore;
    public Text scoreText;
    public Text addScoreText;
    public Animator addScoreAnimator;

    [Header("Time Limit")]
    public Text timerText;

    [Header("Message Window")]
    public MessageWindow messageWindow;



    void Awake() {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        chargingBarMaxHeight = chargingBar.rectTransform.sizeDelta.y;
        updateChargingBar(0);
        scoreText.text = "0";
    }

    public void updateChargingBar(float rate) {
        chargingBar.color = chargingBarGradient.Evaluate(rate);
        Vector2 size = chargingBar.rectTransform.sizeDelta;
        size.y = chargingBarMaxHeight * rate;
        chargingBar.rectTransform.sizeDelta = size;
    }

    public void updateScore(int addScore, int currentScore) {
        updateScoreDisplay();
        addScoreText.text = "+" + addScore.ToString();
        scoreStore = currentScore;
        addScoreAnimator.Play("ScoreDisplay_addScore",0, 0);
    }

    public void updateScoreDisplay() {
        scoreText.text = scoreStore.ToString();
    }

    public void updateTimerText(string txt) {
        timerText.text = txt;
    }

    public void updateTimer(float time) {
        timerText.text = "Time Left: " + Mathf.CeilToInt(time).ToString();
    }

    public void gameEndMessage(int score, int rank) {
        if (rank < 1) { //low score
            messageWindow.updateText(score, rank, "Your score is low. Try again.");
            messageWindow.showTryAgainBtn(true);
        } else if (rank <2) {
            messageWindow.updateText(score, rank, "Amazing! You got the highest score!");
        } else {
            messageWindow.updateText(score, rank, "Nice one!");
            messageWindow.showTryAgainBtn(true);
        }
        messageWindow.showWindow(true);
    }
}
