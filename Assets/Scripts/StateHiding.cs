using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StateHiding : State
{
    List<Transition> transitions = new List<Transition>();
    GameObject character;

    public StateHiding(GameObject character, List<Transition> trans)
    {
        this.character = character;
        this.transitions = trans;
        this.name = "Hiding";
    }

    public override void getActions()
    {
        var random = new System.Random();
        GameObject[] hidingSpots = GameObject.FindGameObjectsWithTag("HidingSpot");
        int index = random.Next(hidingSpots.Length);
        
        character.GetComponent<pathFind>().target = hidingSpots[index];
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