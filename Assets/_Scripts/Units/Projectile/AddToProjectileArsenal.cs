using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToProjectileArsenal : MonoBehaviour
{
    [SerializeField]
    private string playerTag = "Player";

    [SerializeField]
    private TypeUtility.Type type;

    private Animator animator;

    [SerializeField]
    DialogueManager orientationDialogue;

    public static Action playerInteractAction;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetTrigger(type.ToString());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == playerTag)
        {
            ControlAroundBorder controlAroundBorder = collision.GetComponentInChildren<ControlAroundBorder>();
            if(controlAroundBorder != null )
            {
                controlAroundBorder.AddProjectileTypeToArsenal(type);
                if(orientationDialogue != null)
                {
                    orientationDialogue.StartDialogue();
                    playerInteractAction?.Invoke();

                }
                this.gameObject.SetActive(false);
            }
            
        }
        
    }
}
