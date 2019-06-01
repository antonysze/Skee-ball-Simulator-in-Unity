using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour{
    public static int currentLevel = 0;
    public int numberOfLevel;

    public Animator transitionCover;

    void Start() {
        for (int i = 0; i < numberOfLevel; i++) {
            RankingManager.loadRankLocaly(i+1);
            int[] rank = RankingManager.localScoreRank;
            MenuManager._instance.createLeaderboardObj(rank, i+1);
        }
    }

    public void enterLevel(int i) {
        currentLevel = i;
        transitionCover.Play("TransitionCover_fadeIn");
        SceneManager.LoadScene("GameScene"+i, LoadSceneMode.Single);
    }
}