using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ProjectileController : MonoBehaviour
{

    public AttackState attackState;
    public HoldState holdState;
    public ReleaseState releaseState;

    IProjectileState currentState;

    [Header("Speed values")]
    public float baseSpeed = 3f;
    [HideInInspector]
    public float currentSpeed;
    public float upSpeedPerSecond = 1f;

    [Header("Hold values")]
    public float holdProjectileTime = 5f;

    private void Awake()
    {
        attackState = new AttackState(this);
        holdState = new HoldState(this);
        releaseState = new ReleaseState(this);
    }
    void Start()
    {
        currentState = attackState;
        currentState.OnBeginState();
    }
    void Update()
    {
        

        if (currentState != null)
            currentState.OnUpdate();
    }

    public void ChangeState()
    {
        if (currentState != null)
        {
            currentState = currentState.ChangeState();
            if(currentState == releaseState)
            {
                transform.parent.gameObject.GetComponent<ControlAroundBorder>().isFather = false;
                transform.parent.gameObject.GetComponent<ControlAroundBorder>().CleanInsideObjectData();
            }
        }
           

            
        currentState.OnBeginState();
    }
}