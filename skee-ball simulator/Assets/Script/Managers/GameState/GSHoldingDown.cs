using UnityEngine;

public class GSHoldingDown : GameState {

    private int forceChargeSign = 1;
    private float forceChargeMaxTime;
    private float forceRate;

    public GSHoldingDown(float forceReachMaxTime) {
        forceChargeMaxTime = forceReachMaxTime;
    }

    public override void StateUpdate() {
        //check if game is ended
        GameManager gm = GameManager._instance;
        if (gm.gameEnded) {
            gm.changeState(new GSGameEnd());
            return;
        }

        //check if player released the finger
        if (InputManager._instance.inputState == 0) {
            gm.shootBall(forceRate);
            gm.changeState(new GSWaitForBallStop(gm.ball.transform, gm.ballDisappearHeight, gm.ballDisappearTime));
            return;
        }

        //force shifting
        stateTimer += forceChargeSign * Time.deltaTime;
        if (stateTimer >= forceChargeMaxTime) {
            stateTimer = forceChargeMaxTime;
            forceChargeSign = -1;
        } else if (stateTimer <= 0) {
            stateTimer = 0;
            forceChargeSign = 1;
        }

        //calculate force
        forceRate = stateTimer/forceChargeMaxTime;
        forceRate *= forceRate;

        //move ball pos by input pos
        gm.updateBallStartPos();

        //update UI
        UIManager._instance.updateChargingBar(forceRate);
    }
    
}