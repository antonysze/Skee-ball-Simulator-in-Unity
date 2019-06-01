using UnityEngine;

public class GSGameEnd : GameState {
    public GSGameEnd() {
        int score = GameManager._instance.score;
        int rank = RankingManager.checkRank(score);
        UIManager._instance.gameEndMessage(score,rank);
    }

    public override void StateUpdate() {
        
    }
}