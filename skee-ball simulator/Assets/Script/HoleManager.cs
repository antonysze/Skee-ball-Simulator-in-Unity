using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleManager : MonoBehaviour
{

    public float holeRadius;
    public Transform ball;
    public Collider WallCollider;
    private Collider ballCollider;
    

    private void OnDrawGizmosSelected()
    {  
        Gizmos.color = Color.black;
        foreach (Transform child in transform) {
            Gizmos.DrawSphere(child.transform.position, holeRadius);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        ballCollider = ball.GetComponent<Collider>();
        Vector3 holeScale = Vector3.one * holeRadius*2;
        foreach (Transform child in transform) {
            child.localScale = holeScale;
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool canPassTheHole = false;
        foreach (Transform child in transform) {
            if (Vector3.Distance(ball.position, child.position)<holeRadius) {
                canPassTheHole = true;
                break;
            }
        }
        Physics.IgnoreCollision(ballCollider, WallCollider, canPassTheHole);
    }
}
