using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshCollider))]
public class SquareCoillderCreator : MeshCreator
{
    public static Mesh squareMesh;
    protected override Mesh buildMesh() {
        if (squareMesh!=null) return squareMesh;
        
        /* Wall Collider Mesh: square */
        Mesh mesh = new Mesh();
        mesh.name = "Square Mesh";
        Vector3[] vertices = new Vector3[4];
        int[] triangles = new int[6];
        vertices[0] = new Vector3(0, 0, 0);
        vertices[2] = new Vector3(-1, 0, 0);
        vertices[1] = new Vector3(0, 0, 1);
        vertices[3] = new Vector3(-1, 0, 1);
        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;
        triangles[3] = 2;
        triangles[4] = 1;
        triangles[5] = 3;
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        
        squareMesh = mesh;
        return mesh;
    }

    public static GameObject generateSquareObj() {
        GameObject wallColliderObj = new GameObject("Wall Collider", typeof(MeshCollider), typeof(SquareCoillderCreator));
        wallColliderObj.GetComponent<SquareCoillderCreator>().updateMesh();
        wallColliderObj.GetComponent<MeshCollider>().convex = true;
        return wallColliderObj;
    }
}

