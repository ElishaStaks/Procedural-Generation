/*
* Author: Elisha_Anagnostakis
* Description: This script generates our mesh in the scene
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    // Our mesh in the scene
    private Mesh m_mesh;
    // Array of verticies for our mesh
    private Vector3[] m_verticies;
    // Array of triangles for our mesh
    private int[] m_triangles;

    [SerializeField]
    [Tooltip("How many verticies on the x axis")]
    private int m_xSize = 20;

    [SerializeField]
    [Tooltip("How many verticies on the z axis")]
    private int m_zSize = 20;


    // Start is called before the first frame update
    void Start()
    {
        // Creates a new mesh object
        m_mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = m_mesh;

        StartCoroutine(CreateMesh());
        //UpdateShape();
    }

    private void Update()
    {
        UpdateShape();
    }

    /// <summary>
    /// Updates the properties of the mesh
    /// </summary>
    private void UpdateShape()
    {
        // clears any existing shapes
        m_mesh.Clear();

        // assign the verticies to our mesh
        m_mesh.vertices = m_verticies;
        // assign the triangles to our mesh
        m_mesh.triangles = m_triangles;
        // recalculates the normals of our mesh
        m_mesh.RecalculateNormals();
    }

    /// <summary>
    /// Creates the meshes verticies and triangles
    /// </summary>
    IEnumerator CreateMesh()
    {
        // This will create enough verticies to make 20 quads on the x and z axis
        m_verticies = new Vector3[(m_xSize + 1) * (m_zSize + 1)];

        for (int index = 0, z = 0; z <= m_zSize; z++)
        {
            for (int x = 0; x <= m_xSize; x++)
            {
               float perlinNoise = Mathf.PerlinNoise(x * .3f, z * .3f) * 2f;
                m_verticies[index] = new Vector3(x, perlinNoise, z);
                index++;
            }
        }

        // Creates enough triangles to create a quad
        m_triangles = new int[m_xSize * m_zSize * 6];
        int vert = 0; // vertex
        int tris = 0; // triangle

        // Cycles through the zSize of the verticies
        for (int z = 0; z < m_zSize; z++)
        {
            // Cycles through the xSize of the verticies 
            for (int x = 0; x < m_xSize; x++)
            {   
                // Creates triangles                                     
                m_triangles[tris + 0] = vert + 0;                // (0,0)
                m_triangles[tris + 1] = vert + m_xSize + 1;      // (0,1)
                m_triangles[tris + 2] = vert + 1;                // (1,0)
                m_triangles[tris + 3] = vert + 1;                // (0,1)
                m_triangles[tris + 4] = vert + m_xSize + 1;      // (1,0)
                m_triangles[tris + 5] = vert + m_xSize + 2;      // (1,2)

                // vertex increases by 1 every time we go through the loop
                vert++;
                // triangles increases by 6 every time we go through the loop
                tris += 6;

                yield return new WaitForSeconds(.01f);
            }
            vert++;
        }
    }

    //private void OnDrawGizmos()
    //{
    //    if (m_verticies == null)
    //        return;

    //    for (int i = 0; i < m_verticies.Length; i++)
    //    {
    //        Gizmos.DrawSphere(m_verticies[i], .1f);
    //    }
    //}
}
