using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    int id;
    Vector3[] vertices;
    List<Edge> neighbors = new List<Edge>();
    Vector3 center;

    public Node(int id, Vector3 vertice1, Vector3 vertice2, Vector3 vertice3)
    {
        this.id = id;
        this.vertices = new Vector3[] {vertice1, vertice2, vertice3};
        this.center = (vertice1 + vertice2 + vertice3) / 3;
    }

    public int getId()
    {
        return id;
    }

    public Vector3[] getVertices()
    {
        return vertices;
    }

    public List<Edge> getNeighbors()
    {
        return neighbors;
    }

    public Vector3 getCenter()
    {
        return center;
    }

    public void AddEdge(Edge edge)
    {
        neighbors.Add(edge);
    }

    public void RemoveEdge(Edge edge)
    {
        neighbors.Remove(edge);
    }
}