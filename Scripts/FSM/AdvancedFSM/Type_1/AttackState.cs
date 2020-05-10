using UnityEngine;
using System.Collections;

public class AttackState : FSMState
{

    public AttackState(Transform[] wp)
    {
        waypoints = wp;
        stateID = FSMStateID.Attacking;

        curRotSpeed = 2.0f;
        curSpeed = 5.0f;

        FindNextPoint();
    }


    public override void Reason(Transform player, Transform npc)
    {
        float dist = Vector3.Distance(npc.position, player.position);
        if(dist >= 10.0f&& dist <15.0f)
        {
            npc.GetComponent<NPCTankController>().SetTransition(Transition.SawPlayer);
        }
        else if(dist>= 15.0f)
        {
            npc.GetComponent<NPCTankController>().SetTransition(Transition.LostPlayer);
        }
        
    }



    public override void Act(Transform player, Transform npc)
    {
        destPos = player.position;

        Transform turret = npc.GetComponent<NPCTankController>().turret;

        Quaternion turretRotation = Quaternion.LookRotation(destPos - turret.position);
        turret.rotation = Quaternion.Slerp(turret.rotation, turretRotation, Time.deltaTime * curRotSpeed);
        npc.GetComponent<NPCTankController>().ShootBullet();
    }

}
