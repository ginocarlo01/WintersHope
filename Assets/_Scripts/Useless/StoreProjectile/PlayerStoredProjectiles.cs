using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerStoredProjectiles : MonoBehaviour
{
    [System.Serializable]
    public class StoredProjectile
    {
        public TypeUtility.Type type;
        public int qty;
    }

    public List<StoredProjectile> maxQty;
    public List<StoredProjectile> currentQty;

    public Dictionary<TypeUtility.Type, int> currentQtyDict;
    public Dictionary<TypeUtility.Type, int> maxQtyDict;

    [SerializeField]
    private int indexSelectedType;

    private void Start()
    {
        maxQtyDict = new Dictionary<TypeUtility.Type, int>();
        currentQtyDict = new Dictionary<TypeUtility.Type, int>();

        foreach (StoredProjectile storedProjectile in maxQty)
        {
            maxQtyDict.Add(storedProjectile.type, storedProjectile.qty);
        }

        foreach (StoredProjectile storedProjectile in currentQty)
        {
            currentQtyDict.Add(storedProjectile.type, storedProjectile.qty);
        }

    }

    private void Update()
    {

        if(currentQtyDict.Count > 1)
        {
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
        
    }

    void ChangeProjectileType(bool scrollUp)
    {
        // Calculate the new index based on scroll direction
        int newIndex;

        if (scrollUp)
        {
            // Scroll up, change to the next type
            newIndex = (indexSelectedType + 1) % currentQtyDict.Count;
        }
        else
        {
            // Scroll down, change to the previous type
            newIndex = (indexSelectedType - 1 + currentQtyDict.Count) % currentQtyDict.Count;
        }

        // Set the new projectile type
        indexSelectedType = newIndex;

        // Print for testing purposes
        //Debug.Log("Current Projectile Type: " + newIndex);
    }

    public void SaveProjectile(TypeUtility.Type type)
    {
        currentQtyDict[type]++;
        Debug.Log(type.ToString() + " " + currentQtyDict[type].ToString());
    }

    public bool CanProjectileBeSaved(TypeUtility.Type type)
    {
        if (!maxQtyDict.ContainsKey(type))
        {
            Debug.LogWarning("This type is not registered");
            return false;
        }

        if (!currentQtyDict.ContainsKey(type))
        {
            currentQtyDict.Add(type, 0);
        }

        if (currentQtyDict[type] < maxQtyDict[type])
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CanProjectileBeUsed(TypeUtility.Type type)
    {
        if (!maxQtyDict.ContainsKey(type))
        {
            Debug.LogWarning("This type is not registered");
            return false;
        }

        if (!currentQtyDict.ContainsKey(type))
        {
            Debug.LogWarning("This type was not kept");
            return false;
        }

        if (currentQtyDict[type] > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void UseProjectile(TypeUtility.Type type)
    {
        currentQtyDict[type]--;
    }

    public TypeUtility.Type GetSelectedType()
    {
        TypeUtility.Type[] tipos = currentQtyDict.Keys.ToArray();

        if (indexSelectedType >= 0 && indexSelectedType < tipos.Length)
        {
            return tipos[indexSelectedType];
        }
        else
        {
            Debug.LogError("Índice de tipo selecionado fora dos limites.");
            return TypeUtility.Type.Neutral; 
        }
    }

}
