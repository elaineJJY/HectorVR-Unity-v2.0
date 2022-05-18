/* Author: inmo-jang https://github.com/inmo-jang/unity_assets/tree/master/PointCloudStreaming
 * Modifications: Added queue compatibility by Trung-Hoa Ha
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using RosSharp.RosBridgeClient;

// Script can be placed anywhere.
public class PointCloudRenderer : MonoBehaviour
{
    public PointCloudSubscriber subscriber { get; set; }
    public float pointSize = 1f;
    public Transform offset; 
    // Mesh stores the positions and colours of every point in the cloud.
    // The renderer and filter are used to display it.
    Mesh mesh;
    MeshRenderer meshRenderer;
    MeshFilter mf;

    [Header("Make sure these lists are minimised or editor will crash")]
    private Vector3[] positions = new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, 1, 0) };
    private Color[] colours = new Color[] { new Color(1f, 0f, 0f), new Color(0f, 1f, 0f) };
    private Vector3[] lastpositions;
    private Color[] lastcolours;


    void Start()
    {
        // Give all the required components to the gameObject.
        GameObject rosbridge = GameObject.Find("RosBridge");
        if(null == rosbridge){
            Debug.Log("PointCloudRenderer.cs: RosBridge object not found.");
        }
        subscriber = rosbridge.GetComponent<PointCloudSubscriber>();
        if(subscriber == null){
            Debug.Log("PointCloudRenderer.cs: Pointcloud subscriber component not found.");
        }
        meshRenderer = gameObject.AddComponent<MeshRenderer>();
        mf = gameObject.AddComponent<MeshFilter>();
        meshRenderer.material = new Material(Shader.Find("Custom/PointCloudShader"));
        mesh = new Mesh
        {
            indexFormat = UnityEngine.Rendering.IndexFormat.UInt32
        };

        transform.position = offset.position;
        transform.rotation = offset.rotation;
    }

    // Updates mesh object.
    void UpdateMesh(){
        positions = subscriber.GetPCL();
        colours = subscriber.GetPCLColor();

        if (positions == null)
        {
            Debug.Log("PointCloudRenderer.cs: No points to display. Please check the PointcloudSubscriber whether points are received and saved to queue.");
            return;
        }
        mesh.Clear();
        mesh.vertices = positions;
        mesh.colors = colours;

        int[] indices = new int[positions.Length];

        for (var i = 0; i < positions.Length; i++)
        {
            indices[i] = i;
        }

        mesh.SetIndices(indices, MeshTopology.Points, 0);
        mf.mesh = mesh;
    }

    void Update()
    {
        transform.position = offset.position;
        transform.rotation = offset.rotation;
        meshRenderer.material.SetFloat("_PointSize", pointSize);
        UpdateMesh();
    }
}
