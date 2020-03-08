using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    // We're in one state at a time
    State initialState;
    State currentState;
    List<State> states = new List<State>();
    List<Transition> walkToMineTrans = new List<Transition>();
    List<Transition> miningTrans = new List<Transition>();
    List<Transition> walkToChestTrans = new List<Transition>();
    List<Transition> waitingTrans = new List<Transition>();



    public void Start()
    {
        
        // Creates Transitions
        Transition startMining = new TransitionStartMining(gameObject);
        Transition stopMining = new TransitionStopMining();
        Transition startWait = new TransitionStartWait(gameObject);
        Transition stopWait = new TransitionStopWait(gameObject);

        walkToMineTrans.Add(startMining);
        miningTrans.Add(stopMining);
        walkToChestTrans.Add(startWait);
        waitingTrans.Add(stopWait);

        // Creates States
        StateWalkToChest walkToChest = new StateWalkToChest(gameObject, walkToChestTrans);
        StateMining mining = new StateMining(gameObject, miningTrans, "Mining");
        StateMining waiting = new StateMining(gameObject, waitingTrans, "Waiting");
        

        if (Object.Equals(gameObject.name, "Miner (iron)"))
        {
            StateWalkToMine ironMiner = new StateWalkToMine(gameObject, gameObject.GetComponent<pathFind>().graph, "OreIron", walkToMineTrans);
            initialState = ironMiner;
            states.Add(ironMiner);
        }
        else if (Object.Equals(gameObject.name, "Miner (gold)"))
        {
            StateWalkToMine goldMiner = new StateWalkToMine(gameObject, gameObject.GetComponent<pathFind>().graph, "OreGold", walkToMineTrans);
            initialState = goldMiner;
            states.Add(goldMiner);
        }
        else if (Object.Equals(gameObject.name, "Miner (diamond)"))
        {
            StateWalkToMine diamondMiner = new StateWalkToMine(gameObject, gameObject.GetComponent<pathFind>().graph, "OreDiamond", walkToMineTrans);
            initialState = diamondMiner;
            states.Add(diamondMiner);
        }
        states.Add(walkToChest);
        states.Add(mining);
        states.Add(waiting);

        currentState = initialState;
    }

    // Checks and applies transitions, returning a list of actions.
    public void Update()
    {
        // Assume no transition is triggered.
        Transition triggered = null;

        // Check through each transition and store the first one that triggers.
        foreach(Transition transition in currentState.getTransitions())
        {
            if (transition.isTriggered())
                {
                    triggered = transition;
                    break;        
                }
        }

        // Check if we have a transition to fire.
        if (triggered != null)
        {
            //State targetState = null;
            // Find the target state.
            string targetStateStr = triggered.getTargetState();
            Debug.Log(targetStateStr);
            foreach(State state in states)
            {
                if (Object.Equals(state.name, targetStateStr))
                    {
                        Debug.Log(state.name);
                        currentState = state;
                        break;
                    }
            }

            // Add the exit action of the old state, the transition action
            // and the entry for the new state.
            //List<Action> actions = currentState.getExitActions();
            //actions += triggered.getActions();
            //actions += targetState.getEntryActions();

            //currentState.getExitActions();
            triggered.getActions();
            //targetState.getEntryActions();

            // Complete the transition and return the action list.
            //currentState = targetState;
            //return actions;
        }
        else
            currentState.getActions();
    }
}
