using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingManager {
    public static int[] localScoreRank = new int[5];

    public static void loadRankLocaly(int level) {
        for (int i = 0; i < 5; i ++) {
            localScoreRank[i] = PlayerPrefs.GetInt("Level"+level+"_Rank"+i, 0);
        }
    }

    public static int checkRank(int score, int level) {
        loadRankLocaly(level);

        int rank = -1;
        for (int i = 0; i < 5; i ++) {
            if (localScoreRank[i] <= score) { //replace the rank of lower score
                for (int j = 4; j <= i-1; j --)
                    localScoreRank[j] = localScoreRank[j-1];
                localScoreRank[i] = score;
                storeRankLocaly(i, level);
                rank = i+1;
                break;
            }
        }
        return rank;
    }

    public static void storeRankLocaly(int startForm, int level) {
        for (int i = startForm; i < 5; i ++) {
            PlayerPrefs.SetInt("Level"+level+"_Rank"+i, localScoreRank[i]);
        }
    }
}