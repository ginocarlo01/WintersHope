using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlAroundBorder : MonoBehaviour
{
    [SerializeField]
    GameObject insideObject;
    ProjectileController insideProjectile;

    public bool isFather;
    bool pressedState;

    [SerializeField]
    string playerTag;

    [SerializeField]
    PlayerStoredProjectiles playerStored;

    [SerializeField]
    Transform spawnPoint;

    [SerializeField]
    TypeUtility.ProjectileTag spawnProjectileTag;

    ObjectPooler objectPooler;

    [SerializeField]
    public float attackSpeed = 1f;

    private void Start()
    {
        ChangeToReleased();
        objectPooler = ObjectPooler.instance;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            pressedState = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            pressedState = false;
        }

        if (Input.GetMouseButtonDown(1))
        {
            //check if there is a object inside
            if(insideObject != null)
            {
                Debug.Log("There is a object!");
                ProjectileController projectileController = insideObject.GetComponent<ProjectileController>();

                if(projectileController != null)
                {
                    TypeUtility.Type projectileType = projectileController.GetProjectileType();
                    //check if this object can be kept
                    if (playerStored.CanProjectileBeSaved(projectileType))
                    {
                        Debug.Log("Projectile can be saved");
                        //keep the object or not (wont do nothing in this case)
                        playerStored.SaveProjectile(projectileType);
                        projectileController.OnObjectDisabled();
                        insideObject = null;
                    }
                    else
                    {
                        Debug.Log("Projectile can't be saved");
                    }
                }
                //isFather = false;
            }
            else
            {
                /*
                Debug.Log("There isn't a object!");
                
                */
            }

        }

        if (Input.GetMouseButtonDown(2))
        {
            if (insideObject == null)
            {
                //check if the selected index can spawn
                TypeUtility.Type selectedType = playerStored.GetSelectedType();
                if (playerStored.CanProjectileBeUsed(selectedType))
                {
                    //use the projectle
                    playerStored.UseProjectile(selectedType);

                    //spawn the projectle
                    GameObject newProjectile = objectPooler.SpawnFromPool(spawnProjectileTag.ToString(), spawnPoint.position, Quaternion.identity);

                    //change the type of the object spawned
                    newProjectile.GetComponent<ProjectileController>().SetProjectileType(selectedType);

                    //set the speed of the object
                    newProjectile.GetComponent<ProjectileController>().baseSpeed = attackSpeed;
                }
            }
        }

        if (!isFather && pressedState)
        {
            if (insideObject != null)
            {
                insideProjectile.ChangeState();
                insideObject.transform.position = spawnPoint.position;
                insideObject.transform.parent = transform;
                isFather = true;
            }
        }

        if(isFather && !pressedState)
        {
            if (insideObject != null)
            {
                insideProjectile.ChangeState();
                if (insideObject != null)
                {
                    insideObject.transform.parent = null;
                }
                isFather = false;
                CleanInsideObjectData();
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(this.gameObject.tag != playerTag && !isFather)
        {
            insideObject = collision.gameObject;
            insideProjectile = collision.GetComponent<ProjectileController>();
        }
       /* 
        * else
        {
            if (collision.CompareTag(LifeBorder.instance.enemyTag))
            {
                LifeBorder.instance.TakeDamage(1);
                Destroy(collision.gameObject);
            }
        }
        */
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!isFather)
        {
            CleanInsideObjectData();
        }
        
    }


    public void ChangeToHold()
    {
        this.gameObject.tag = playerTag;
    }

    public void ChangeToReleased()
    {
        this.gameObject.tag = "Untagged";
    }

    public void CleanInsideObjectData()
    {
        if(insideObject != null)
        {
            insideObject.transform.parent = null;
        }
        insideObject = null;
        insideProjectile = null;
        isFather = false;
    }
}
