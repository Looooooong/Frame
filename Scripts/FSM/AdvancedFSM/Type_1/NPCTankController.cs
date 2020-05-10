using UnityEngine;
using System.Collections;
using System;

public class NPCTankController : AdvancedFSM {

    public GameObject bullet;

    private int health;

    protected override void Initialize()
    {
        playerTranform = GameObject.FindGameObjectWithTag("Player").transform;
        turret = transform.Find("Turret/Turret");
        bulletSpawnPoint = transform.Find("Turret/Turret/SpawnPoint");

        shootRate = 2.0f;
        elapsedTime = 0.0f;

        health = 100;

        ConstructFSM();
    }


    private void ConstructFSM()
    {

        pointList = GameObject.FindGameObjectsWithTag("Point");

        Transform[] waypoints = new Transform[pointList.Length];
        for(int i = 0; i< pointList.Length;i++)
        {
            waypoints[i] = pointList[i].transform;
        }

        PatrolState patrol = new PatrolState(waypoints);
        patrol.AddTransition(Transition.SawPlayer, FSMStateID.Chasing);
        patrol.AddTransition(Transition.ReachPlayer, FSMStateID.Attacking);
        patrol.AddTransition(Transition.NoHealth, FSMStateID.Dead);

        ChaseState chase = new ChaseState(waypoints);
        chase.AddTransition(Transition.LostPlayer, FSMStateID.Patrolling);
        chase.AddTransition(Transition.ReachPlayer, FSMStateID.Attacking);
        chase.AddTransition(Transition.NoHealth, FSMStateID.Dead);

        AttackState attack = new AttackState(waypoints);
        attack.AddTransition(Transition.LostPlayer, FSMStateID.Patrolling);
        attack.AddTransition(Transition.SawPlayer, FSMStateID.Chasing);
        attack.AddTransition(Transition.NoHealth, FSMStateID.Dead);

        DeadState dead = new DeadState();


        
        AddFSMState(chase);
        AddFSMState(patrol);
        AddFSMState(attack);
        AddFSMState(dead);
    }

    protected override void FSMUpdate()
    {
        elapsedTime += Time.deltaTime;
    }

    protected override void FSMFixedUpdate()
    {
        CurrentState.Reason(playerTranform, transform);
        CurrentState.Act(playerTranform, transform);
    }

    public void SetTransition(Transition t)
    {
        PerformTransition(t);
    }

    public void ShootBullet()
    {
        if(elapsedTime>= shootRate)
        {
            Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            elapsedTime = 0.0f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {

        }
        if(health<=0)
        {
            SetTransition(Transition.NoHealth);
            Destroy(gameObject);
        }
    }
}
