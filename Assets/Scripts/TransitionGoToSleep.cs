using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TransitionGoToSleep : Transition
{
    GameObject character;
    float creationTimer;

    public TransitionGoToSleep(GameObject character)
    {
        this.character = character;
    }
    public override bool isTriggered()
    {
        return character.GetComponent<StateMachine>().placesVisited >= 4;
    }
    public override string getTargetState()
    {
        return "Walking";
    }
    public override void getActions()
    {
        character.GetComponent<StateMachine>().placesVisited = 0;
        character.GetComponent<pathFind>().target = GameObject.Find("Rock Bed");
        return;
    }
}