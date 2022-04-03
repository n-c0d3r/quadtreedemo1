using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class Node
{

    public Node(Quadtree tree, Node parent, uint level, Vector3 position, float size)
    {

        this.level = level;
        this.position = position;
        this.size = size;
        this.parent = parent;
        this.tree = tree;



        transformList = new List<Transform>();



        if(level + 1 < tree.levelCount)
        {

            childs = new Node[4] {

                new Node(tree, this, level + 1, position + new Vector3(-size,0,size) * 0.25f, size * 0.5f),
                new Node(tree, this, level + 1, position + new Vector3(size,0,size) * 0.25f, size * 0.5f),
                new Node(tree, this, level + 1, position + new Vector3(size,0,-size) * 0.25f, size * 0.5f),
                new Node(tree, this, level + 1, position + new Vector3(-size,0,-size) * 0.25f, size * 0.5f)

            };

        }

    }

    [SerializeField]
    public List<Transform> transformList;

    [SerializeField]
    public uint level;

    [SerializeField]
    public Vector3 position;

    [SerializeField]
    public float size;

    Node parent;

    Quadtree tree;

    [SerializeField]
    public Node[] childs;

    public bool IsPointInside(Vector3 point)
    {

        float minX = position.x - size * 0.5f;
        float maxX = position.x + size * 0.5f;
        float minZ = position.z - size * 0.5f;
        float maxZ = position.z + size * 0.5f;

        return (point.x >= minX && point.x < maxX) && (point.z >= minZ && point.z < maxZ);

    }

    public void Add(Transform obj)
    {
        if (!IsPointInside(obj.position)) return;

        transformList.Add(obj);

        if(level < tree.levelCount - 1)
            for (uint i = 0; i < 4; ++i)
            {

                if (childs[i].IsPointInside(obj.position))
                {

                    childs[i].Add(obj);

                }

            }

    }

    public bool IsLeafNode()
    {

        return level == (tree.levelCount - 1);

    }

}
