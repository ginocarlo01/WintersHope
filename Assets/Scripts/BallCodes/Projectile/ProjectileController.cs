using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ProjectileController : MonoBehaviour, IPooledObject

{

    public AttackState attackState;
    public HoldState holdState;
    public ReleaseState releaseState;
    public FollowAttackState followAttackState;

    public IProjectileState currentState;

    [Header("Speed values")]
    [HideInInspector]
    public float baseSpeed;
    [HideInInspector]
    public float currentSpeed;
    public float upSpeedPerSecond = 1f;
    [HideInInspector]
    public Vector3 currentDirection;

    [Header("Hold values")]
    public float holdProjectileTime = 5f;

    [SerializeField]
    public GameObject holdObj;
    [HideInInspector]
    public Material holdMat;

    [Header("Attack values")]
    [SerializeField]
    private float baseAttack = 1;

    [Header("Projectile Type")]
    [SerializeField]
    private TypeUtility.Type currentProjectileType;


    private void Awake()
    {
        attackState = new AttackState(this);
        holdState = new HoldState(this);
        releaseState = new ReleaseState(this);
        followAttackState = new FollowAttackState(this);
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
    

    void CleanParentData()
    {
        if(transform.parent != null)
        {
            transform.parent.gameObject.GetComponent<ControlAroundBorder>().CleanInsideObjectData();
        }
        
    }

    public void InitMaterial()
    {
        holdObj.GetComponent<SpriteRenderer>().material = Instantiate<Material>(holdObj.GetComponent<SpriteRenderer>().material);
        holdMat = holdObj.GetComponent<SpriteRenderer>().material;
    }

    public TypeUtility.Type GetProjectileType()
    {
        return this.currentProjectileType;
    }

    public float GetBaseAttack()
    {
        return baseAttack;
    }

    public void OnObjectSpawn()
    {
        InitMaterial();
        Invoke("TurnOff", 30f);
        Debug.Log("Spawned, starting timer!");
        currentState = attackState;
        currentState.OnBeginState();
    }

    public void OnObjectDisabled()
    {
        CleanParentData();
        TurnOff();
    }

    protected void TurnOff()
    {
        this.gameObject.SetActive(false);
    }
}
