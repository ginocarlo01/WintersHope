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
    PlayerStoredProjectiles pstored;

    private void Start()
    {
        ChangeToReleased();
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
                    if (pstored.CanProjectileBeSaved(projectileType))
                    {
                        Debug.Log("Projectile can be saved");
                        //keep the object or not (wont do nothing in this case)
                        pstored.SaveProjectile(projectileType);
                        projectileController.OnObjectDisabled();
                    }
                    else
                    {
                        Debug.Log("Projectile can't be saved");
                    }
                }
            }
            else
            {
                Debug.Log("There isn't a object!");
            }

        }

        if (!isFather && pressedState)
        {
            if (insideObject != null)
            {
                insideProjectile.ChangeState();
                //ChangeToHold();
                insideObject.transform.parent = transform;
                isFather = true;
            }
        }

        if(isFather && !pressedState)
        {
            if (insideObject != null)
            {
                insideProjectile.ChangeState();
                //ChangeToReleased();
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
