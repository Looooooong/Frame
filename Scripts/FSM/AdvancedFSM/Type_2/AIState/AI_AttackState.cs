using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_AttackState : StateBase<AICotrl>
{
    public AI_AttackState(int id,AICotrl o) : base(id,o)
    {
    }

    public override void OnEnter(params object[] args)
    {
        base.OnEnter(args);
    }

    public override void OnExit(params object[] args)
    {
        base.OnExit(args);
    }

    public override void OnStay(params object[] args)
    {
        base.OnStay(args);
    }
}
