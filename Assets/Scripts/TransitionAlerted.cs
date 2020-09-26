using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TransitionAlerted : Transition
{
    GameObject character;
    float creationTime;
    bool change = true;

    public TransitionAlerted(GameObject character)
    {
        this.character = character;
    }
    public override bool isTriggered()
    {
        bool triggered = false;
        GameObject sound = GameObject.FindGameObjectWithTag("Sound");
        if (sound != null)
        {
            if (change)
            {
                creationTime = Time.time;
                change = false;
            }
            float timer = Time.time - creationTime;
            if (Vector3.Distance(sound.transform.position, character.transform.position) < (timer * 5))
                triggered = true;
        }
        return triggered;
    }
    public override string getTargetState()
    {
        change = true;
        return "Hiding";
    }
    public override void getActions()
    {
        character.GetComponent<pathFind>().maxAcceleration = 6;
        Object.Instantiate(character.GetComponent<StateMachine>().spawn, character.transform.position, Quaternion.identity);
        return;
    }
}