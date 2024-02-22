using System.Collections.Generic;
using UnityEngine;

public class ActivateByDeath : MonoBehaviour
{
    public List<GameObject> objectsToDisable;
    public GameObject targetObject;

    private int disabledObjectsCount = 0;

    private void Start()
    {
        foreach (GameObject obj in objectsToDisable)
        {
            CallWhenDead deadScript = obj.GetComponent<CallWhenDead>();
            if (deadScript != null)
            {
                deadScript.OnDead += ObjectDeadHandler;
            }
            else
            {
                Debug.LogWarning("The objects in the list do not contain the needed script!");
            }
        }

        
    }

    void ObjectDeadHandler()
    {
        disabledObjectsCount++;

        if (disabledObjectsCount == objectsToDisable.Count)
        {
            if (targetObject != null)
            {
                targetObject.SetActive(true);
            }
        }
    }

    private void OnEnable()
    {
        targetObject.SetActive(false);
        disabledObjectsCount = 0;
    }
}
