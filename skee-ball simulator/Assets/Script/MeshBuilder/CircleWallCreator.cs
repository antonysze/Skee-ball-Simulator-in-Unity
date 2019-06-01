using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class CircleWallCreator : MeshCreator
{
    public int amountOfFace;
    public float radius;
    public float wallHeight;

    protected override void Start()
    {
        base.Start();
        if (!meshFilter.sharedMesh) //build mesh if mesh is empty
            updateMesh();
    }

    protected override Mesh buildMesh() {
        /* remove all collider child */
        while (transform.childCount>0) 
            DestroyImmediate(transform.GetChild (0).gameObject);

         /* Create Wall Mesh */
        Mesh mesh = new Mesh();
        mesh.name = "Circle Wall";

        /* initialize */
        Vector3[] vertices = new Vector3[amountOfFace*2];
        int[] triangles = new int[amountOfFace*6]; //2 per face * 3 coner

        vertices[0] = new Vector3(0, radius, 0);
        vertices[1] = new Vector3(0, radius, -wallHeight);

        GameObject wallColliderObj = null;

        for (int i = 1; i < amountOfFace; i ++) {
            Vector3 vertexPos = Vector3.zero;
            float radian = (float)i/(amountOfFace-1) * Mathf.PI * 2;
            vertexPos.x = Mathf.Sin(radian)*radius;
            vertexPos.y = Mathf.Cos(radian)*radius;
            
            //create 2 new vertices
            int verticePivot = i*2;
            vertices[verticePivot] = vertexPos;
            vertices[verticePivot+1] = vertexPos + new Vector3(0,0,-wallHeight);
            
            //create 2 triangles of a square face
            int trianglePivot = (i-1)*6;
            triangles[trianglePivot] = verticePivot-2;
            triangles[trianglePivot+1] = verticePivot-1;
            triangles[trianglePivot+2] = (verticePivot<vertices.Length)?verticePivot:0;

            triangles[trianglePivot+3] = verticePivot;
            triangles[trianglePivot+4] = verticePivot-1;
            triangles[trianglePivot+5] = (verticePivot+1<vertices.Length)?verticePivot+1:1;

            //build collider obj for the face
            Vector3 driection = vertices[verticePivot-2]-vertexPos;
            float width = driection.magnitude;

            GameObject wallObj; //create object
            if (wallColliderObj == null) {
                wallColliderObj = SquareCoillderCreator.generateSquareObj();
                wallObj = wallColliderObj;
            } else {
                wallObj = Instantiate(wallColliderObj);
            }

            wallObj.transform.parent = transform;
            wallObj.transform.position = transform.position + transform.rotation * vertexPos;
            wallObj.transform.rotation = transform.rotation * Quaternion.LookRotation(driection, driection.x<0?Vector3.up:-Vector3.up);
            wallObj.transform.localScale = new Vector3(wallHeight,1,width);
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        return mesh;
    }
}