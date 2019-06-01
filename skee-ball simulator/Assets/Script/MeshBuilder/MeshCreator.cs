using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MeshCreator : MonoBehaviour
{
    protected MeshFilter meshFilter;
    protected MeshCollider meshCollider;

    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        if (!meshFilter)
            meshFilter = GetComponent<MeshFilter>();
        if (!meshCollider)
            meshCollider = GetComponent<MeshCollider>();
    }

    public void updateMesh() {
        if (!meshFilter)
            meshFilter = GetComponent<MeshFilter>();
        if (!meshCollider)
            meshCollider = GetComponent<MeshCollider>();
        
        Mesh mesh = buildMesh();
        if (meshFilter)
            meshFilter.sharedMesh = mesh;
        if (meshCollider)
            meshCollider.sharedMesh = mesh;
    }

    protected abstract Mesh buildMesh();
}
