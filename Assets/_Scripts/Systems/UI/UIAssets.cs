using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIAssets : MonoBehaviour
{
    [SerializeField]
    private GameObject[] handPositions;

    [SerializeField]
    private GameObject[] elements;

    [SerializeField]
    private GameObject hand;

    [SerializeField]
    private Transform target;

    [SerializeField]
    float roundingDistance = .2f;

    [SerializeField]
    float speed = 5f;

    private void Awake()
    {
        DisableAllOrbs();
    }



    private void Update()
    {
        if(target != null)
        {
            hand.transform.position = Vector3.MoveTowards(hand.transform.position, target.position, speed * Time.deltaTime);
            if (Vector3.Distance(hand.transform.position, target.position) < roundingDistance)
            {
                target = null;
            }
        }
    }

    #region SubjectSubscription
    private void OnEnable()
    {
        ControlAroundBorder.SelectedTypeAction += ChangeTarget;
    }
    private void OnDisable()
    {
        ControlAroundBorder.SelectedTypeAction -= ChangeTarget;
    }

    #endregion

    private void ChangeTarget(int newTarget)
    {
        target = handPositions[newTarget].transform;

    }

    private void DisableAllOrbs()
    {
        for(int i = 0; i < elements.Length; i++)
        {
            elements[i].SetActive(false);
        }
    }
}
