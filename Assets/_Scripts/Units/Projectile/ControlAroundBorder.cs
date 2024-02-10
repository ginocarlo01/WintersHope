using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlAroundBorder : MonoBehaviour
{
    [Header("Control Projectile")]
    [SerializeField]
    GameObject insideObject;
    ProjectileController insideProjectile;
    [SerializeField]
    Transform spawnPoint;
    [HideInInspector]
    public bool isFather;
    bool pressedState;

    [Header("Tags")]
    [SerializeField]
    string playerTag;
    [SerializeField]
    string enemyTag = "Enemy";

    [Header("Change Projectile")]
    [SerializeField]
    List<TypeUtility.Type> availableProjectiles = new List<TypeUtility.Type>();
    [SerializeField]
    private int indexSelectedType;
    public static Action<int> SelectedTypeAction;
    public static Action<int> EnableOrbAction;

    private void Start()
    {
        ChangeToReleased(); //idk why this is here but keep it!

        for(int i = 0; i < availableProjectiles.Count; i++)
        {
            EnableOrbAction?.Invoke(i);
        }
    }

    void Update()
    {
        #region InputControls
        if (Input.GetMouseButtonDown(0))
        {
            pressedState = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            pressedState = false;
        }

        if (Input.GetMouseButtonDown(1))
        {
            ChangeProjectileType(true);
        }
        #endregion

        #region PastCode
        /*
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

                Debug.Log("There isn't a object!");


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
        */
        #endregion

        if (!isFather && pressedState)
        {
            if (insideObject != null)
            {
                insideProjectile.ChangeState();
                insideObject.transform.position = spawnPoint.position;
                insideObject.transform.parent = transform;
                isFather = true;
                ChangeIndexProjectile(((int)insideProjectile.GetProjectileType()));
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

    void ChangeProjectileType(bool scrollUp)
    {
        // Calculate the new index based on scroll direction
        int newIndex;

        if (scrollUp)
        {
            // Scroll up, change to the next type
            newIndex = (indexSelectedType + 1) % availableProjectiles.Count;
            
        }
        else
        {
            // Scroll down, change to the previous type
            newIndex = (indexSelectedType - 1 + availableProjectiles.Count) % availableProjectiles.Count;
            
        }

        // Set the new projectile type
        if(indexSelectedType != newIndex)
        {
            ChangeIndexProjectile(newIndex);
            if (insideProjectile != null)
            {
                insideProjectile.SetProjectileType(availableProjectiles[indexSelectedType]);
            }
        }
        
    }

    private void ChangeIndexProjectile(int newIndex_)
    {
        indexSelectedType = newIndex_;
        SelectedTypeAction?.Invoke(newIndex_);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(this.gameObject.tag != playerTag && !isFather && collision.tag == enemyTag)
        {
            insideObject = collision.gameObject;
            insideProjectile = collision.GetComponent<ProjectileController>();
        }
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
        this.gameObject.tag = "PlayerReleased";
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

    public void AddProjectileTypeToArsenal(TypeUtility.Type type)
    {
        if(!availableProjectiles.Contains(type))
        {
            availableProjectiles.Add(type);
            EnableOrbAction?.Invoke(((int)type));
        }
        
    }
}
