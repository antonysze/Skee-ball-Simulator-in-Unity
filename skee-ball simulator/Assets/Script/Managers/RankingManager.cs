using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingManager {
    private static int[] localScoreRank = new int[5];
    private static bool scoreLoaded = false;

    public static void loadRankLocaly() {
        scoreLoaded = true;
        for (int i = 0; i < 5; i ++) {
            localScoreRank[i] = PlayerPrefs.GetInt("Score_Rank"+i, 0);
        }
    }

    public static int checkRank(int score) {
        if (!scoreLoaded) loadRankLocaly();

        int rank = -1;
        for (int i = 0; i < 5; i ++) {
            if (localScoreRank[i] <= score) {
                for (int j = i; j < 4; j ++)
                    localScoreRank[j+1] = localScoreRank[j];
                localScoreRank[i] = score;
                storeRankLocaly();
                rank = i+1;
                break;
            }
        }
        return rank;
    }

    public static void storeRankLocaly() {
        for (int i = 0; i < 5; i ++) {
            PlayerPrefs.SetInt("Score_Rank"+i, localScoreRank[i]);
        }
    }
}