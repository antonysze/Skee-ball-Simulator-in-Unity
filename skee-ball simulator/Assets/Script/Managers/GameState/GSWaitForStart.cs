using UnityEngine;

public class GSWaitForStart : GameState {

    public GSWaitForStart(float waitTime) {
        setTimer(waitTime);
    }
    public override void StateUpdate() {
        stateTimer -= Time.deltaTime;
        if (stateTimer < 0) {
            GameManager gm = GameManager._instance;
            gm.startGame();
            gm.changeState(new GSWaitForInput());
        }
    }
}