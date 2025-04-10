using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField] int xSize = 50;
    [SerializeField] int zSize = 50;

    [SerializeField] int xOffset;
    [SerializeField] int zOffset;

    [SerializeField] float noiseScale = 0.06f;
    [SerializeField] float heightMultiplier = 7;

    [SerializeField] Gradient terrainGradient;
    [SerializeField] Material mat;

    private Mesh mesh;
    private Texture2D gradientTexture;

    void Start()
    {
        mesh = new Mesh();
        mesh.name = "Ground";
        GetComponent<MeshFilter>().mesh = mesh;

        xOffset = Random.Range(10, 500);
        zOffset = Random.Range(10, 500);

        GenerateTerrain();
        GradientToTexture();

        GetComponent<MeshCollider>().sharedMesh = mesh;

        for (int i = 0; i < WayPoints.waypoints.Length; i++)
        {
            if (Physics.Raycast(WayPoints.waypoints[i].position, Vector3.down, out RaycastHit hit, 50))
            {
                WayPoints.waypoints[i].position = new Vector3(hit.point.x, hit.point.y + 1.5f, hit.point.z);
            }
        }
    }

    void Update()
    {
        GenerateTerrain();
        GradientToTexture();

        float minTerrainHeight = mesh.bounds.min.y + transform.position.y - 0.1f;
        float maxTerrainHeight = mesh.bounds.max.y + transform.position.y + 0.1f;

        mat.SetTexture("terrainGradient", gradientTexture);

        mat.SetFloat("minTerrainHeight", minTerrainHeight);
        mat.SetFloat("maxTerrainHeight", maxTerrainHeight);
    }

    private void GradientToTexture()
    {
        gradientTexture = new Texture2D(1, 100);
        Color[] pixelColors = new Color[100];

        for (int i = 0; i < 100; i++)
        {
            pixelColors[i] = terrainGradient.Evaluate((float)i / 100);
        }

        gradientTexture.SetPixels(pixelColors);
        gradientTexture.Apply();
    }

    private void GenerateTerrain()
    {
        //VERTICES
        Vector3[] vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        int i = 0;
        for (int z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float yPos = Mathf.PerlinNoise((x + xOffset) * noiseScale, (z + zOffset) * noiseScale) * heightMultiplier;
                vertices[i] = new Vector3(x, yPos, z);
                i++;
            }
        }

        //TRIANGLES
        int[] triangles = new int[xSize * zSize * 6];

        int vertex = 0;
        int triangleIndex = 0;

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[triangleIndex + 0] = vertex + 0;
                triangles[triangleIndex + 1] = vertex + xSize + 1;
                triangles[triangleIndex + 2] = vertex + 1;

                triangles[triangleIndex + 3] = vertex + 1;
                triangles[triangleIndex + 4] = vertex + xSize + 1;
                triangles[triangleIndex + 5] = vertex + xSize + 2;

                vertex++;
                triangleIndex += 6;
            }
            vertex++;
        }

        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}