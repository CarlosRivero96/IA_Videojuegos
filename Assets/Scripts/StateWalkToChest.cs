using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StateWalkToChest : State
{
    GameObject character;
    List<Transition> transitions;
    public StateWalkToChest( GameObject character, List<Transition> trans)
    {
        this.character = character;
        this.transitions = trans;
        this.name = "WalkToChest";
    }
    public override void getActions()
    {
        GameObject chest = GameObject.FindGameObjectWithTag("OreChest");
        character.GetComponent<pathFind>().target = chest;
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