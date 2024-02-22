using EasyTransition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransition : MonoBehaviour
{
    [Header("Transition Settings")]
    [SerializeField]
    private string newLevelName;
    [SerializeField]
    private TransitionSettings transition;
    [SerializeField]
    private float startDelay;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            TransitionManager.Instance().Transition(newLevelName, transition, startDelay);
        }
    }
}
