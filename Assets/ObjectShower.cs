using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using UnityEngine;

public class Utils
{

    public static float sign(Vector3 p1, Vector3 p2, Vector3 p3)
    {
        return (p1.x - p3.x) * (p2.z - p3.z) - (p2.x - p3.x) * (p1.z - p3.z);
    }

    public static bool PointInTriangle(Vector3 pt, Vector3 v1, Vector3 v2, Vector3 v3)
    {
        float d1, d2, d3;
        bool has_neg, has_pos;

        d1 = sign(pt, v1, v2);
        d2 = sign(pt, v2, v3);
        d3 = sign(pt, v3, v1);

        has_neg = (d1 < 0) || (d2 < 0) || (d3 < 0);
        has_pos = (d1 > 0) || (d2 > 0) || (d3 > 0);

        return !(has_neg && has_pos);
    }

    public static bool IsLinesIntersecting(Vector3 a, Vector3 b, Vector3 c, Vector3 d)
    {

        float d1, d2, d3, d4;

        d1 = sign(a, c, d);
        d2 = sign(b, c, d);

        d3 = sign(c, a, b);
        d4 = sign(d, a, b);

        return (d1 * d2 <= 0) && (d3 * d4 <= 0);

    }

    public static bool IsPointInsideSquare(Vector3 point, Vector3 position, float size)
    {

        float minX = position.x - size * 0.5f;
        float maxX = position.x + size * 0.5f;
        float minZ = position.z - size * 0.5f;
        float maxZ = position.z + size * 0.5f;

        return (point.x >= minX && point.x < maxX) && (point.z >= minZ && point.z < maxZ);

    }

    /*
    public static bool SquareInTriangle(Vector3 position, float size, Vector3 v1, Vector3 v2, Vector3 v3)
    {
        Vector3 a1 = position + new Vector3(-size, 0, size) * 0.5f;
        Vector3 a2 = position + new Vector3(size, 0, size) * 0.5f;
        Vector3 a3 = position + new Vector3(size, 0, -size) * 0.5f;
        Vector3 a4 = position + new Vector3(-size, 0, -size) * 0.5f;

        return (

            PointInTriangle(a1, v1, v2, v3)
            || PointInTriangle(a2, v1, v2, v3)
            || PointInTriangle(a3, v1, v2, v3)
            || PointInTriangle(a4, v1, v2, v3)
            || PointInTriangle(position, v1, v2, v3)

        );
    }

    */

    public static bool SquareInTriangle3(Vector3 position, float size, Vector3 v1, Vector3 v2, Vector3 v3)
    {
        Vector3 a1 = position + new Vector3(-size, 0, size) * 0.5f;
        Vector3 a2 = position + new Vector3(size, 0, size) * 0.5f;
        Vector3 a3 = position + new Vector3(size, 0, -size) * 0.5f;
        Vector3 a4 = position + new Vector3(-size, 0, -size) * 0.5f;

        return (

            IsLinesIntersecting(a1, a2, v1, v2) ||
            IsLinesIntersecting(a1, a2, v2, v3) ||
            IsLinesIntersecting(a1, a2, v3, v1) ||

            IsLinesIntersecting(a2, a3, v1, v2) ||
            IsLinesIntersecting(a2, a3, v2, v3) ||
            IsLinesIntersecting(a2, a3, v3, v1) ||

            IsLinesIntersecting(a3, a4, v1, v2) ||
            IsLinesIntersecting(a3, a4, v2, v3) ||
            IsLinesIntersecting(a3, a4, v3, v1) ||

            IsLinesIntersecting(a4, a1, v1, v2) ||
            IsLinesIntersecting(a4, a1, v2, v3) ||
            IsLinesIntersecting(a4, a1, v3, v1)

        );
    }

    public static bool SquareInTriangle2(Vector3 position, float size, Vector3 v1, Vector3 v2, Vector3 v3)
    {
        return (

            PointInTriangle(position, v1, v2, v3)

        );
    }

    public static bool SquareInTriangle4(Vector3 position, float size, Vector3 v1, Vector3 v2, Vector3 v3)
    {

        return (

            IsPointInsideSquare(v1, position, size) ||
            IsPointInsideSquare(v2, position, size) ||
            IsPointInsideSquare(v3, position, size)

        );
    }

    public static bool SquareInTriangle(Vector3 position, float size, Vector3 v1, Vector3 v2, Vector3 v3)
    {
        Vector3 a1 = position + new Vector3(-size, 0, size) * 0.5f;
        Vector3 a2 = position + new Vector3(size, 0, size) * 0.5f;
        Vector3 a3 = position + new Vector3(size, 0, -size) * 0.5f;
        Vector3 a4 = position + new Vector3(-size, 0, -size) * 0.5f;

        return (
        
            IsLinesIntersecting(a1, a2, v1,v2) ||
            IsLinesIntersecting(a1, a2, v2,v3) ||
            IsLinesIntersecting(a1, a2, v3,v1) ||

            IsLinesIntersecting(a2, a3, v1, v2) ||
            IsLinesIntersecting(a2, a3, v2, v3) ||
            IsLinesIntersecting(a2, a3, v3, v1) ||

            IsLinesIntersecting(a3, a4, v1, v2) ||
            IsLinesIntersecting(a3, a4, v2, v3) ||
            IsLinesIntersecting(a3, a4, v3, v1) ||

            IsLinesIntersecting(a4, a1, v1, v2) ||
            IsLinesIntersecting(a4, a1, v2, v3) ||
            IsLinesIntersecting(a4, a1, v3, v1)

            || PointInTriangle(position, v1, v2, v3)

            || (

                IsPointInsideSquare(v1, position, size) ||
                IsPointInsideSquare(v2, position, size) ||
                IsPointInsideSquare(v3, position, size)

            )


        );
    }

}



public class ObjectShower : MonoBehaviour
{

    public bool useQuadtree = true;

    public Transform a;
    public Transform b;
    public Transform c;

    public uint objectCount = 20;

    public uint quadTreeLevelCount = 4;

    public float objectSpawnAreaSize = 2.0f;

    public Transform objectPrefab;

    public bool showObjects = true;



    Transform[] transformList;

    [SerializeField]
    private Quadtree quadTree;


    static ulong ticks = 0;

    private static Task HandleTimer()
    {
        ticks++;

        throw new NotImplementedException();
    }

    void Start()
    {

        Vector3 pos = (a.position + b.position + c.position) / 3.0f;

        float maxRadius = (pos - a.position).magnitude;

        if ((pos - b.position).magnitude > maxRadius)
        {

            maxRadius = (pos - b.position).magnitude;

        }

        if ((pos - c.position).magnitude > maxRadius)
        {

            maxRadius = (pos - c.position).magnitude;

        }

        quadTree = new Quadtree(quadTreeLevelCount, pos, maxRadius * 2 * objectSpawnAreaSize);

        CreateObjects();

        Timer timer = new Timer(1);
        timer.Elapsed += async (sender, e) => await HandleTimer();
        timer.Start();

        Show();

        timer.Stop();
        timer.Dispose();

        Debug.Log(ticks);

    }

    void Update()
    {

        Debug.DrawLine(a.position, b.position, Color.blue);
        Debug.DrawLine(b.position, c.position, Color.blue);
        Debug.DrawLine(c.position, a.position, Color.blue);

        DebugDraw(quadTree.rootNode);


    }

    void CreateObjects()
    {

        float minX = Mathf.Min(a.position.x, Mathf.Min(b.position.x, c.position.x));
        float maxX = Mathf.Max(a.position.x, Mathf.Max(b.position.x, c.position.x));
        float minZ = Mathf.Min(a.position.z, Mathf.Min(b.position.z, c.position.z));
        float maxZ = Mathf.Max(a.position.z, Mathf.Max(b.position.z, c.position.z));

        transformList = new Transform[objectCount];

        for (uint i = 0; i < objectCount; ++i)
        {

            float x = UnityEngine.Random.RandomRange(minX, maxX) * objectSpawnAreaSize;
            float z = UnityEngine.Random.RandomRange(minZ, maxZ) * objectSpawnAreaSize;

            Transform obj = Instantiate(objectPrefab, new Vector3(x, 0, z), Quaternion.identity);

            AddObject(obj, i);

        }

    }

    void AddObject(Transform obj, uint i)
    {

        transformList[i] = obj;

        obj.gameObject.SetActive(false);

        if (useQuadtree)
        {

            //if (Utils.PointInTriangle(obj.position, a.position, b.position, c.position))
           //     obj.gameObject.SetActive(true);
            //else

            quadTree.Add(obj);

        }

    }

    void CheckAndShowQuadtreeNode(Node node)
    {

        bool check1 = Utils.SquareInTriangle2(node.position, node.size, a.position, b.position, c.position);
        bool check2 = Utils.SquareInTriangle3(node.position, node.size, a.position, b.position, c.position);
        bool check3 = Utils.SquareInTriangle4(node.position, node.size, a.position, b.position, c.position);

        if (
            !(check1 || check2 || check3)
        ) return;

        if (node.IsLeafNode() || ((check1 && !check2 && !check3)))
        {

            //node.transformList[0].gameObject.SetActive(showObjects);

           // return;

            for (uint i = 0; i < node.transformList.Count; ++i)
            {

                if (Utils.PointInTriangle(node.transformList[(int)i].position, a.position, b.position, c.position))
                {

                    node.transformList[(int)i].gameObject.SetActive(showObjects);

                }

            }

        }
        else
        {

            for (uint i = 0; i < 4; ++i)
            {

                CheckAndShowQuadtreeNode(node.childs[i]);

            }

        }

    }

    void DebugDraw(Node node)
    {
        Vector3 v1 = node.position + new Vector3(-node.size, 0, node.size) * 0.5f;
        Vector3 v2 = node.position + new Vector3(node.size, 0, node.size) * 0.5f;
        Vector3 v3 = node.position + new Vector3(node.size, 0, -node.size) * 0.5f;
        Vector3 v4 = node.position + new Vector3(-node.size, 0, -node.size) * 0.5f;

        bool check1 = Utils.SquareInTriangle2(node.position, node.size, a.position, b.position, c.position);
        bool check2 = Utils.SquareInTriangle3(node.position, node.size, a.position, b.position, c.position);
        bool check3 = Utils.SquareInTriangle4(node.position, node.size, a.position, b.position, c.position);

        if (

            check1 || check2 || check3

        )
        {

            Debug.DrawLine(v1, v2, Color.yellow);
            Debug.DrawLine(v2, v3, Color.yellow);
            Debug.DrawLine(v3, v4, Color.yellow);
            Debug.DrawLine(v4, v1, Color.yellow);

            if ((!node.IsLeafNode()) && (!(check1 && !check2 && !check3)))
            {

                for (uint i = 0; i < 4; ++i)
                {

                    DebugDraw(node.childs[i]);

                }

            }

        }
        else
        {

            Debug.DrawLine(v1, v2, Color.green);
            Debug.DrawLine(v2, v3, Color.green);
            Debug.DrawLine(v3, v4, Color.green);
            Debug.DrawLine(v4, v1, Color.green);

            if (!node.IsLeafNode())
            {

                for (uint i = 0; i < 4; ++i)
                {

                    //DebugDraw(node.childs[i]);

                }

            }

        }

    }

    void Show()
    {

        if (useQuadtree)
        {

            CheckAndShowQuadtreeNode(quadTree.rootNode);

        }
        else
        {

            for (uint i = 0; i < objectCount; ++i)
            {

                if (Utils.PointInTriangle(transformList[i].position, a.position, b.position, c.position))
                {

                    transformList[i].gameObject.SetActive(true);

                }

            }

        }

    }

}
