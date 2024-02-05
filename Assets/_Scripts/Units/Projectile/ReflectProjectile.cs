using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectProjectile : MonoBehaviour
{
    [SerializeField]
    string mirrorTag = "Mirror";

    private ProjectileController controller;

    private void Start()
    {
        controller = GetComponent<ProjectileController>();
        if(controller == null )
        {
            Debug.LogWarning("Projectile Controller not found!");
        }
    }

   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (!collision.gameObject.CompareTag(mirrorTag))
        {
            return;
        }
        Debug.Log("Collided with mirror!");
        Vector3 surfaceNormal = (collision.transform.position - transform.position).normalized;

        // Calculate the reflection direction based on the surface normal
        controller.currentDirection = Vector3.Reflect(controller.currentDirection, surfaceNormal);
        Debug.Log("Direction changed!");
        
    }
}
