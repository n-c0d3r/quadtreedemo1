using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class Quadtree
{
    public Quadtree(uint levelCount, Vector3 position, float size)
    {

        this.levelCount = levelCount;
        this.position = position;
        this.size = size;

        rootNode = new Node(this, null, 0, position, size);

    }

    [SerializeField]
    public uint levelCount;

    [SerializeField]
    public Node rootNode;

    [SerializeField]
    public Vector3 position;

    [SerializeField]
    public float size;

    public uint GetLevelCount()
    {

        return levelCount;

    }

    public void Add(Transform obj)
    {

        rootNode.Add(obj);

    }

}
