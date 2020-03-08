using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge
{
    public Node node1;
    public Node node2;
    public float weight;

    public Edge(Node n1, Node n2, float w)
    {
        this.node1 = n1;
        this.node2 = n2;
        this.weight = w;
    }
}