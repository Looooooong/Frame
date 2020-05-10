using UnityEngine;
using System.Collections;

public class DeadState : FSMState
{
    public DeadState()
    {
        stateID = FSMStateID.Dead;
    }

    public override void Reason(Transform player, Transform npc)
    {
        //Do Nothing
    }

    public override void Act(Transform player, Transform npc)
    {
        //Do Nothing
    }

}
