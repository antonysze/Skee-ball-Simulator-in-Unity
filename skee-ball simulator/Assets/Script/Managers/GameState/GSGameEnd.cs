using UnityEngine;

public class GSGameEnd : GameState {
    public GSGameEnd() {
        int rank = RankingManager.checkRank(GameManager._instance.score);
        
    }

    public override void StateUpdate() {
        
    }
}