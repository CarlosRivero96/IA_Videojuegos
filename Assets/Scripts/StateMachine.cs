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
    List<Transition> hidingTrans = new List<Transition>();
    List<Transition> sleepingTrans = new List<Transition>();
    List<Transition> wanderTrans = new List<Transition>();
    List<Transition> walkingTrans = new List<Transition>();
    public GameObject spawn;



    // For Monster
    public int placesVisited;
    public GameObject zzz;

    public void Start()
    {
        
        // Creates Transitions
        Transition startMining = new TransitionStartMining(gameObject);
        Transition stopMining = new TransitionStopMining();
        Transition startWait = new TransitionStartWait(gameObject);
        Transition stopWait = new TransitionStopWait(gameObject);
        Transition alerted = new TransitionAlerted(gameObject);
        Transition wakeUp = new TransitionWakeUp(gameObject);
        Transition sleep = new TransitionSleep(gameObject);
        Transition goToSleep = new TransitionGoToSleep(gameObject);
        Transition backToMining = new TransitionBackToMining(gameObject);


        walkToMineTrans.Add(startMining);
        walkToMineTrans.Add(alerted);

        miningTrans.Add(stopMining);
        miningTrans.Add(alerted);

        walkToChestTrans.Add(startWait);
        walkToChestTrans.Add(alerted);

        waitingTrans.Add(stopWait);
        waitingTrans.Add(alerted);

        hidingTrans.Add(backToMining);

        sleepingTrans.Add(wakeUp);

        walkingTrans.Add(sleep);

        wanderTrans.Add(goToSleep);


        // Creates States
        StateWalkToChest walkToChest = new StateWalkToChest(gameObject, walkToChestTrans);
        StateMining mining = new StateMining(gameObject, miningTrans, "Mining");
        StateWaiting waiting = new StateWaiting(gameObject, waitingTrans, "Waiting");
        StateHiding hiding = new StateHiding(gameObject, hidingTrans);
        
        StateWaiting sleeping = new StateWaiting(gameObject, sleepingTrans, "Sleeping");
        StateWander wander = new StateWander(gameObject, wanderTrans);
        StateWaiting walking = new StateWaiting(gameObject, walkingTrans, "Walking");


        if (Object.Equals(gameObject.name, "Miner (iron)"))
        {
            StateWalkToMine ironMiner = new StateWalkToMine(gameObject, gameObject.GetComponent<pathFind>().graph, "OreIron", walkToMineTrans);
            initialState = ironMiner;
            states.Add(ironMiner);
            states.Add(walkToChest);
            states.Add(mining);
            states.Add(waiting);
            states.Add(hiding);
        }
        else if (Object.Equals(gameObject.name, "Miner (gold)"))
        {
            StateWalkToMine goldMiner = new StateWalkToMine(gameObject, gameObject.GetComponent<pathFind>().graph, "OreGold", walkToMineTrans);
            initialState = goldMiner;
            states.Add(goldMiner);
            states.Add(walkToChest);
            states.Add(mining);
            states.Add(waiting);
            states.Add(hiding);
        }
        else if (Object.Equals(gameObject.name, "Miner (diamond)"))
        {
            StateWalkToMine diamondMiner = new StateWalkToMine(gameObject, gameObject.GetComponent<pathFind>().graph, "OreDiamond", walkToMineTrans);
            initialState = diamondMiner;
            states.Add(diamondMiner);
            states.Add(walkToChest);
            states.Add(mining);
            states.Add(waiting);
            states.Add(hiding);
        }
        else if (Object.Equals(gameObject.name, "Monster"))
        {
            initialState = sleeping;
            states.Add(sleeping);
            states.Add(wander);
            states.Add(walking);
        }

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
            Debug.Log("exit: " + currentState.name);
            currentState.getExitActions();
            //State targetState = null;
            // Find the target state.
            string targetStateStr = triggered.getTargetState();
            //Debug.Log(targetStateStr);
            foreach(State state in states)
            {
                if (Object.Equals(state.name, targetStateStr))
                    {
                        //Debug.Log(state.name);
                        currentState = state;
                        break;
                    }
            }
            Debug.Log("enter: " + currentState.name);


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
