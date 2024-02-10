using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TypeUtility;

public class EnemyController : MonoBehaviour
{
    //state values
    public EnemyLoadState loadState;
    public EnemyDisappearState disappearState;
    IEnemyState currentState;

    [Header("Enemy Type")]
    [SerializeField]
    //is an indepdent value from enemylife
    private TypeUtility.Type type;

    [Header("Load state attributes")]
    [SerializeField]
    public float loadTime = 0.5f;
    [SerializeField]
    public float loadTimeRandomness = .1f;
    [SerializeField]
    public float attackSpeed = 1f;
    [SerializeField]
    public float attackSpeedRandomness = .3f;
    [SerializeField]
    public Transform centerPositionProjectile;
    [SerializeField]
    public Transform spawnPositionProjectile;

    [Header("Disappear state attributes")]
    [SerializeField]
    public float disappearTime = 0f;

    [SerializeField]
    public float disappearTimeRandomness = .3f; 

    [Header("Visual Elements")]
    [SerializeField]
    private SpriteRenderer spriteRendererLoadArc;
    [HideInInspector]
    public Material materialLoadArc;
    [SerializeField]
    private SpriteRenderer spriteRendererDisappearArc;
    [HideInInspector]
    public Material materialDisappearArc;
    private Vector2 homePosition;

    //object pool
    ObjectPooler objectPooler;
    [SerializeField]
    public ObjectFromPoolTag objectPoolTag;

    //enemy life
    [SerializeField]
    EnemyLife enemyLife;

    

    private void Awake()
    {
        loadState = new EnemyLoadState(this);
        disappearState = new EnemyDisappearState(this);
        homePosition = transform.position;
    }

    void Start()
    {
        //load arc
        spriteRendererLoadArc.material = Instantiate<Material>(spriteRendererLoadArc.material);
        materialLoadArc = spriteRendererLoadArc.material;
        //disappear arc
        spriteRendererDisappearArc.material = Instantiate<Material>(spriteRendererDisappearArc.material);
        materialDisappearArc = spriteRendererDisappearArc.material;

        //init state
        currentState = loadState;
        currentState.OnBeginState();

        //find object pool
        objectPooler = ObjectPooler.instance;

        loadTime += Random.Range(-loadTimeRandomness, loadTimeRandomness);
        attackSpeed += Random.Range(-attackSpeedRandomness, attackSpeedRandomness);

    }

    private void OnEnable()
    {
        transform.position = homePosition;
        if(enemyLife != null)
        {
            enemyLife.RestartLife();
        }
        SpawnProjectile();
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
        }
        currentState.OnBeginState();
    }

    public void SpawnProjectile()
    {
        GameObject newProjectile = objectPooler.SpawnFromPool(objectPoolTag.ToString(), spawnPositionProjectile.position, centerPositionProjectile.rotation);
        ProjectileController pc_ = newProjectile.GetComponent<ProjectileController>();
        pc_.baseSpeed = attackSpeed;
        pc_.SetProjectileType(this.type);

    }

    public void ChangePosition()
    {
        this.gameObject.transform.position = SpawnEnemies.instance.ChangeObjectPosition(this.gameObject.transform).position;
    }

    
}
