using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Transition
{
    None = 0,
    SawPlayer,
    ReachPlayer,
    LostPlayer,
    NoHealth,
}

public enum FSMStateID
{
    None = 0,
    Patrolling = 1,
    Chasing = 2,
    Attacking = 3,
    Dead = 4,

}

public class AdvancedFSM : FSM {

    private List<FSMState> fsmStates;

    private FSMStateID currentStateID;
    public FSMStateID CurrentStateID
    {
        get
        {
            return currentStateID;
        }
    }

    private FSMState currentState;
    public FSMState CurrentState
    {
        get
        {
            return currentState;
        }
    }

    public AdvancedFSM()
    {
        fsmStates = new List<FSMState>();
    }


    public void AddFSMState(FSMState fsmState)
    {
        if(fsmState == null)
        {
            Debug.LogError("FSM ERROR: Null reference is not allowed");
            return;
        }
        if(fsmStates.Count == 0)
        {
            fsmStates.Add(fsmState);
            currentState = fsmState;
            currentStateID = fsmState.ID;
            return;
        }
      
        foreach (FSMState state in fsmStates)
        {
            if (state.ID == fsmState.ID)
            {
                Debug.LogError("FSM ERROR: Trying to add a state was already inside the list");
                return;
            }
        }
        fsmStates.Add(fsmState);
    }

    public void DeleteFSMState(FSMStateID id)
    {
        if (id == FSMStateID.None)
        {
            Debug.LogError("FSM ERROR: Null reference is not allowed");
            return;
        }
        foreach (FSMState state in fsmStates)
        {
            if (state.ID == id)
            {
                fsmStates.Remove(state);
                return;
            }
        }
    }

    protected void PerformTransition(Transition trans)
    {
        if(trans == Transition.None)
        {
            Debug.LogError("FSM ERROR: Null transition is not allowed");
            return;
        }
        FSMStateID id = currentState.GetOutputState(trans);

        if(id == FSMStateID.None)
        {
            Debug.LogError("FSM ERROR: Current state id not have a target state");
            return;
        }
        currentStateID = id;
        foreach(FSMState state in fsmStates)
        {
            if(state.ID == currentStateID)
            {
                currentState = state;
                break;
            }
        }
    }

}
