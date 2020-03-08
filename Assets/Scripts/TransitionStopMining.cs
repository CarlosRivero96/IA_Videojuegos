using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TransitionStopMining : Transition
{
    float creationTimer;
    bool change = true;

    public override bool isTriggered()
    {
        bool triggered = false;
        if (change)
        {
            creationTimer = Time.time;
            change = false;
        }
        if (Time.time - creationTimer > 5)
        {
            triggered = true;
        }
        return triggered;
    }
    public override string getTargetState()
    {
        change = true;
        return "WalkToChest";
    }
    public override void getActions()
    {
        return;
    }
}