using UnityEngine;

public class GSWaitForBallStop : GameState {
    private Transform ballTrans;
    private float disappearHeight;
    private float timeLimit;
    public GSWaitForBallStop(Transform ball, float disappearHeight, float timeLimit) {
        ballTrans = ball;
        this.timeLimit = timeLimit;
        this.disappearHeight = disappearHeight;
    }
    public override void StateUpdate() {
        //check ball disappeared
        if (ballTrans.position.y < disappearHeight) {
            stateTimer += Time.deltaTime;
        }

        //game ended
        GameManager gm = GameManager._instance;
        if (stateTimer > 0 && gm.gameEnded) {
            gm.changeState(new GSGameEnd());
            return;
        }

        //ball reset
        if (Input.GetKeyDown("r") || stateTimer > timeLimit) {
            UIManager._instance.updateChargingBar(0);
            gm.resetBall();
            gm.changeState(new GSWaitForInput());
    }
    }
}