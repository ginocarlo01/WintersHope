using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIAssets : MonoBehaviour
{
    [SerializeField]
    private GameObject[] handPositions;

    [SerializeField]
    private Image[] elements;

    [SerializeField]
    private GameObject hand;

    [SerializeField]
    private Transform target;

    [SerializeField]
    float roundingDistance = .2f;

    [SerializeField]
    private Sprite disabledOrb;

    [SerializeField]
    private Sprite enabledOrb;

    [SerializeField]
    private Sprite[] loadedOrbs;

    [SerializeField]
    float speed = 5f;


    private void Start()
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
        ControlAroundBorder.EnableOrbAction += EnableOrb;
        ControlAroundBorder.UpdateQtyOrbAction += ChangeOrbQty;
    }
    private void OnDisable()
    {
        ControlAroundBorder.SelectedTypeAction -= ChangeTarget;
        ControlAroundBorder.EnableOrbAction -= EnableOrb;
        ControlAroundBorder.UpdateQtyOrbAction -= ChangeOrbQty;
    }

    #endregion

    private void ChangeTarget(int newTarget)
    {
        target = handPositions[newTarget].transform;

    }

    private void DisableAllOrbs()
    {
        for(int i = 1; i < elements.Length; i++)
        {
            elements[i].sprite = disabledOrb;
        }
    }

    private void EnableOrb(int index)
    {
        elements[index].sprite = enabledOrb;
    }

    public void ChangeOrbQty(TypeUtility.Type orbType, int qty)
    {
        elements[((int)orbType)].sprite = loadedOrbs[qty];
    }
}
