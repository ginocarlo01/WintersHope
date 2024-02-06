using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldReflect : MonoBehaviour
{
    [SerializeField]
    string projectileTag = "Enemy";
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(projectileTag))
        {
            Debug.Log("reflected");
            Transform enemyTransform = collision.gameObject.transform;

            Vector3 currentRotation = enemyTransform.eulerAngles;

            // Add 180 degrees to the Z axis
            currentRotation.z += 180f;

            // Apply the new rotation
            enemyTransform.eulerAngles = currentRotation;
        }
    }
}
