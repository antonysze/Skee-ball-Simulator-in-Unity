using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    public int score;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Ball")) {
            GameManager._instance.ballIn(score);
        }
    }
}
