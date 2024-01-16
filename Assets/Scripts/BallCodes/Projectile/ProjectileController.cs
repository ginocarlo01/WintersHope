using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ProjectileController : MonoBehaviour
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

    [Header("Hold values")]
    public float holdProjectileTime = 5f;

    [SerializeField]
    public GameObject holdObj;
    [HideInInspector]
    public Material holdMat;

    private void Awake()
    {
        attackState = new AttackState(this);
        holdState = new HoldState(this);
        releaseState = new ReleaseState(this);
        followAttackState = new FollowAttackState(this);
    }
    void Start()
    {

        InitiMaterial();

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
    void OnDestroy()
    {
        CleanParentData();
    }

    void CleanParentData()
    {
        if(transform.parent != null)
        {
            transform.parent.gameObject.GetComponent<ControlAroundBorder>().CleanInsideObjectData();
        }
        
    }

    public void InitiMaterial()
    {
        holdObj.GetComponent<SpriteRenderer>().material = Instantiate<Material>(holdObj.GetComponent<SpriteRenderer>().material);
        holdMat = holdObj.GetComponent<SpriteRenderer>().material;
    }
}
