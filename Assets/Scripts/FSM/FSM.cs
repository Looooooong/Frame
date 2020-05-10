using UnityEngine;
using System.Collections;

public class FSM : MonoBehaviour {

    protected Transform playerTranform;

    protected Vector3 destPos;

    protected GameObject[] pointList;

    protected virtual void Initialize() { }
    protected virtual void FSMUpdate() { }
    protected virtual void FSMFixedUpdate() { }

    protected float shootRate;
    protected float elapsedTime;
    

    public Transform turret { set; get; }
    public Transform bulletSpawnPoint { set; get; }

    protected void Start () {

        Initialize();


    }


    protected void Update () {

        FSMUpdate();

    }

    protected void FixedUpdate()
    {
        FSMFixedUpdate();
    }
}
