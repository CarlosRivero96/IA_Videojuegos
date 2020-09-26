using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StateWander : State
{
    List<Transition> transitions = new List<Transition>();
    GameObject character;
    Node target = null;

    public StateWander(GameObject character, List<Transition> trans)
    {
        this.character = character;
        this.transitions = trans;
        this.name = "Wander";
    }

    public override void getActions()
    {
        if (character.GetComponent<pathFind>().path.Count == 0)
        {
            character.GetComponent<StateMachine>().placesVisited += 1;
            do
            {
                var nodesList = character.GetComponent<pathFind>().graph.nodes;
                target = nodesList[Random.Range(0, nodesList.Count)];

            } while(target.getCenter().x > 18 && target.getCenter().y > 4);

            GameObject.Find("MonsterTarget").transform.position = target.getCenter();
        }
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