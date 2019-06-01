using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{   
    public static GameManager _instance;

    [Header("Game Setting")]
    public float startWaitTime = 3;
    public float gameTimeLimit = 30;
    private float gameTimer;
    private bool gameRunning = false;
    
    [HideInInspector] public bool gameEnded = false;

    [Header("Ball Setting")]
    public GameObject ball;
    private Rigidbody ballRigibody;
    private Material ballMaterial;
    public Transform ballStartPoint;
    public float horizontalBoundRadius;
    public float forceReachMaxTime;
    public Vector2 forceRange;
    public Vector2 weightRange;
    public Vector2 ballSizeRangeByWeight;
    public Vector2 bounsPointRange;
    public Gradient ballColorRangeByBounsPoint;
    public float ballDisappearTime;
    public float ballDisappearHeight;

    // [Header("Others")]
    private Vector2 horizontalBoundOnScreen;

    #region game value
    
    [HideInInspector] public int score = 0;
    private int currentBallBounsPoint;
    private bool ableToGetPoint;

    #endregion

    
    private float currentBallPoint;

    private GameState gameState;


    void Awake() {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        ballRigibody = ball.GetComponent<Rigidbody>();
        ballMaterial = ball.GetComponent<Renderer>().material;
        gameReset();
        Camera cam = Camera.main;
        horizontalBoundOnScreen.x = cam.WorldToScreenPoint(ballStartPoint.position-new Vector3(horizontalBoundRadius,0)).x;
        horizontalBoundOnScreen.y = cam.WorldToScreenPoint(ballStartPoint.position+new Vector3(horizontalBoundRadius,0)).x;
    }

    // Update is called once per frame
    void Update()
    {
        InputManager._instance.InputUpdateManually(); //input update

        if (gameState != null)
            gameState.StateUpdate(); //state update

        //counting down the timer
        if (gameRunning) {
            gameTimer -= Time.deltaTime;
            if (gameTimer <= 0) { //times up!
                gameTimer = 0;
                gameRunning = false;
                gameEnded = true;
            }
            UIManager._instance.updateTimer(gameTimer); //update timer ui
        }
    }

    public void gameReset() {
        resetBall();
        score = 0;
        gameTimer = gameTimeLimit;
        gameRunning = false;
        gameEnded = false;
        changeState(new GSWaitForStart(startWaitTime));
    }

    public void startGame() {
        gameRunning = true;
    }

    public void changeState(GameState newState) {
        Debug.Log("[GameState] change state to: " + newState.GetType().Name);
        gameState = newState;
    }

    //move ball by user finger before shoot
    public void updateBallStartPos() {
        float inputX = InputManager._instance.inputPosition.x;
        inputX = Mathf.Clamp(inputX, horizontalBoundOnScreen.x, horizontalBoundOnScreen.y);
        inputX = (inputX-horizontalBoundOnScreen.x)/(horizontalBoundOnScreen.y-horizontalBoundOnScreen.x)-0.5f;
        //inputX range: [-0.5,0.5]
        ball.transform.position = ballStartPoint.position + new Vector3(horizontalBoundRadius*inputX*2,0);
    }

    //shoot the ball
    public void shootBall(float forceRate = 1) {
        ballRigibody.isKinematic = false;
        ballRigibody.AddForce(ballStartPoint.forward * Mathf.Lerp(forceRange.x, forceRange.y, forceRate),ForceMode.Impulse);
    }

    //reset ball to origial position
    public void resetBall() {
        ballRigibody.isKinematic = true;
        ball.transform.position = ballStartPoint.position;
        ableToGetPoint = true;


        float randomValue;

        //random weight(mass) of ball and size
        randomValue = Random.value;
        ball.transform.localScale = Vector3.one * (randomValue * (ballSizeRangeByWeight.y - ballSizeRangeByWeight.x) + ballSizeRangeByWeight.x);
        ballRigibody.mass = randomValue * (weightRange.y - weightRange.x) + weightRange.x;

        //random bouns point of ball and color
        randomValue = Random.value;
        ballMaterial.color = ballColorRangeByBounsPoint.Evaluate(randomValue);
        currentBallBounsPoint = Mathf.CeilToInt(randomValue * (bounsPointRange.y - bounsPointRange.x) + bounsPointRange.x);
    }

    public void ballIn(int scoreGain) {
        if (!ableToGetPoint) return; //prevent get score repeatly
        ableToGetPoint = false;

        scoreGain += currentBallBounsPoint;
        score += scoreGain;
        gameState.setTimer(ballDisappearTime-0.5f);
        UIManager._instance.updateScore(scoreGain, score);
        Debug.Log("[Game] Ball get in: score +" + scoreGain + " | current score: " + score);
    }

    public void goBackMainMenu() {
        UIManager._instance.showBlackOver(true);
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
