using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager _instance;
    public Animator gameMapAni;
    public Animator leaderboardAni;
    public GameObject leaderboardObj;
    public RectTransform leaderboard;
    
    void Awake() {
        _instance = this;
    }
    public void createLeaderboardObj(int[] leaderboardData, int level) {
        Transform obj = Instantiate(leaderboardObj, leaderboard.transform.position, Quaternion.identity, leaderboard).transform;
        obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 700 - level * 800);

        obj.GetChild(0).GetComponent<Text>().text = "level " + level;
        obj.GetChild(1).GetComponent<Text>().text = "1st: " + leaderboardData[0] + "\n2nd: " +leaderboardData[1] + "\n3rd: " + leaderboardData[2] + "\n4:" + leaderboardData[3] + "\n5:" + leaderboardData[4];
    }

    public void showGameMap(bool show) {
        gameMapAni.Play(show?"GameMap_fadeIn":"GameMap_fadeOut");
    }
    public void showLeaderboard(bool show) {
        leaderboardAni.Play(show?"Leaderboard_fadeIn":"Leaderboard_fadeOut");
    }
}
