using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateWalkToMine : State
{
    GameObject character;
    List<Transition> transitions;
    Graph graph;
    string tag;
    public StateWalkToMine( GameObject character, Graph graph, string tag, List<Transition> trans )
    {
        this.character = character;
        this.graph = graph;
        this.tag = tag;
        this.transitions = trans;
        this.name = "WalkToMine";
    }
    public override void getActions()
    {
        GameObject[] ores = GameObject.FindGameObjectsWithTag(tag);
        GameObject closestOre = ores[0];
        float minDistance = Vector3.Distance(ores[0].transform.position, character.transform.position);
        foreach(GameObject g in ores)
        {
            float distance = Vector3.Distance(g.transform.position, character.transform.position);
            if (distance < minDistance){
                minDistance = distance;
                closestOre = g;
            }
        }
        character.GetComponent<pathFind>().target = closestOre;
        return;
    }
    public override void getEntryActions()
    {
        return;
    }
    public override void getExitActions()
    {
        return;
    }
    public override List<Transition> getTransitions()
    {
        return transitions;
    }
}