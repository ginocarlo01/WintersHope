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
    void Start()
    {

        InitMaterial();

        currentState = attackState;
        currentState.OnBeginState();
    }
    void Update()
    {

        if (currentState != null)
            currentState.OnUpdate();

        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        if (scrollInput > 0f)
        {
            ChangeProjectileType(true);
        }
        else if (scrollInput < 0f)
        {
            ChangeProjectileType(false);
        }
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

    void ChangeProjectileType(bool scrollUp)
    {
        // Get the array of all types
        TypeUtility.Type[] allTypes = (TypeUtility.Type[])System.Enum.GetValues(typeof(TypeUtility.Type));

        // Find the index of the current type
        int currentIndex = System.Array.IndexOf(allTypes, currentProjectileType);

        // Calculate the new index based on scroll direction
        int newIndex;

        if (scrollUp)
        {
            // Scroll up, change to the next type
            newIndex = (currentIndex + 1) % allTypes.Length;
        }
        else
        {
            // Scroll down, change to the previous type
            newIndex = (currentIndex - 1 + allTypes.Length) % allTypes.Length;
        }

        // Set the new projectile type
        currentProjectileType = allTypes[newIndex];

        // Print for testing purposes
        Debug.Log("Current Projectile Type: " + currentProjectileType);
    }

}
