using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ControlAroundBorder : MonoBehaviour
{
    [System.Serializable]
    public class Projectile
    {
        public TypeUtility.Type type;
        public bool available;
        public bool limited;
        public int qty;
        public int maxQty;
        public Sprite sprite;
    }

    [Header("Control Projectile")]
    [SerializeField]
    GameObject insideObject;
    ProjectileController insideProjectile;
    [SerializeField]
    Transform spawnPoint;
    [SerializeField]
    float projectileNewSpeed = 5f;
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
    List<Projectile> availableProjectiles = new List<Projectile>();
    [SerializeField]
    private int indexSelectedType;
    public static Action<int> SelectedTypeAction;
    public static Action<int> EnableOrbAction;
    public static Action<TypeUtility.Type, int> UpdateQtyOrbAction;

    [Header("Hand")]
    [SerializeField]
    SpriteRenderer handSprite;

    [SerializeField]
    Sprite emptyHandSprite;


    private void Start()
    {
        ChangeToReleased(); //idk why this is here but keep it!

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

        /*if (Input.GetMouseButtonDown(1))
        {
            ChangeProjectileType(true);
        }
        */
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
                ChangeIndexProjectile((insideProjectile.GetProjectileType()));
            }
        }

        if(isFather && !pressedState)
        {
            if (insideObject != null)
            {
                if (availableProjectiles[indexSelectedType].limited)
                {
                    availableProjectiles[indexSelectedType].qty--;
                    AddToProjectileQty(availableProjectiles[indexSelectedType].type, availableProjectiles[indexSelectedType].qty);
                }

                handSprite.sprite = emptyHandSprite;

                insideProjectile.SetProjectileType(availableProjectiles[indexSelectedType].type);
                insideProjectile.SetProjectileCurrentSpeed(projectileNewSpeed);
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
            TypeUtility.Type newType = (TypeUtility.Type)Enum.ToObject(typeof(TypeUtility.Type), newIndex);
            ChangeIndexProjectile(newType);
            
        }
        
    }

    private bool ChangeIndexProjectile(TypeUtility.Type newType)
    {
        foreach(Projectile proj in availableProjectiles)
        {
            if (proj.type == newType && proj.qty > 0 && proj.available)
            {
                indexSelectedType = ((int)newType);
                SelectedTypeAction?.Invoke(indexSelectedType);
                handSprite.sprite = proj.sprite;
                return true;
            }
        }

        indexSelectedType = (0);
        handSprite.sprite = availableProjectiles[0].sprite;
        SelectedTypeAction?.Invoke(indexSelectedType);
        return false;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(this.gameObject.tag != playerTag && !isFather && collision.gameObject.layer != 6 && collision.tag == "Enemy")
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
        foreach (Projectile proj in availableProjectiles)
        {
            if (proj.type == type)
            {
                if (!proj.available)
                {
                    proj.available = true;
                    EnableOrbAction?.Invoke(((int)type));
                }
            }
        }
    }

    public void AddToProjectileQty(TypeUtility.Type type, int qty)
    {
        foreach (Projectile proj in availableProjectiles)
        {
            if (proj.type == type)
            {
                if (!proj.available)
                {
                    proj.available = true;
                    EnableOrbAction?.Invoke(((int)type));
                }
                proj.qty += qty;
                if (proj.qty > proj.maxQty)
                {
                    proj.qty = proj.maxQty;
                }

                UpdateQtyOrbAction?.Invoke(type, proj.qty);
                
            }
        }
    }

    

    #region ObserverSubscription
    private void OnEnable()
    {
        global::AddToProjectileQty.collectedAction += AddToProjectileQty;
    }

    private void OnDisable()
    {
        global::AddToProjectileQty.collectedAction -= AddToProjectileQty;
    }
    #endregion
}
