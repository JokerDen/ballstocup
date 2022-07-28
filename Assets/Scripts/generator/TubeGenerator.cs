using System;
using System.Collections.Generic;
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
    [Header("Add '.bytes' to end of svg-file name to recognize it as TextAsset")] [SerializeField]
    TextAsset sourceSVG;

    [SerializeField]
    string pathToGenerateMesh;

    [SerializeField]
    Material sharedMaterial;
    
    [SerializeField] GameObject startPrefab;

    [SerializeField]
    bool overwriteMeshAsset;

    [SerializeField] float innerRadius;
    [SerializeField] float outerRadius;
    [SerializeField] int accuracyNum;
    [SerializeField] float interval;
    [SerializeField] bool reverseDirection;
    
    [SerializeField] float scale;

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

        var tubeContainer = new GameObject("tube");
        tubeContainer.transform.SetParent(transform);
        Instantiate(startPrefab, tubeContainer.transform);
        
        var tubeLine = new GameObject("tube_line");
        tubeLine.transform.SetParent(tubeContainer.transform);
        var meshFilter = tubeLine.AddComponent<MeshFilter>();
        var mesh = new Mesh();
        BuildMesh(mesh, segments);
        meshFilter.mesh = mesh;

        var meshRenderer = tubeLine.AddComponent<MeshRenderer>();
        meshRenderer.sharedMaterial = sharedMaterial;
        
        tubeLine.transform.localScale = Vector3.one * scale;
        tubeLine.AddComponent<MeshCollider>();

#if UNITY_EDITOR
        var filename = pathToGenerateMesh + sourceSVG.name + ".asset";
        if (overwriteMeshAsset)
            AssetDatabase.DeleteAsset(filename);
        AssetDatabase.CreateAsset(mesh, filename);
#endif
    }

    private List<Vector3> GetPath(BezierPathSegment[] segments)
    {
        float[] segLengths = VectorUtils.SegmentsLengths(segments, false);
        
        int curIndex = 0;
        float curLength = 0f;
        List<Vector3> path = new List<Vector3>();
        while (interval > 0f)
        {
            var diff = segLengths[curIndex] - curLength;
            if (diff < 0)
            {
                
                curIndex++;
                if (curIndex >= segLengths.Length)
                {
                    curIndex--;
                    path.Add(VectorUtils.Eval(GetSegment(segments, curIndex), 1f));
                    break;
                }
                
                curLength = -diff;
            }
            else
            {
                if (diff >= 0)
                    path.Add(VectorUtils.Eval(GetSegment(segments, curIndex), curLength / segLengths[curIndex]));
                curLength += interval;
            }
        }
        
        if (reverseDirection)
            path.Reverse();

        var startPos = path[0];
        for (int i = 0; i < path.Count; i++)
        {
            path[i] -= startPos;
        }

        return path;
    }

    private void BuildMesh(Mesh mesh, BezierPathSegment[] segments)
    {
        var path = GetPath(segments);
        
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        float rInterval = 360f / accuracyNum;
        Vector3 direction = Vector3.down;
        for (int i = 0; i < path.Count; i++)
        {
            if (i == 0)
            {
                direction = path[i + 1] - path[i];
            }
            else if (i < path.Count - 1)
            {
                direction = path[i + 1] - path[i - 1];
            } 
            direction.Normalize();

            var pos = path[i];

            for (int j = 0; j < accuracyNum; j++)
            {
                var vert = Quaternion.AngleAxis(j * rInterval, direction) * Vector3.forward;
                var inner = pos + vert * innerRadius;
                var outer = pos + vert * outerRadius;
                vertices.Add(inner);
                vertices.Add(outer);
            }

            var sliceVerts = accuracyNum * 2;
            if (i > 0)
            {
                var vertNum = vertices.Count;
                for (int j = 0; j < sliceVerts; j++)
                {
                    int idx0 = vertNum - sliceVerts + j;
                    int idx1 = idx0 + 2;
                    if (idx1 >= vertNum)
                        idx1 -= sliceVerts;
                    int idx2 = idx0 - sliceVerts;
                    bool isInner = idx0 % 2 == 0;
                    
                    AddTriangle(triangles, isInner ? idx1 : idx2, isInner ? idx2 : idx1, idx0);
                    idx0 = idx1 - sliceVerts;
                    AddTriangle(triangles, isInner ? idx1 : idx0, isInner ? idx0 : idx1, idx2);
                }
            }
            
            // TODO: start and end of tube
            /*// if (i == 0 || i == path.Count - 1)
            if (i == 0)
            {
                int offset = vertices.Count - sliceVerts;
                for (int j = 0; j < accuracyNum; j++)
                {
                    int idx0 = offset + j * 2;
                    int idx1 = idx0 + 1;
                    if (idx1 >= accuracyNum * 2)
                        idx1 -= accuracyNum * 2;
                    int idx2 = idx0 + 2;
                    if (idx2 >= accuracyNum * 2)
                        idx2 -= accuracyNum * 2;
                    AddTriangle(triangles, idx2, idx1, idx0);
                }
            }*/
        }

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
    }

    private void AddTriangle(List<int> triangles, int v0, int v1, int v2)
    {
        triangles.Add(v0);
        triangles.Add(v1);
        triangles.Add(v2);
    }

    private BezierSegment GetSegment(BezierPathSegment[] segments, int index)
    {
        BezierSegment segment = new BezierSegment();
        if (index >= segments.Length - 1)
        {
            Debug.LogWarning("not well...");
            return segment;
        }

        segment.P0 = segments[index].P0;
        segment.P1 = segments[index].P1;
        segment.P2 = segments[index].P2;
        segment.P3 = segments[index + 1].P0;
        return segment;
    }
}