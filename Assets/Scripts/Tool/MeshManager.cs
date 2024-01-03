using UnityEngine;

public class MeshManager : MonoBehaviour
{
    private MeshFilter meshFilter;
    private Mesh myMesh;
    private Vector3[] myVertices = new Vector3[4];
    private int[] myTriangles = new int[6];
    private float width = 2;
    private float hight = 2;

    void Start()
    {
        meshFilter = gameObject.GetComponent<MeshFilter>();
        myMesh = new Mesh();

        myVertices[0] = new Vector3(0, 0, 0);
        myVertices[1] = new Vector3(width, 0, 0);
        myVertices[2] = new Vector3(0, 0, hight);
        myVertices[3] = new Vector3(width, 0, hight);

        myMesh.SetVertices(myVertices);

        myTriangles[0] = 1;
        myTriangles[1] = 2;
        myTriangles[2] = 0;
        myTriangles[3] = 1;
        myTriangles[4] = 3;
        myTriangles[5] = 2;

        myMesh.SetTriangles(myTriangles, 0);

        //MeshFilter‚Ö‚ÌŠ„‚è“–‚Ä
        meshFilter.mesh = myMesh;
    }
}