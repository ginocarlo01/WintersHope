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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.isTrigger)
        {
            if (collision.CompareTag("Player"))
            {
                playerInteractAction?.Invoke();
                dialogue.StartDialogue();
                this.gameObject.SetActive(false);
            }
        }


    }

}
