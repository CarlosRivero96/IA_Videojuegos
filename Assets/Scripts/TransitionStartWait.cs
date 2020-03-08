using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TransitionStartWait : Transition
{
    GameObject character;
    GameObject chest;

    public TransitionStartWait(GameObject character)
    {
        this.character = character;
        this.chest = GameObject.FindGameObjectWithTag("OreChest");

    }
    public override bool isTriggered()
    {
        return Vector3.Distance(chest.transform.position, character.transform.position) < 1.5;
    }
    public override string getTargetState()
    {
        return "Waiting";
    }
    public override void getActions()
    {
        return;
    }
}