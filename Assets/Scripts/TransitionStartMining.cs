using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TransitionStartMining : Transition
{
    GameObject character;
    GameObject[] ores;

    public TransitionStartMining(GameObject character)
    {
        this.character = character;
    }
    public override bool isTriggered()
    {
        bool triggered = false;

        if (Object.Equals(character.name, "Miner (iron)"))
            {
                this.ores = GameObject.FindGameObjectsWithTag("OreIron");
            }
        else if (Object.Equals(character.name, "Miner (gold)"))
            {
                this.ores = GameObject.FindGameObjectsWithTag("OreGold");
            }
        else if (Object.Equals(character.name, "Miner (diamond)"))
            {
                this.ores = GameObject.FindGameObjectsWithTag("OreDiamond");
            }

        foreach(GameObject ore in ores)
        {
            if (Vector3.Distance(ore.transform.position, character.transform.position) < 1.5)
                triggered = true;
        }
        return triggered;
    }
    public override string getTargetState()
    {
        return "Mining";
    }
    public override void getActions()
    {
        return;
    }
}