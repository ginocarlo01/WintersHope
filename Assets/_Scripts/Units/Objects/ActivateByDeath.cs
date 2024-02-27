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
            IHit deathHandler = obj.GetComponent<IHit>();
            if (deathHandler != null)
            {
                deathHandler.OnHit += ObjectDeadHandler;
            }
            else
            {
                Debug.LogWarning("The objects in the list do not contain the needed interface!");
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
                targetObject.SetActive(false);
            }
        }
    }

    private void OnEnable()
    {
        targetObject.SetActive(true);
        disabledObjectsCount = 0;
    }
}
