using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingManager {
    private static int[] localScoreRank = new int[5];
    private static bool scoreLoaded = false;

    public static void loadRankLocaly() {
        scoreLoaded = true;
        for (int i = 0; i < 5; i ++) {
            localScoreRank[i] = PlayerPrefs.GetInt("Level"+LevelManager.currentLevel+"_Rank"+i, 0);
        }
    }

    public static int checkRank(int score) {
        // Debug.Log(scoreLoaded);
        if (!scoreLoaded) loadRankLocaly();

        int rank = -1;
        for (int i = 0; i < 5; i ++) {
            if (localScoreRank[i] <= score) { //replace the rank of lower score
                for (int j = 4; j <= i-1; j --)
                    localScoreRank[j] = localScoreRank[j-1];
                localScoreRank[i] = score;
                storeRankLocaly(i);
                rank = i+1;
                break;
            }
        }
        return rank;
    }

    public static void storeRankLocaly(int startForm) {
        for (int i = startForm; i < 5; i ++) {
            PlayerPrefs.SetInt("Level"+LevelManager.currentLevel+"_Rank"+i, localScoreRank[i]);
        }
    }
}