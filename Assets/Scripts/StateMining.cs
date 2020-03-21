using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StateMining : State
{
    List<Transition> transitions = new List<Transition>();
    GameObject character;
    GameObject ore;
    bool setOre = true;

    public StateMining(GameObject character, List<Transition> trans, string name)
    {
        this.transitions = trans;
        this.character = character;
        this.name = name;
    }

    public override void getActions()
    {
        if (setOre)
        {
            this.ore = character.GetComponent<pathFind>().target;
            this.setOre = false;
        }
        character.GetComponent<pathFind>().target = character;
        return;
    }
    public override void getEntryActions()
    {
        return;
    }
    public override void getExitActions()
    {
        this.ore.GetComponent<oreBehaviour>().active = false;
        this.setOre = true;
        return;
    }
    public override List<Transition> getTransitions()
    {
        return transitions;
    }
}