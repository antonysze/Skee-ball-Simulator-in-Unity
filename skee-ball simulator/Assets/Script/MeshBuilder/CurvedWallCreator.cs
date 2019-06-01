using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class CurvedWallCreator : MeshCreator
{
    public int amountOfFace;
    public Vector3 size;
    public float curveLength = 1;

    // Start is called before the first frame update
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
        mesh.name = "Curved Wall";

        /* initialize */
        Vector3[] vertices = new Vector3[(amountOfFace+1)*2];
        int[] triangles = new int[amountOfFace*6]; //2 per face * 3 coner

        Vector2 curveVertex = new Vector2(-size.x/2.0f, 0) * curveLength;
        Vector2 conerPos = new Vector2(-size.x/2.0f, size.y);  //left top coner
        Vector2 bottomPos = new Vector2(0, 0); //middle bottom
        Vector2 tangentLineVertex1, tangentLineVertex2;
        int halfOfFace = amountOfFace/2;

        GameObject wallColliderObj = null;

        vertices[0] = conerPos;
        vertices[1] = (Vector3)conerPos + new Vector3(0,0,-size.z);

        /* build mesh and collider */
        for (int side = 0; side < 2; side ++) {
            for (int i = 1; i <= halfOfFace; i ++) {
                float ratio = (float)i/halfOfFace;
                if (side > 0) ratio = 1 - ratio;

                tangentLineVertex1 = Vector2.Lerp(conerPos, curveVertex, ratio);
                tangentLineVertex2 = Vector2.Lerp(curveVertex, bottomPos, ratio);
                Vector3 nextPos = Vector2.Lerp(tangentLineVertex1, tangentLineVertex2, ratio);

                //create 2 new vertices
                int verticePivot = (side * halfOfFace + i)*2;
                vertices[verticePivot] = nextPos;
                vertices[verticePivot+1] = nextPos + new Vector3(0,0,-size.z);

                //create 2 triangles of a square face
                int trianglePivot = (side * halfOfFace + i - 1)*6;
                triangles[trianglePivot] = verticePivot-2;
                triangles[trianglePivot+1] = verticePivot-1;
                triangles[trianglePivot+2] = verticePivot;

                triangles[trianglePivot+3] = verticePivot;
                triangles[trianglePivot+4] = verticePivot-1;
                triangles[trianglePivot+5] = verticePivot+1;   

                //build collider obj for the face
                Vector3 driection = vertices[verticePivot-2]-nextPos;
                float width = driection.magnitude;
    
                GameObject wallObj; //create object
                if (wallColliderObj == null) {
                    wallColliderObj = SquareCoillderCreator.generateSquareObj();
                    wallObj = wallColliderObj;
                } else {
                    wallObj = Instantiate(wallColliderObj);
                }

                wallObj.transform.parent = transform;
                wallObj.transform.position = transform.position + transform.rotation * nextPos;
                wallObj.transform.rotation = transform.rotation * Quaternion.LookRotation(driection.normalized, driection.x<0?Vector3.up:-Vector3.up);
                wallObj.transform.localScale = new Vector3(size.z,1,width);
            }
            conerPos.x *= -1;
            curveVertex.x *= -1;
        }
        
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        return mesh;
    }
}
