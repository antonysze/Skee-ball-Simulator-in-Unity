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
        if (ballTrans.position.y < disappearHeight) {
            stateTimer += Time.deltaTime;
        }
        if (Input.GetKeyDown("r") || stateTimer > timeLimit) {
            UIManager._instance.updateChargingBar(0);
            GameManager gm = GameManager._instance;
            if (gm.gameEnded) {
                gm.changeState(null);
            } else {
                gm.resetBall();
                gm.changeState(new GSWaitForInput());
            }
        }
    }
}