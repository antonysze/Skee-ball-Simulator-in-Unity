using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager {
    public static int currentLevel = 0;
    public int numberOfLevels;

    public void enterLevel(int i) {
        currentLevel = i;
        SceneManager.LoadScene(i, LoadSceneMode.Single);
    }
}