using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlAroundBorder : MonoBehaviour
{
    [SerializeField]
    GameObject insideObject;
    ProjectileController insideProjectile;

    [SerializeField]
    public bool isFather;
    [SerializeField]
    bool pressedState;

    [SerializeField]
    string playerTag;

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
        Debug.Log("Clean object data");
        if(insideObject != null)
        {
            insideObject.transform.parent = null;
        }
        insideObject = null;
        insideProjectile = null;
        isFather = false;
    }
}
