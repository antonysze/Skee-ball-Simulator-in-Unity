using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardRay : MonoBehaviour
{
    public float length  = 1;
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * length);
    }
}
