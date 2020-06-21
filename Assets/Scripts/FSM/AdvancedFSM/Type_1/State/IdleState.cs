using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : FSMState
{

    public override void Init()
    {
        StateID = FSMStateID.Idle;
    }


    public override void EnterState(FSMBase fsm)
    {
        base.EnterState(fsm);
        //播放动画
    }

    public override void ExitState(FSMBase fsm)
    {
        base.ExitState(fsm);
        //播放动画
    }


}
