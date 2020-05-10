using UnityEngine;
using System.Collections;

public class ChaseState : FSMState
{
    public ChaseState(Transform[] wp)
    {
        waypoints = wp;
        stateID = FSMStateID.Chasing;


        curRotSpeed = 2.0f;
        curSpeed = 5.0f;

        FindNextPoint();
    }


    public override void Reason(Transform player, Transform npc)
    {
        destPos = player.position;
        float dist = Vector3.Distance(npc.position, destPos);
        if(dist >= 15f)
        {
            npc.GetComponent<NPCTankController>().SetTransition(Transition.LostPlayer);
        }
        else if(dist < 10.0f)
        {
            npc.GetComponent<NPCTankController>().SetTransition(Transition.ReachPlayer);
        }

        
    }

    public override void Act(Transform player, Transform npc)
    {
        destPos = player.position;

        Quaternion targetRotation = Quaternion.LookRotation(destPos - npc.position);
        npc.rotation = Quaternion.Slerp(npc.rotation, targetRotation, Time.deltaTime * curRotSpeed);

        npc.Translate(Vector3.forward * Time.deltaTime * curSpeed);
    }

}
