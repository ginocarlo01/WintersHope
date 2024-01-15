using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //state values
    public EnemyLoadState loadState;
    public EnemyDisappearState disappearState;
    IEnemyState currentState;

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
    public GameObject projectile;

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

    private void Awake()
    {
        loadState = new EnemyLoadState(this);
        disappearState = new EnemyDisappearState(this);
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
        GameObject newProjectile = Instantiate(projectile, spawnPositionProjectile.position, Quaternion.identity);
        newProjectile.GetComponent<ProjectileController>().baseSpeed = attackSpeed;
    }

    public void ChangePosition()
    {
        this.gameObject.transform.position = SpawnEnemies.instance.ChangeObjectPosition(this.gameObject.transform).position;
    }

    
}
