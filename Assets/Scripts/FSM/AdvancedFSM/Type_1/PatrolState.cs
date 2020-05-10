using UnityEngine;
using System.Collections;

public class PatrolState : FSMState
{
    public PatrolState(Transform[] wp)
    {
        waypoints = wp;
        stateID = FSMStateID.Patrolling;

        curRotSpeed = 2.0f;
        curSpeed = 5.0f;
    }

    public override void Reason(Transform player, Transform npc)
    {
        float dist = Vector3.Distance(npc.position, player.position);
        
        if (dist < 10.0f)
        {
            npc.GetComponent<NPCTankController>().SetTransition(Transition.ReachPlayer);
        }
        else if(dist < 15.0f)
        {
            npc.GetComponent<NPCTankController>().SetTransition(Transition.SawPlayer);
        }

    }

    public override void Act(Transform player, Transform npc)
    {
        if (Vector3.Distance(npc.position, destPos) <= 5.0f)
        {
            FindNextPoint();
        }

        Quaternion targetRotation = Quaternion.LookRotation(destPos - npc.position);
        npc.rotation = Quaternion.Slerp(npc.rotation, targetRotation, Time.deltaTime * curRotSpeed);

        npc.Translate(Vector3.forward * Time.deltaTime * curSpeed);
    }



}
