
public abstract class GameState {
    protected float stateTimer;
    public abstract void StateUpdate();
    public void setTimer(float time) {stateTimer = time;}
}