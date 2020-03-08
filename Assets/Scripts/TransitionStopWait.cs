using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TransitionStopWait : Transition
{
    GameObject character;
    GameObject[] ores;

    public TransitionStopWait(GameObject character)
    {
        this.character = character;
    }
    public override bool isTriggered()
    {
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
        return ores.Length > 0;
    }
    public override string getTargetState()
    {
        return "WalkToMine";
    }
    public override void getActions()
    {
        return;
    }
}