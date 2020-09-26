using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TransitionBackToMining : Transition
{
    GameObject character;
    float creationTimer;

    public TransitionBackToMining(GameObject character)
    {
        this.character = character;
    }
    public override bool isTriggered()
    {
        return GameObject.Find("Zzz(Clone)") != null;
    }
    public override string getTargetState()
    {
        return "WalkToChest";
    }
    public override void getActions()
    {
        character.GetComponent<pathFind>().maxAcceleration = 4;
        return;
    }
}