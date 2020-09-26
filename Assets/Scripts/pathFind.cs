using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathFind : MonoBehaviour
{
    public GameObject target;
    public List<Node> path = new List<Node>();
    public float maxAcceleration;
    public Kinematic character;
    public Graph graph;

    // This structure is used to keep track of the
    // information we need for each node.
    class NodeRecord
    {
        public Node node;
        // Connection connection;
        //public Edge connection;
        public NodeRecord prev;
        public float costSoFar;
        public float estimatedTotalCost;
    }
    
    public List<Node> pathFindAStar(Node start, Node goal)
    {
        if (goal == null)
            return new List<Node>();
        // Initialize the record for the start node.
        NodeRecord startRecord = new NodeRecord();
        startRecord.node = start;
        startRecord.prev = null;
        startRecord.costSoFar = 0;
        //startRecord.estimatedTotalCost = HEURISTICA

        // Initialize the open and closed lists.
        List<NodeRecord> open = new List<NodeRecord>();
        open.Add(startRecord);
        List<NodeRecord> closed = new List<NodeRecord>();

        NodeRecord current = new NodeRecord();

        // Iterate through processing each node.
        while (open.Count > 0)
        {
            // Find the smallest element in the open list (using the estimatedTotalCost)
            //current = open.smallestElement();
            current = open[0];
            foreach(NodeRecord n in open)
            {
                if (n.estimatedTotalCost < current.estimatedTotalCost)
                    current = n;
            }

            // If it is the goal node, then terminate.
            if (current.node.getId() == goal.getId())
                break;

            // Otherwise get its outgoing connections.
            List<Edge> connections = current.node.getNeighbors();

            // Loop through each connection in turn.
            foreach(Edge connection in connections)
            {
                Node endNode = connection.node1;
                // Get the cost estimate fot eh end node.
                if (connection.node1 == current.node) {
                    endNode = connection.node2;
                }
                
                float endNodeCost = current.costSoFar + connection.weight;

                // If the node is closed we may have to skip, or remove it
                // from the closed list.
                // closed.contains(endNode)
                bool closedContains = false;
                bool openContains = false;
                NodeRecord endNodeRecord = null;
                float endNodeHeuristic = 0;

                foreach (NodeRecord n in closed)
                {
                    if (n.node == endNode){
                        closedContains = true;
                        endNodeRecord = n;
                    }
                }

                // open.contains(endNode)
                foreach (NodeRecord n in open)
                {
                    if (n.node == endNode){
                        openContains = true;
                        endNodeRecord = n;
                    }
                }

                if (closedContains)
                {
                    // Here we find the record in the closed list
                    // corresponding to the endNode.
                    //NodeRecord endNodeRecord = closed.find(endNode)

                    // If we didn't find a shorter route, skip.
                    if (endNodeRecord.costSoFar <= endNodeCost)
                        continue;
                    
                    //closed -= endNodeRecord;
                    closed.Remove(endNodeRecord);

                    // We can use the node's old cost values to calculate
                    // its heuristic without calling the possibly expensive
                    // heuristic function.
                    endNodeHeuristic = endNodeRecord.estimatedTotalCost - endNodeRecord.costSoFar;
                }
                
                // Skip if the node is open and we've not found a better route
                else if (openContains)
                {
                    // Here we find the record in the open list
                    // corresponding to the endNode
                    //endNodeRecord = open.find(endNode);

                    // If our route is no better, then skip
                    if (endNodeRecord.costSoFar <= endNodeCost)
                        continue;
                    
                    // Again, we can calculate its heuristic
                    endNodeHeuristic = endNodeRecord.estimatedTotalCost - endNodeRecord.costSoFar;
                }

                // Otherwise we know we've got an unvisited node, so make a
                // record for it.
                else
                {
                    endNodeRecord = new NodeRecord();
                    endNodeRecord.node = endNode;

                    // We'll need to calculate the heuristic value using
                    // the function, since we don't have and existing record to use
                    //endNodeHeuristic = heuristic.estimate(endNode);
                    endNodeHeuristic = Vector3.Distance(endNode.getCenter(), goal.getCenter());
                }

                // We're here if we need to update the node. Update the
                // cost, estimate and connection.
                endNodeRecord.costSoFar = endNodeCost;
                endNodeRecord.prev = current;
                endNodeRecord.estimatedTotalCost = endNodeCost + endNodeHeuristic;

                // And add it to the open list.
                if (!openContains)
                    open.Add(endNodeRecord);
            }

            // We've finished looking at the connections for the current
            // node, so add it to the closed list and remove it from the open list
            open.Remove(current);
            closed.Add(current);
        }

        // We're here if we've either found the goal, or if we've no more
        // nodes to search, find which.
        if (current.node != goal)
            // We've run out of nodes without finding the coal, so there's
            // no solution.
            return null;

        else
        {
            // Compile the list of connections in the path.
            List<Node> path = new List<Node>();

            // Work back along the path, accumulating connections.
            while (current.node.getId() != start.getId())
            {
            
                path.Insert(0, current.node);
                current = current.prev;

            }

            //path.Insert(0, start);
            // Reverse the path, and return it.
            return path;
        }
    }

    protected SteeringOutput getSteering(Node goal)
    {
        SteeringOutput result = new SteeringOutput();
        // Get the direction to the target.

        result.linear = goal.getCenter() - this.transform.position;

        // The velocity is along this direction, at full speed.
        result.linear.Normalize();
        result.linear *= maxAcceleration;
        result.linear = new Vector3(result.linear.x, result.linear.y, 0);

        result.angular = 0;
        return result;
    }

    // Start is called before the first frame update
    void Start()
    {
        graph = new Graph();
        graph.calcNodes();
        graph.calcEdges();
    }

    // Update is called once per frame
    void Update()
    {
        graph.drawTriangles();

        /*
        foreach(KeyValuePair<int, Node> n in nodes)
        {
            Node node = n.Value;
            foreach(Edge e in node.getNeighbors())
                drawEdge(e);
        }
        */
        if (graph.nodeIn(this.transform.position) == null)
            path = pathFindAStar(graph.nodes[141] ,graph.nodeIn(target.transform.position));
        else
            path = pathFindAStar(graph.nodeIn(this.transform.position) ,graph.nodeIn(target.transform.position));
        for(int i = 1; i<path.Count-1; i++)
            Debug.DrawLine (path[i-1].getCenter(), path[i].getCenter(), Color.red);
        if (path.Count > 0)
            character.updateSLinear(getSteering(path[0]).linear, maxAcceleration/2);
        else{ 
            character.velocity = Vector3.zero;

        }
    }
}