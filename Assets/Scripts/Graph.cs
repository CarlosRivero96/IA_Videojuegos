using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph
{
    public Dictionary<int, Node> nodes = new Dictionary<int, Node>();

    public void calcNodes()
    {
        int id = 0;
        foreach(GameObject o in GameObject.FindGameObjectsWithTag("Squares"))
        {
            Vector3 upLeft = o.transform.position + (new Vector3(-o.transform.localScale.x / 2, o.transform.localScale.y / 2, 0));
            Vector3 upRight = o.transform.position + (new Vector3(o.transform.localScale.x / 2, o.transform.localScale.y / 2, 0));
            Vector3 downRight = o.transform.position + (new Vector3(o.transform.localScale.x / 2, -o.transform.localScale.y / 2, 0));
            Vector3 downLeft = o.transform.position + (new Vector3(-o.transform.localScale.x / 2, -o.transform.localScale.y / 2, 0));

            Node node1 = new Node(id, upRight, downLeft, upLeft);
            id++;
            Node node2 = new Node(id, upRight, downLeft, downRight);
            id++;

            nodes.Add(node1.getId(), node1);
            nodes.Add(node2.getId(), node2);
        }
    }

    public void calcEdges()
    {
        foreach (KeyValuePair<int, Node> i in nodes)
        {
            foreach (KeyValuePair<int, Node> j in nodes)
            {
                if (i.Value.getId() == j.Value.getId())
                    continue;
                else
                {
                    int count = 0;
                    foreach (Vector3 vertexI in i.Value.getVertices())
                    {
                        foreach (Vector3 vertexJ in j.Value.getVertices())
                        {
                            if (Vector3.Equals(vertexI, vertexJ))
                            count++;
                        }    
                    }

                    if (count == 2)
                        {
                            Edge e = new Edge(i.Value, j.Value, Vector3.Distance(i.Value.getCenter(), j.Value.getCenter()));
                            i.Value.AddEdge(e);
                            j.Value.AddEdge(e);
                        }
                }
            }
        }
    }

    public void drawTriangles()
    {
        foreach (KeyValuePair<int, Node> n in nodes)
        {
            Vector3[] vertices = n.Value.getVertices();
            Debug.DrawLine (vertices[0], vertices[1], Color.white);
            Debug.DrawLine (vertices[1], vertices[2], Color.white);
            Debug.DrawLine (vertices[2], vertices[0], Color.white);
        }
    }

    public void drawEdge(Edge e)
    {
        Debug.DrawLine (e.node1.getCenter(), e.node2.getCenter(), Color.red);
    }

    bool sameSide(Vector3 p1, Vector3 p2, Vector3 a, Vector3 b)
    {
        Vector3 cp1 = Vector3.Cross(b-a, p1-a);
        Vector3 cp2 = Vector3.Cross(b-a, p2-a);
        if (Vector3.Dot(cp1, cp2) >= 0) 
            return true;
        else 
            return false;
    }

    bool pointInTriangle(Vector3 p, Vector3 a, Vector3 b, Vector3 c)
    {
        if (sameSide(p, a, b, c) && sameSide(p, b, a, c) && sameSide(p, c, a, b))
            return true;
        else
            return false;
    }

    public Node nodeIn(Vector3 p)
    {
        foreach(KeyValuePair<int, Node> n in nodes)
        {
            Node node = n.Value;
            if (pointInTriangle(p, node.getVertices()[0], node.getVertices()[1], node.getVertices()[2]))
                return node;
        }

        return null;
    }
}