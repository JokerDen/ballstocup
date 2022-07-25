using System;
using System.IO;
using Unity.VectorGraphics;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(TubeGenerator))]
public class TubeGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        TubeGenerator thisTarget = target as TubeGenerator;
        if (GUILayout.Button($"Generate"))
            thisTarget.Generate();
    }
}
#endif 

public class TubeGenerator : MonoBehaviour
{
    [Header("Add '.bytes' to end of svg-file name to recognize it as TextAsset")]
    [SerializeField] TextAsset sourceSVG;
    
    [SerializeField] string pathToGenerateMesh;
    
    [SerializeField] Material sharedMaterial;
    
    [SerializeField] bool overwriteMeshAsset;

    public void Generate()
    {
        var imported = SVGParser.ImportSVG(new StringReader(sourceSVG.text));

        BezierPathSegment[] segments;
        try
        {
            segments = imported.Scene.Root.Children[0].Shapes[0].Contours[0].Segments;
        }
        catch (Exception e)
        {
            Debug.LogError($"Can't parse SVG for segments. Check the file and it hierarchy\n{e}");
            throw;
        }

        while (transform.childCount > 0)
        {
            var child = transform.GetChild(0);
            child.SetParent(null);
            DestroyImmediate(child.gameObject);
        }
        
        var tube = new GameObject("tube");
        tube.transform.SetParent(transform);
        var meshFilter = tube.AddComponent<MeshFilter>();
        var mesh = new Mesh();
        BuildMesh(mesh, segments);
        mesh.RecalculateNormals();
        meshFilter.mesh = mesh;
        
        var meshRenderer = tube.AddComponent<MeshRenderer>();
        meshRenderer.sharedMaterial = sharedMaterial;
        
#if UNITY_EDITOR
        var filename = pathToGenerateMesh + sourceSVG.name + ".asset";
        if (overwriteMeshAsset)
            AssetDatabase.DeleteAsset(filename);
        AssetDatabase.CreateAsset(mesh, filename);
#endif
    }

    private void BuildMesh(Mesh mesh, BezierPathSegment[] segments)
    {
        // Unity Docs
        /*Vector3[] vertices = new Vector3[4]
        {
            new Vector3(0, 0, 0),
            new Vector3(width, 0, 0),
            new Vector3(0, height, 0),
            new Vector3(width, height, 0)
        };
        mesh.vertices = vertices;
        
        int[] tris = new int[6]
        {
            // lower left triangle
            0, 2, 1,
            // upper right triangle
            2, 3, 1
        };
        mesh.triangles = tris;*/
    }
}
