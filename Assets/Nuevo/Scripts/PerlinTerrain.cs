using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
public class PerlinTerrain : MonoBehaviour
{
    [SerializeField] private float scale = 0.1f;
    [SerializeField] private float height = 2f;
    private Mesh mesh;
    private MeshCollider meshCollider;
    private Vector3[] baseVertices;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        meshCollider = GetComponent<MeshCollider>();

        baseVertices = mesh.vertices;

        DeformTerrain();
    }

    void DeformTerrain()
    {
        Vector3[] vertices = new Vector3[baseVertices.Length];

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 v = baseVertices[i];

            float noise = Mathf.PerlinNoise(
                (v.x + transform.position.x) * scale,
                (v.z + transform.position.z) * scale
            );

            v.y = noise * height;
            vertices[i] = v;
        }

        mesh.vertices = vertices;
        mesh.RecalculateNormals();

        meshCollider.sharedMesh = null;
        meshCollider.sharedMesh = mesh;
    }
}