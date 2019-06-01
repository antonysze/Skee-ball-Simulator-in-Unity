using UnityEngine;

public class GSWaitForInput : GameState {

    public override void StateUpdate() {
        if (InputManager._instance.inputState == 1)
            GameManager._instance.changeState(new GSHoldingDown(GameManager._instance.forceReachMaxTime));
    }
}