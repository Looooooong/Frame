using UnityEngine;
using System.Collections;
using System;

public class SimpleFSM : FSM {

    public enum FSMState
    {
        None,
        Patrol,
        Chase,
        Attack,
        Dead
    }
    

    public FSMState curState;
    private float curSpeed;
    private float curRotSpeed;

    public GameObject bullet;

    private int health;
    private bool isDead;

    protected override void Initialize()
    {
        base.Initialize();

        curState = FSMState.Patrol;
        curSpeed = 5.0f;
        curRotSpeed = 2.0f;
        health = 100;
        isDead = false;

        shootRate = 3.0f;
        elapsedTime = 0.0f;

        pointList = GameObject.FindGameObjectsWithTag("Point");

        FindNextPoint();

        GameObject objPlayer = GameObject.FindGameObjectWithTag("Player");
        playerTranform = objPlayer.transform;
        turret = transform.Find("Turret/Turret");
        bulletSpawnPoint = transform.Find("Turret/Turret/SpawnPoint");
    }

    private void FindNextPoint()
    {
        int rndIndex = UnityEngine.Random.Range(0, pointList.Length);
        destPos = pointList[rndIndex].transform.position;

        float rndRadius = 10.0f;
        if(IsInCurrentRange(destPos))
        {
            Vector3 rndPosition = new Vector3(UnityEngine.Random.Range(-rndRadius, rndRadius), 0, UnityEngine.Random.Range(-rndRadius, rndRadius));
            destPos = pointList[rndIndex].transform.position + rndPosition;
        }
    }

    private bool IsInCurrentRange(Vector3 destPos)
    {
        float xPos = Mathf.Abs(destPos.x - transform.position.x);
        float zPos = Mathf.Abs(destPos.z - transform.position.z);

        if (xPos <= 3 && zPos <= 3)
        {
            return true;
        }
        else return false;

    }

    protected override void FSMUpdate()
    {
        base.FSMUpdate();

        switch(curState)
        {
            case FSMState.Patrol:
                UpdatePatrolState();
                break;
            case FSMState.Attack:
                UpdateAttackState();
                break;
            case FSMState.Chase:
                UpdateChaseState();
                break;
            case FSMState.Dead:
                UpdateDeadState();
                break;
            case FSMState.None: break;

        }


        if(health <= 0)
        {
            curState = FSMState.Dead;
        }
    }

    private void UpdateAttackState()
    {
        destPos = playerTranform.position;
        float dist = Vector3.Distance(transform.position, playerTranform.position);
        if (dist >= 300)
        {
            curState = FSMState.Patrol;
        }
        else if (dist >= 200 && dist < 300)
        {
            curState = FSMState.Chase;
        }
        Quaternion targetRotation = Quaternion.LookRotation(destPos - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * curRotSpeed);
        ShootBullet();

    }

    private void ShootBullet()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= shootRate)
        {
            Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            elapsedTime = 0.0f;
        }
        
    }

    private void UpdateChaseState()
    {
        destPos = playerTranform.position;
        float dist = Vector3.Distance(transform.position, playerTranform.position);
        if(dist < 200)
        {
            curState = FSMState.Attack;
        }
        else if(dist >= 300)
        {
            curState = FSMState.Patrol;
        }

        Quaternion targetRotation = Quaternion.LookRotation(destPos - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * curRotSpeed);
        transform.Translate(Vector3.forward * Time.deltaTime * curSpeed);
    }

    private void UpdateDeadState()
    {
        if(!isDead)
        {
            isDead = true;
            Dead();
        }
    }

    private void Dead()
    {
        float rndX = UnityEngine.Random.Range(1.0f, 3.0f);
        float rndZ = UnityEngine.Random.Range(1.0f, 3.0f);
        Rigidbody tempRigid = GetComponentInChildren<Rigidbody>();
        for (int i = 0;i<3;i++)
        {
            tempRigid.AddExplosionForce(10.0f, transform.position - new Vector3(rndX, 0, rndZ),40.0f,10.0f);
            tempRigid.velocity = transform.TransformDirection(new Vector3(rndX, 3.0f, rndZ));
        }
        Destroy(gameObject, 5f);
    }

    private void UpdatePatrolState()
    {
        if(Vector3.Distance(transform.position,destPos)< 5)
        {
            FindNextPoint();
        }
        else if(Vector3.Distance(transform.position,playerTranform.position) <= 300)
        {
            curState = FSMState.Chase;
        }
        Quaternion targetRotation = Quaternion.LookRotation(destPos - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * curRotSpeed);

        transform.Translate(Vector3.forward * Time.deltaTime * curSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
        }

    }
}
