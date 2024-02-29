using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CollectableInteraction : MonoBehaviour
{
    [Header("Dialog Values")]
    [SerializeField]
    DialogueManager dialogue;

    public static Action playerInteractAction;
    public static Action<int> collectibleInteractAction;

    [SerializeField]
    int collectibleIndex;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.isTrigger)
        {
            if (collision.CompareTag("Player"))
            {
                playerInteractAction?.Invoke();
                dialogue.StartDialogue();
                collectibleInteractAction?.Invoke(collectibleIndex);
                this.gameObject.SetActive(false);
            }
        }


    }

}
