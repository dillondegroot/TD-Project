using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField] int xSize = 20;
    [SerializeField] int zSize = 20;
    [SerializeField] MeshFilter mF;

    Vector3[] vertices;
    private Mesh mesh;

    void Start()
    {
        GenerateTerrain();
        mesh = new Mesh();
    }

    // Update is called once per frame
    void Update()
    {
        GenerateTerrain();
    }

    private void GenerateTerrain()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        int i = 0;
        for (int z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                vertices[i] = new Vector3(x, 0, z);
                i++;
            }
        }

        mF.mesh = mesh;
    }

    private void OnDrawGizmos()
    {
        if (vertices == null)
            return;

        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }
}
