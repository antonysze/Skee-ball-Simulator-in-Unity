using UnityEngine;

public class GSHoldingDown : GameState {

    private int forceChargeSign = 1;
    private float forceChargeMaxTime;
    private float forceRate;

    public GSHoldingDown(float forceReachMaxTime) {
        forceChargeMaxTime = forceReachMaxTime;
    }

    public override void StateUpdate() {
        if (InputManager._instance.inputState == 0) {
            GameManager gm = GameManager._instance;
            gm.shootBall(forceRate);
            gm.changeState(new GSWaitForBallStop(gm.ball.transform, gm.ballDisaapearTime, gm.ballDisaapearTime));
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
        GameManager._instance.updateBallStartPos();

        //update UI
        UIManager._instance.updateChargingBar(forceRate);
    }
    
}