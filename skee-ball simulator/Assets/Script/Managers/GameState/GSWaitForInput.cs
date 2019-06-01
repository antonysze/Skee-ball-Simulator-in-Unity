using UnityEngine;

public class GSWaitForInput : GameState {

    public override void StateUpdate() {
        //check is game ended
        if (GameManager._instance.gameEnded) {
            GameManager._instance.changeState(new GSGameEnd());
            return;
        }

        //change state if player finger down
        if (InputManager._instance.inputState == 1)
            GameManager._instance.changeState(new GSHoldingDown(GameManager._instance.forceReachMaxTime));
    }
}