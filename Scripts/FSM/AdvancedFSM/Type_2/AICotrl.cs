using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 挂载在AI上，控制AI的状态机
/// </summary>
public class AICotrl : MonoBehaviour
{

    public enum AIState : int
    {
        None,
        Idle,
        Run,
        Attack,
    }


    public Animator ani;

    public AIState state = AIState.None;

    public StateMachine<AICotrl> machine;

    private void Start()
    {
        ani = GetComponent<Animator>();

        IdleState_AI idle = new IdleState_AI((int)AIState.Idle, this);
        RunState_AI run = new RunState_AI((int)AIState.Run, this);
        AI_AttackState attack = new AI_AttackState((int)AIState.Attack, this);

        machine = new StateMachine<AICotrl>(idle);
        machine.AddState(run);
        machine.AddState(attack);

    }

    private void Update()
    {
        machine.UpdateState();
    }
    

}
