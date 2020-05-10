using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class FSMState {

    protected Dictionary<Transition, FSMStateID> map = new Dictionary<Transition, FSMStateID>();

    protected FSMStateID stateID;
    public FSMStateID ID
    {
        get
        {
            return stateID;
        }
    }

    protected Vector3 destPos;
    protected Transform[] waypoints;
    protected float curSpeed;
    protected float curRotSpeed;


    public abstract void Reason(Transform player, Transform npc);
    public abstract void Act(Transform player, Transform npc);

    public void AddTransition(Transition transition,FSMStateID id)
    {
        if(transition == Transition.None || id == FSMStateID.None)
        {
            Debug.LogError("FSMState ERROR: Null transition not allowed");
            return;
        }
        if(map.ContainsKey(transition))
        {
            Debug.LogWarning("FSMState ERROR: transition is already inside the map");
            return;
        }
        map.Add(transition, id);
        Debug.LogWarning("Added:" + transition + "with ID:" + id);
    }

    public void DeleteTransition(Transition trans)
    {
        if(trans == Transition.None)
        {
            Debug.LogError("FSMState ERROR: Null transition not allowed");
            return;
        }

        if(map.ContainsKey(trans))
        {
            map.Remove(trans);
            return;
        }
        Debug.LogError("FSMState ERROR: " + trans + " Transition passed to the state was not in the list");
    }

    public FSMStateID GetOutputState(Transition trans)
    {
        if(trans == Transition.None)
        {
            Debug.LogError("FSMState ERROR: Null Transition is not allowed");
            return FSMStateID.None;
        }
        if(map.ContainsKey(trans))
        {
            return map[trans];
        }
        Debug.LogError("FSMState ERROR: " + trans + " Transition passed to the state was not in the list");
        return FSMStateID.None;
    }

    protected void FindNextPoint()
    {
        int rndIndex = UnityEngine.Random.Range(0, waypoints.Length);
        destPos = waypoints[rndIndex].transform.position;

       
    }

    

    protected bool IsInCurrentRange(Transform trans , Vector3 destPos)
    {
        float xPos = Mathf.Abs(destPos.x - trans.position.x);
        float zPos = Mathf.Abs(destPos.z - trans.position.z);

        if (xPos <= 3 && zPos <= 3)
        {
            return true;
        }
        else return false;

    }


}
