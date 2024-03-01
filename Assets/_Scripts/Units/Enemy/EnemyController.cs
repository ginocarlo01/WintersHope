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
    [SerializeField]
    private bool hasRange = false;

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
    public Transform[] centerPositionProjectile;
    [SerializeField]
    public Transform[] spawnPositionProjectile;
    [SerializeField]
    private bool canSpawnProjectile = true;

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
    [SerializeField]
    Animator anim;
    [SerializeField]
    bool controlAnimation = true;

    [Header("Object Pool")]
    //object pool
    ObjectPooler objectPooler;
    public ObjectFromPoolTag objectPoolTag;

    [Header("Enemy Life")]
    [SerializeField]
    int baseLife = 2;
    [SerializeField]
    EnemyLife enemyLife;
    [SerializeField]
    private bool active = false;


    private void Awake()
    {
        loadState = new EnemyLoadState(this);
        disappearState = new EnemyDisappearState(this);
        homePosition = transform.position;
    }

    void Start()
    {
        enemyLife.SetBaseLife(baseLife);

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
        anim.SetTrigger(type.ToString());

        if(hasRange)
        {
            canSpawnProjectile = false;
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
        if (!active) { return; }
        if (!canSpawnProjectile) { return; }

       for (int i = 0; i < spawnPositionProjectile.Length; i++)
        {
            GameObject newProjectile = objectPooler.SpawnFromPool(objectPoolTag.ToString(), spawnPositionProjectile[i].position, centerPositionProjectile[i].rotation);
            ProjectileController pc_ = newProjectile.GetComponent<ProjectileController>();
            pc_.baseSpeed = attackSpeed;
            pc_.SetProjectileType(this.type);

            
        }
        if (controlAnimation) { UpdateAnimation(); }

    }

    public void ChangePosition()
    {
        this.gameObject.transform.position = SpawnEnemies.instance.ChangeObjectPosition(this.gameObject.transform).position;
    }

    
    public void DisableControlAnimation()
    {
        controlAnimation = false;
    }


    void UpdateAnimation()
    {
        Vector2 direction = (spawnPositionProjectile[0].position - centerPositionProjectile[0].position).normalized;

        anim.SetFloat("moveX", direction.x);
        anim.SetFloat("moveY", direction.y);
    }

    private void OnDisable()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            canSpawnProjectile = true;
        }
    }

    public void EnableEnemy()
    {
        active = true;
    }

    public void DisableEnemy()
    {
        active = false;
    }

}
