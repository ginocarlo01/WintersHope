using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToProjectileArsenal : MonoBehaviour
{
    [SerializeField]
    private string playerTag = "Player";

    [SerializeField]
    private TypeUtility.Type type;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == playerTag)
        {
            ControlAroundBorder controlAroundBorder = collision.GetComponentInChildren<ControlAroundBorder>();
            if(controlAroundBorder != null )
            {
                controlAroundBorder.AddProjectileTypeToArsenal(type);
                this.gameObject.SetActive(false);
            }
            
        }
        
    }
}
