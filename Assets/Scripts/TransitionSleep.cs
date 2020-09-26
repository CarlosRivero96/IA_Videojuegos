using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TransitionSleep : Transition
{
    GameObject character;

    public TransitionSleep(GameObject character)
    {
        this.character = character;
    }
    public override bool isTriggered()
    {
        bool triggered = false;
        if (character.GetComponent<pathFind>().path.Count == 0)
            triggered = true;
        return triggered;
    }
    public override string getTargetState()
    {
        return "Sleeping";
    }
    public override void getActions()
    {
        Object.Instantiate(character.GetComponent<StateMachine>().zzz, new Vector3(character.transform.position.x + 1.5f, character.transform.position.y + 1f, 0f), Quaternion.identity);
        return;
    }
}