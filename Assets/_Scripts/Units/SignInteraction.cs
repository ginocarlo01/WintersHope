using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class SignInteraction : MonoBehaviour
{
    [Header("Dialog Values")]
    [SerializeField]
    DialogueManager dialogue;

    public static Action playerInteractAction;

    bool canInteract;

    private void Update()
    {
        if (!canInteract) { return; }

        if(Input.GetMouseButton(0))
        {
            playerInteractAction?.Invoke();
            dialogue.StartDialogue();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canInteract && !collision.isTrigger)
        {
            if (collision.CompareTag("Player"))
            {
                canInteract = true;
            }
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (canInteract && !collision.isTrigger)
        {
            if (collision.CompareTag("Player"))
            {
                canInteract = false;
            }
        }
    }
}
