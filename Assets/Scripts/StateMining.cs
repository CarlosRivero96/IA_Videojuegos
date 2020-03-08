using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StateMining : State
{
    List<Transition> transitions = new List<Transition>();
    GameObject character;

    public StateMining(GameObject character, List<Transition> trans, string name)
    {
        this.transitions = trans;
        this.character = character;
        this.name = name;
    }

    public override void getActions()
    {
        character.GetComponent<pathFind>().target = character;
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