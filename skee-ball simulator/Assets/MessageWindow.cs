using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageWindow : MonoBehaviour
{

    private Animator animator;
    public Text scoreText;
    public Text rankText;
    public Text messageText;
    public GameObject tryAgainBtn;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    

    public void updateText(int score, int rank, string message) {
        scoreText.text = "Score\n" + score.ToString();
        if (rank>0)
            rankText.text = "Rank: " + rank.ToString();
        else
            rankText.text = "Unranked";
        messageText.text = message;
    }

    public void showWindow(bool show) {
        animator.Play(show?"MessageBox_Show":"MessageBox_Hide",0,0);
    }

    public void showTryAgainBtn(bool show) {
        tryAgainBtn.SetActive(show);
    }
}
