using System.Collections;
using System.Collections.Generic;
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

    [SerializeField]
    private int indexSelectedType;

    private void Update()
    {

        if(currentQty.Count > 1)
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
            newIndex = (indexSelectedType + 1) % currentQty.Count;
        }
        else
        {
            // Scroll down, change to the previous type
            newIndex = (indexSelectedType - 1 + currentQty.Count) % currentQty.Count;
        }

        // Set the new projectile type
        indexSelectedType = newIndex;

        // Print for testing purposes
        Debug.Log("Current Projectile Type: " + newIndex);
    }
}
