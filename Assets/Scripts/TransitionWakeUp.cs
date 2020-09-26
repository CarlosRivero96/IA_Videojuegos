using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TransitionWakeUp : Transition
{
    GameObject character;
    float creationTimer;
    bool change = true;

    public TransitionWakeUp(GameObject character)
    {
        this.character = character;
    }
    public override bool isTriggered()
    {
        bool triggered = false;
        if (change)
        {
            creationTimer = Time.time;
            change = false;
        }
        if (Time.time - creationTimer > 60)
        {
            triggered = true;
        }
        return triggered;
    }
    public override string getTargetState()
    {
        change = true;
        return "Wander";
    }
    public override void getActions()
    {
        Object.Destroy(GameObject.Find("Zzz(Clone)"));
        Object.Instantiate(character.GetComponent<StateMachine>().spawn, new Vector3(-21, 8, 0f), Quaternion.identity);
        character.GetComponent<pathFind>().target = GameObject.Find("MonsterTarget");
        return;
    }
}